using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;

using Breakout.BreakoutStates;
/// <summary>
/// This class is responsible for setting up the game, as it initializes and subscribes to the
/// eventBus. It handles and processes events, as well as rendering and updating active states
/// </summary>
namespace Breakout;
public class Game : DIKUGame, IGameEventProcessor{
    
    private StateMachine stateMachine;
    private GameEventBus eventBus;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
        eventBus = BreakoutBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, 
                                                              GameEventType.WindowEvent,
                                                              GameEventType.GameStateEvent,
                                                              GameEventType.PlayerEvent,
                                                              GameEventType.TimedEvent});
        window.SetKeyEventHandler(KeyHandler);
        
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
    }   
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.Message == "QUIT") {
            window.CloseWindow();
        }
        stateMachine.ProcessEvent(gameEvent);
    }
    

    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }
    
    public override void Update() {
        stateMachine.ActiveState.UpdateState();
    }
}  