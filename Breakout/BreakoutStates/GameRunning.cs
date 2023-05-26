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
using Breakout.Hazards;

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState {
    private Entity backGroundImage;
    private Player player;
    private EntityContainer<BlockEffect> effects;
    private EntityContainer<Block> blocks;
    private Ball ball;
    private EntityContainer<Ball> ballCont;
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
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
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

    private void InitGame() {
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f, 0.02f)),
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

    public void HealthUpdate() {
        ballCont.Iterate(b => {
            if (ballCont.CountEntities() == 1) {
                if (b.LosingHealth() == 1) {
                    player.health.LoseHealth();
                    b.DeleteEntity();
                    ballCont.AddEntity(new Ball(
                        new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
                        new Image(Path.Combine("Assets", "Images", "ball.png"))));
                    RemoveBall(ballCont);
                }
                if (player.health.Value == 0) {
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

    public void DoubleSize() {
        ballCont.Iterate(b => {
            b.DoubleSize();
        });
    }

    public void ActivateEffect() {
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
    public void ProcessDeadBlocks() {
        blocks.Iterate(block => {
            if (block.Health == 0) {
                if (block.ingameEffect is not null) {
                    effects.AddEntity(block.ingameEffect);
                }
                block.DeleteEntity();
            }
        });
    }

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

    public EntityContainer<Ball> RemoveBall(EntityContainer<Ball> container){
        var count = container.CountEntities();
        EntityContainer<Ball> newCont = new EntityContainer<Ball>(count);
        foreach (Ball ent in container) {
            if (!ent.IsDeleted()) {
                newCont.AddEntity(ent);
            }
        }
        return newCont;
    }

    public void RenderBlocks(){
        blocks.Iterate(block => {    
            if (!(block is InvisibleBlock)) {
                block.RenderEntity();
            } else if ((block is InvisibleBlock) && (((InvisibleBlock)block).Visible == true)) {
                block.RenderEntity();
            }
        });
    }

    private bool AllBlocksUnbreakable() {
        bool b = true;
        blocks.Iterate(block => {
            if (block is not UnbreakableBlock) {
                b = false;
            }
        });
        return b;
    }

    private void NextLevel() {
        if ((blocks.CountEntities() == 0)) {
            blocks = levelLoader.LevelMaker();
        } else if (AllBlocksUnbreakable()) {
            blocks = levelLoader.LevelMaker();
        }
    }

    public void RenderBalls() {
        ballCont.Iterate(b => {
            b.RenderEntity();
        });
    }

    public void MoveBalls() {
        ballCont.Iterate(b => {
            b.Move(blocks, player);
        });
    }

    private void ProcessBallTimedEvents() {
        ballCont.Iterate(b => {
            b.ProcessTimedEvents();
        });
    }

    public void MoveEffects() {
        effects.Iterate(effect => {
            effect.Move();
        });
    }

    public void ResetState() {
        InitGame();
    }

    public void UpdateState() {
        eventBus.ProcessEventsSequentially();
        player.ProcessTimedEvents();
        ProcessBallTimedEvents();
        levelLoader.timer.TimeGameOver();
        player.Move();
        MoveBalls();
        MoveEffects();
        NextLevel();
        Score.PointsUpdate();
        HealthUpdate();
        levelLoader.timer.TimeUpdate();
        ProcessDeadBlocks();
        blocks = RemoveBlocks(blocks);
        ActivateEffect();
    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        player.Render();
        RenderBlocks();
        RenderBalls();
        Score.RenderPoints();
        player.health.RenderHealth();
        levelLoader.timer.RenderTime();
        effects.RenderEntities();
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyRelease){
                KeyRelease(key);
            }
            else if (action == KeyboardAction.KeyPress){
                KeyPress(key);
            }
    }
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
            }
        }
}