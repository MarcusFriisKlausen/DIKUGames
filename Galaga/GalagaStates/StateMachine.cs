// using DIKUArcade.EventBus;
using DIKUArcade.State;
using DIKUArcade.Events;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = GameStateType.MainMenu.GetInstance();
            GameStateType.GameRunning.GetInstance();
            GameStateType.GamePaused.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    break;
                // Ikke korrekt kode, indsæt korrekt i switch case
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            
        }

    }
}