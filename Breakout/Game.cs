using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;

using Breakout.BreakoutStates;

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
                                                              GameEventType.PlayerEvent });
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