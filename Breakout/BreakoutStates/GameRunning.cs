using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState {
    private Entity backGroundImage;
    private Player player;
    private EntityContainer<Entity> blocks;
    // private Health health;
    private static GameRunning? instance;
    private GameEventBus eventBus;
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

        instance = null;

        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);
    }

    private void InitGame() {
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));

        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.02f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));

        // BreakoutBus Subscriptions
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);
        
        // Health
        // health = new Health(new Vec2F(0.0f, -0.2f), new Vec2F(0.3f, 0.32f));
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
        // HealthUpdate();

    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        player.Render();
        // blocks.RenderEntities();
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

}