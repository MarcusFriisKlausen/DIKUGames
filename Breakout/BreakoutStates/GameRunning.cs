<<<<<<< Updated upstream
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Physics;

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState {
    private Entity backGroundImage;
    private Player player;
    private EntityContainer<Entity> blocks;
    private Ball ball;
    //private Health health;
    private static GameRunning? instance;
    private GameEventBus eventBus;
    private LevelLoader levelLoader;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;
    }
    //public void InitGame();

    public GameRunning() {
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));

        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));

        instance = null;

        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);

        levelLoader = new LevelLoader();

        blocks = levelLoader.LevelMaker(levelLoader.currentMap);

        ball = new Ball(
            new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));    
    }

    private void InitGame() {
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));

        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f, 0.02f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));

        // BreakoutBus Subscriptions
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);
        
        // Health
        // health = new Health(new Vec2F(0.0f, -0.2f), new Vec2F(0.3f, 0.32f));

        ball = new Ball(
            new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));
    }

        private void LosBreakosBlocksos() {
            blocks.Iterate(block => {
                var blockShape = block.Shape;
                var ballShape = ball.Shape;
                var colData = CollisionDetection.Aabb(ballShape.AsDynamicShape(), blockShape);
                if (colData.Collision == true) {
                    block.DeleteEntity();
                } 
            });
            RemoveEntities(blocks);
        }
        public EntityContainer<Entity> RemoveEntities(EntityContainer<Entity> container){
            var count = container.CountEntities();
            EntityContainer<Entity> newCont = new EntityContainer<Entity>(count);

            foreach (Entity ent in container) {
                if (!ent.IsDeleted()) {
                    newCont.AddEntity(ent);
                }
            }
            return newCont;
        }

        public void HealthUpdate() {
            // health.display.SetText("HEALTH: " + health.ToString());
            throw new NotImplementedException();
        }

    public void ResetState() {
        InitGame();
    }

    public void UpdateState() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        ball.Move(blocks, player);
        // LosBreakosBlocksos();
        // HealthUpdate();

    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        player.Render();
        blocks.RenderEntities();
        ball.RenderEntity();
        // health.RenderHealth();

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
                    BreakoutBus.GetBus().RegisterEvent(pause);
                    break;
            }
        }

=======
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Physics;

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState {
    private Entity backGroundImage;
    private Player player;
    private EntityContainer<Block> blocks;
    private Ball ball;
    public Points Score;
    public Health health;
    // private Health health;
    private static GameRunning? instance;
    private GameEventBus eventBus;
    private LevelLoader levelLoader;
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

        ball = new Ball(
            new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));

        instance = null;

        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, ball);

        levelLoader = new LevelLoader();

        blocks = levelLoader.LevelMaker();

        Score = new Points(new Vec2F(0.0f, -0.25f), new Vec2F(0.3f, 0.32f));
        health = new Health();
    }

    private void InitGame() {
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f, 0.02f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));
        
        ball = new Ball(
            new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));

        // BreakoutBus Subscriptions
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, ball);
        
        // Health
        // health = new Health(new Vec2F(0.1f, 0.1f));

        levelLoader = new LevelLoader();

        blocks = levelLoader.LevelMaker();

        Score = new Points(new Vec2F(0.0f, -0.25f), new Vec2F(0.3f, 0.32f));
        health = new Health();
    }
    public void HealthUpdate() {
        if (ball.LosingHealth() == 1) {
            health.LoseHealth();
        ball = new Ball(
            new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));
        }
        if (health.Value == 0) {
            GameEvent gameOver = new GameEvent();
                                    gameOver.EventType = GameEventType.GameStateEvent;
                                    gameOver.Message = "GAME_LOST";
                                    BreakoutBus.GetBus().RegisterEvent(gameOver);
        }
    }

    public void ResetState() {
        InitGame();
    }

    public void UpdateState() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        ball.Move(blocks, player);
        if ((blocks.CountEntities() == 0)) {
            blocks = levelLoader.LevelMaker();
        }
        Score.PointsUpdate();
        HealthUpdate();
    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        player.Render();
        blocks.RenderEntities();
        ball.RenderEntity();
        Score.RenderPoints();
        health.RenderHealth();
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
                    BreakoutBus.GetBus().RegisterEvent(pause);
                    break;
            }
        }

>>>>>>> Stashed changes
}