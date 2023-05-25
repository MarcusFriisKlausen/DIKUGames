using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;


namespace Breakout.BreakoutStates;

public class MainMenu : IGameState {
    private static MainMenu? instance;
    private Text[] menuButtons;
    private Entity backGroundImage;
    private int activeMenuButton = 0;
    private int maxMenuButtons;
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.ResetState();
            StaticTimer.RestartTimer();
            StaticTimer.PauseTimer();
            return MainMenu.instance;
        } else {
            StaticTimer.RestartTimer();
            return MainMenu.instance;
        }
    }

    public MainMenu() {
        instance = null;
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));
        menuButtons = new Text[] {
                new Text("New Game", new Vec2F(0.45f, 0.3f), new Vec2F(0.4f, 0.4f)),
                new Text("Quit", new Vec2F(0.45f, 0.2f), new Vec2F(0.4f, 0.4f))
            };
        maxMenuButtons = menuButtons.Length;
    }
    public void ResetState() {
    }

    public void UpdateState() {
        BreakoutBus.GetBus().ProcessEvents();
        
        menuButtons[activeMenuButton].SetColor(255, 0, 255, 0);
        for (int i = 0; i < menuButtons.Length; i++){
            if (menuButtons[i] != menuButtons[activeMenuButton]){
                menuButtons[i].SetColor(255, 255, 255, 255);
            }
            menuButtons[i].RenderText();
        }
    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        for (int i = 0; i < menuButtons.Length; i++){
            menuButtons[i].RenderText();
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyRelease){
            return;
        }
        switch (key){
            case KeyboardKey.Up:
                activeMenuButton = Math.Max(activeMenuButton - 1, 0);
                break;
            case KeyboardKey.Down:
                activeMenuButton = Math.Min(activeMenuButton + 1, maxMenuButtons);
                break;
            case KeyboardKey.Enter:
                if (menuButtons[activeMenuButton].Equals(menuButtons[0])) {
                    GameEvent newGame = new GameEvent();
                    newGame.EventType = GameEventType.GameStateEvent;
                    newGame.Message = "GAME_RUNNING";
                    BreakoutBus.GetBus().RegisterEvent(newGame);
                    
                }
                else if (menuButtons[activeMenuButton].Equals(menuButtons[1])) {
                    GameEvent quit = new GameEvent();
                    quit.EventType = GameEventType.WindowEvent;
                    quit.Message = "QUIT";
                    BreakoutBus.GetBus().RegisterEvent(quit);
                }
                break;
        }
    }
}