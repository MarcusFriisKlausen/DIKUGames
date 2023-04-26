using DIKUArcade.State;
using DIKUArcade.Events;
using System;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        //private GameRunning gameRunning = new GameRunning();
        //private MainMenu mainMenu = new MainMenu();
        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            // ActiveState = MainMenu.GetInstance();
            // GameRunning.GetInstance();
            // GamePaused.GetInstance();
            // GameLost.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    // ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    // ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    // ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GameLost:
                    //ActiveState = GameLost.GetInstance();
                    break;
                default:
                    throw new ArgumentException("Invalid GameStateType");
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.InputEvent) {
                return;
            }
            var newState =  StateTransformer.TransformStringToState(gameEvent.Message);
            SwitchState(newState);
        }

    }
}