using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using Breakout.BreakoutStates;
using DIKUArcade.Timers;


namespace Breakout;
/// <summary>
/// This class is responsible for creating and positioning the player, which includes functions
/// for making the player able to move left and right. The class also contains functions
/// responsible for power ups and hazards affecting the player, and processing these events
/// </summary>
public class Player : IGameEventProcessor {
    private GameEvent widenBack;
    private GameEvent falseInvincible;
    private double? widenTimeStop;
    private double? invincibleTimeStop;
    private bool isWiden = false;
    private Entity entity;
    private Health health;
    public Health Health {
        get {
            return health;
        }
    }
    private DynamicShape shape;
    public DynamicShape Shape {
        get {return shape;}
    }
    private float moveLeft;
    private float moveRight;
    private const float MOVEMENT_SPEED = 0.01f;
    private GameEventBus eventBus;
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
        eventBus.Subscribe(GameEventType.TimedEvent, this);
        this.shape = shape;
        this.moveLeft = 0.0f;
        this.moveRight = 0.0f;
        this.health = new Health();
    }

    public void Move() {
        float oldX = this.shape.Position.X;
        this.shape.Move();
        if (this.shape.Position.X < 0.0f) {
            this.shape.Position.X = 0.0f;
        } else if (this.shape.Position.X > 1.0f - this.shape.Extent.X) {
            this.shape.Position.X = 1.0f - this.shape.Extent.X;
        }
        if (this.shape.Position.X != oldX) {
            this.moveLeft = 0.0f;
            this.moveRight = 0.0f;
        }
    }
    private void SetMoveLeft(bool val) {
        if (val == true){
           this.moveLeft -= MOVEMENT_SPEED; 
        }
        else{
            this.moveLeft = 0.0f;
        }
        UpdateDirection();
    }
    private void SetMoveRight(bool val) {
        if (val == true){
            this.moveRight += MOVEMENT_SPEED; 
        }
        else{
            this.moveRight = 0.0f;
        }
        UpdateDirection();
    }
    private void UpdateDirection() {
        this.shape.Direction.X = this.moveLeft + this.moveRight;
    }
    public void Render() {
        entity.RenderEntity();
    }
    public Vec2F GetPosition() {
        return this.shape.Position;
    }
    public float GetMS() {
        return MOVEMENT_SPEED;
    }

    public void ReduceTime() {
        GameEvent reduceTime = new GameEvent();
            reduceTime.EventType = GameEventType.PlayerEvent;
            reduceTime.Message = "reduceTime";
            BreakoutBus.GetBus().RegisterEvent(reduceTime);
        ProcessEvent(reduceTime);
    }
    public void MoreTime() {
        GameEvent moreTime = new GameEvent();
            moreTime.EventType = GameEventType.PlayerEvent;
            moreTime.Message = "moreTime";
            BreakoutBus.GetBus().RegisterEvent(moreTime);
        ProcessEvent(moreTime);
    }

    public void MakeInvincible() {
        GameEvent trueInvincible = new GameEvent();
           trueInvincible.EventType = GameEventType.PlayerEvent;
           trueInvincible.Message = "trueInvincible";
           BreakoutBus.GetBus().RegisterEvent(trueInvincible);
        ProcessEvent(trueInvincible);

        falseInvincible = new GameEvent();
            falseInvincible.EventType = GameEventType.PlayerEvent;
            falseInvincible.Message = "falseInvincible";
            BreakoutBus.GetBus().RegisterEvent(falseInvincible);
        invincibleTimeStop = StaticTimer.GetElapsedSeconds() + 10;
    }

    public void Widen(){
        GameEvent widen = new GameEvent();
            widen.EventType = GameEventType.PlayerEvent;
            widen.Message = "widen";
            BreakoutBus.GetBus().RegisterEvent(widen);
        ProcessEvent(widen);

        widenBack = new GameEvent();
            widenBack.EventType = GameEventType.PlayerEvent;
            widenBack.Message = "widenBack";
            BreakoutBus.GetBus().RegisterEvent(widenBack);
        widenTimeStop = StaticTimer.GetElapsedSeconds() + 10;
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.Message == KeyboardKey.Left.ToString()) {
                this.SetMoveLeft(true);
        }
        else if (gameEvent.Message == KeyboardKey.Right.ToString()) {
            this.SetMoveRight(true);
        }
        else if (gameEvent.Message == KeyboardKey.Right.ToString() + " Stop") {
            this.SetMoveRight(false);
        }
        else if (gameEvent.Message == KeyboardKey.Left.ToString() + " Stop") {
            this.SetMoveLeft(false);
        }
        else if (gameEvent.Message == "reduceTime") {
            if ((GameRunning.GetInstance().levelLoader.timer is not null)) {
                GameRunning.GetInstance().levelLoader.timer.ReduceTime();
            }
        }
        else if (gameEvent.Message == "moreTime") {
            if (GameRunning.GetInstance().levelLoader.timer is not null) {
                GameRunning.GetInstance().levelLoader.timer.MoreTime();
            }
        }
        else if (gameEvent.Message == "trueInvincible") {
            this.health.Invincible = true;
        }
        else if (gameEvent.Message == "falseInvincible") {
            this.health.Invincible = false;
        }
        else if (gameEvent.Message == "widen") {
            if (!isWiden) {
                this.Shape.Extent = new Vec2F(this.Shape.Extent.X * 2, this.Shape.Extent.Y);
            } 
            isWiden = true;
        }
        else if (gameEvent.Message == "widenBack") {
            this.Shape.Extent = new Vec2F(this.Shape.Extent.X * 0.5f, this.Shape.Extent.Y);
            isWiden = false;
        }
    }

    public void ProcessTimedEvents() {
        if (widenTimeStop is not null) {
            if ((int)widenTimeStop == (int)StaticTimer.GetElapsedSeconds()) {
                if (isWiden) {
                    ProcessEvent(widenBack);
                }
            }
        }
        if (invincibleTimeStop is not null) {
            if ((int)invincibleTimeStop == (int)StaticTimer.GetElapsedSeconds()) {
                if (this.Health.Invincible) {
                    ProcessEvent(falseInvincible);
                }
            }
        }
    }
}
    
