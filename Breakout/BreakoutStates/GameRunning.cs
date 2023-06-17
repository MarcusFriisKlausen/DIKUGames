using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Physics;
using Breakout.Effects;
using Breakout.Blocks;
using DIKUArcade.Timers;

namespace Breakout.BreakoutStates;
/// <summary>
/// This class is responsible for instantiating  
/// </summary>
public class GameRunning : IGameState {
    private Entity backGroundImage;
    private Player player;
    public Player Player {
        get {
            return player;
        }
    }
    private EntityContainer<BlockEffect> effects;
    private EntityContainer<Block> blocks;
    private Ball ball;
    public Ball Ball {
        get {
            return ball;
        }
    }
    private EntityContainer<Ball> ballCont;
    public EntityContainer<Ball> BallCont {
        get {
            return ballCont;
        }
    }
    public Points Score;
    private static GameRunning? instance;
    private GameEventBus eventBus;
    public LevelLoader levelLoader;

    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;
    }

    public GameRunning() {
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));

        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.1f), new Vec2F(0.15f, 0.02f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));

        ballCont = new EntityContainer<Ball>();

        ball = new Ball(
            new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));

        ballCont.AddEntity(ball);

        instance = null;

        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, ball);

        levelLoader = new LevelLoader();

        blocks = levelLoader.LevelMaker();
        
        Score = new Points(new Vec2F(0.0f, -0.25f), new Vec2F(0.3f, 0.32f));

        effects = new EntityContainer<BlockEffect>();
    }
    /// </summary>
    /// Initializes the game with all the components of the game, such as player, ball etc
    /// </summary>
    private void InitGame() {
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.1f), new Vec2F(0.15f, 0.02f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));

        ballCont = new EntityContainer<Ball>();
        
        ball = new Ball(
            new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));

        ballCont.AddEntity(ball);

        // BreakoutBus Subscriptions
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, ball);

        levelLoader = new LevelLoader();

        blocks = levelLoader.LevelMaker();

        Score = new Points(new Vec2F(0.0f, -0.25f), new Vec2F(0.3f, 0.32f));

        effects = new EntityContainer<BlockEffect>();
    }

    /// </summary>
    /// Updates the player health, decrements health if all balls leaves the window. 
    /// If player health reaches zero the game is over. 
    /// </summary>
    private void HealthUpdate() {
        ballCont.Iterate(b => {
            if (ballCont.CountEntities() == 1) {
                if (b.LosingHealth() == 1) {
                    player.Health.LoseHealth();
                    b.DeleteEntity();
                    ballCont.AddEntity(new Ball(
                        new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
                        new Image(Path.Combine("Assets", "Images", "ball.png"))));
                    RemoveBall(ballCont);
                }
                if (player.Health.Value == 0) {
                    GameEvent gameOver = new GameEvent();
                                            gameOver.EventType = GameEventType.GameStateEvent;
                                            gameOver.Message = "GAME_LOST";
                                            BreakoutBus.GetBus().RegisterEvent(gameOver);
                }
            } else if (b.LosingHealth() == 1) {
                b.DeleteEntity();
                RemoveBall(ballCont);
            }
        });
    }

    /// </summary>
    /// Makes the balls in the container double size
    /// </summary>
    public void DoubleSize() {
        ballCont.Iterate(b => {
            b.DoubleSize();
        });
    }

    /// </summary>
    /// Activates effects if it collides with the player, else deletes effect
    ///  and finally removes them
    /// </summary>
    private void ActivateEffect() {
        effects.Iterate(effect => {
            if (CollisionDetection.Aabb((DynamicShape)effect.Shape, player.Shape).Collision) {
                effect.Effect(player);
                effect.DeleteEntity();
            }
            if (effect.Shape.Position.Y < 0) {
                effect.DeleteEntity();
            }
        });
        effects = RemoveEffects(effects);
    }

    /// </summary>
    /// Adds effects to a new entity container to be removed
    /// </summary>
    public EntityContainer<BlockEffect> RemoveEffects(EntityContainer<BlockEffect> container){
        var count = container.CountEntities();
        EntityContainer<BlockEffect> newCont = new EntityContainer<BlockEffect>(count);
        foreach (BlockEffect ent in container) {
            if (!ent.IsDeleted()) {
                newCont.AddEntity(ent);
            }
        }
        return newCont;
    }

    /// </summary>
    /// Adds in-game-effects to effects when a block is destroyed
    /// </summary>
    private void ProcessDeadBlocks() {
        blocks.Iterate(block => {
            if (block.Health == 0) {
                if (block.IngameEffect is not null) {
                    effects.AddEntity(block.IngameEffect);
                }
                block.DeleteEntity();
            }
        });
    }

    /// </summary>
    /// Adds blocks to a new entity container to be removed
    /// </summary>
    public EntityContainer<Block> RemoveBlocks(EntityContainer<Block> container){
        var count = container.CountEntities();
        EntityContainer<Block> newCont = new EntityContainer<Block>(count);
        foreach (Block ent in container) {
            if (!ent.IsDeleted()) {
                newCont.AddEntity(ent);
            }
        }
        return newCont;
    }

    /// </summary>
    /// Adds balls to a new entity container to be removed
    /// </summary>
    private EntityContainer<Ball> RemoveBall(EntityContainer<Ball> container){
        var count = container.CountEntities();
        EntityContainer<Ball> newCont = new EntityContainer<Ball>(count);
        foreach (Ball ent in container) {
            if (!ent.IsDeleted()) {
                newCont.AddEntity(ent);
            }
        }
        return newCont;
    }

    /// </summary>
    /// Renders all visible blocks
    /// </summary>
    private void RenderBlocks(){
        blocks.Iterate(block => {    
            if (!(block is InvisibleBlock)) {
                block.RenderEntity();
            } else if ((block is InvisibleBlock) && (((InvisibleBlock)block).Visible == true)) {
                block.RenderEntity();
            }
        });
    }

    /// </summary>
    /// Makes blocks that are different from unbreakable blocks breakable
    /// </summary>
    private bool AllBlocksUnbreakable() {
        bool b = true;
        blocks.Iterate(block => {
            if (block is not UnbreakableBlock) {
                b = false;
            }
        });
        return b;
    }

    /// </summary>
    /// Goes to the next level when all breakable blocks are gone, resets player, ball and timer
    /// </summary>
    private void NextLevel() {
        if ((blocks.CountEntities() == 0)) {
            blocks = levelLoader.LevelMaker();
            ballCont.ClearContainer();
            ballCont.AddEntity(new Ball(new DynamicShape(new Vec2F(0.485f, 0.15f), 
                new Vec2F(0.03f, 0.03f)), new Image(Path.Combine("Assets", "Images", "ball.png"))));
            // levelLoader.timer.ResetTimer();
            effects.ClearContainer();
        } else if (AllBlocksUnbreakable()) {
            blocks = levelLoader.LevelMaker();
            ballCont.ClearContainer();
            ballCont.AddEntity(new Ball(new DynamicShape(new Vec2F(0.485f, 0.15f), 
                new Vec2F(0.03f, 0.03f)), new Image(Path.Combine("Assets", "Images", "ball.png"))));
            // levelLoader.timer.ResetTimer();
            effects.ClearContainer();
        }

    }

    /// </summary>
    /// Renders balls
    /// </summary>
    private void RenderBalls() {
        ballCont.Iterate(b => {
            b.RenderEntity();
        });
    }

    /// </summary>
    /// Moves the ball
    /// </summary>
    private void MoveBalls() {
        ballCont.Iterate(b => {
            b.Move(blocks, player);
        });
    }

    /// </summary>
    /// Processes events related to the timer
    /// </summary>
    private void ProcessBallTimedEvents() {
        ballCont.Iterate(b => {
            b.ProcessTimedEvents();
        });
    }

    /// </summary>
    /// Moves the effects
    /// </summary>
    private void MoveEffects() {
        effects.Iterate(effect => {
            effect.Move();
        });
    }

    /// </summary>
    /// Resets the game
    /// </summary>
    public void ResetState() {
        InitGame();
    }

    /// </summary>
    /// Updates all components doing runtime 
    /// </summary>
    public void UpdateState() {
        eventBus.ProcessEventsSequentially();
        player.ProcessTimedEvents();
        ProcessBallTimedEvents();
        player.Move();
        MoveBalls();
        MoveEffects();
        NextLevel();
        Score.PointsUpdate();
        HealthUpdate();
        if (levelLoader.timer is not null) {
            levelLoader.timer.TimeGameOver();
            levelLoader.timer.TimeUpdate();
        }
        ProcessDeadBlocks();
        blocks = RemoveBlocks(blocks);
        ActivateEffect();
    }

    /// </summary>
    /// Renders the state
    /// </summary>
    public void RenderState() {
        backGroundImage.RenderEntity();
        player.Render();
        RenderBlocks();
        RenderBalls();
        Score.RenderPoints();
        player.Health.RenderHealth();
        if (levelLoader.timer is not null) {
            levelLoader.timer.RenderTime();
        }
        effects.RenderEntities();
    }

    /// </summary>
    /// Handles key events
    /// </summary>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyRelease){
                KeyRelease(key);
            }
            else if (action == KeyboardAction.KeyPress){
                KeyPress(key);
            }
    }

    /// </summary>
    /// Processes events when key is released
    /// </summary>
    private void KeyRelease(KeyboardKey key) {
            switch (key){
                case KeyboardKey.Left:
                    GameEvent stopLeft = new GameEvent();
                    stopLeft.EventType = GameEventType.PlayerEvent;
                    stopLeft.Message = key.ToString() + " Stop";
                    BreakoutBus.GetBus().RegisterEvent(stopLeft);
                    player.ProcessEvent(stopLeft);
                    break;
                case KeyboardKey.Right:
                    GameEvent stopRight = new GameEvent();
                    stopRight.EventType = GameEventType.PlayerEvent;
                    stopRight.Message = key.ToString() + " Stop";
                    BreakoutBus.GetBus().RegisterEvent(stopRight);
                    player.ProcessEvent(stopRight);
                    break;
            }
        }
    /// </summary>
    /// Processes events when key is pressed
    /// </summary>
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Space:
                GameEvent startBall = new GameEvent();
                startBall.EventType = GameEventType.InputEvent;
                startBall.Message = key.ToString();
                BreakoutBus.GetBus().RegisterEvent(startBall);
                ball.ProcessEvent(startBall);
                break;
            case KeyboardKey.Left:
                GameEvent moveLeft = new GameEvent();
                moveLeft.EventType = GameEventType.PlayerEvent;
                moveLeft.Message = key.ToString();
                BreakoutBus.GetBus().RegisterEvent(moveLeft);
                player.ProcessEvent(moveLeft);
                break;
            case KeyboardKey.Right:
                GameEvent moveRight = new GameEvent();
                moveRight.EventType = GameEventType.PlayerEvent;
                moveRight.Message = key.ToString();
                BreakoutBus.GetBus().RegisterEvent(moveRight);
                player.ProcessEvent(moveRight);
                break;
            case KeyboardKey.Escape:
                GameEvent pause = new GameEvent();
                pause.EventType = GameEventType.GameStateEvent;
                pause.Message = "GAME_PAUSED";
                StaticTimer.PauseTimer();
                BreakoutBus.GetBus().RegisterEvent(pause);
                break;
            case KeyboardKey.Tab:
                blocks = levelLoader.LevelMaker();
                ballCont.ClearContainer();
                ballCont.AddEntity(new Ball(new DynamicShape(new Vec2F(0.485f, 0.15f), 
                    new Vec2F(0.03f, 0.03f)), new Image(Path.Combine("Assets", "Images", "ball.png"))));
                // levelLoader.timer.ResetTimer();
                effects.ClearContainer();
                break;
        }
    }
}