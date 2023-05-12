using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;


namespace Breakout;
public class Player : IGameEventProcessor {
    private Entity entity;
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
        this.shape = shape;
        this.moveLeft = 0.0f;
        this.moveRight = 0.0f;
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
    }
}
    
