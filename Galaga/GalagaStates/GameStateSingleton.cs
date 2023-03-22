using System.Collections.Generic;
using DIKUArcade.State;
using DIKUArcade.Input;

public static class GameStateSingleton {
    private static readonly Dictionary<GameStateType, IGameState> instances = new Dictionary<GameStateType, IGameState>();

    public static IGameState GetInstance(this GameStateType stateType) {
        if (!instances.ContainsKey(stateType)) {
            instances[stateType] = new GameState { State = stateType };
        }
        return instances[stateType];
    }
}

public class GameState : IGameState {
    public GameStateType State { get; set; }

    public void ResetState() {
    }

    public void UpdateState() {
    }

    public void RenderState() {
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
    }
}