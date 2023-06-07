namespace Breakout.BreakoutStates;
/// <summary>
/// This class is responsible for transforming a string to a state.
/// </summary>
public class StateTransformer {
    public static GameStateType TransformStringToState(string state) {
        switch(state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "QUIT":
                return GameStateType.MainMenu;
            case "GAME_LOST":
                return GameStateType.GameLost;
            default:
                throw new System.ArgumentException(
                        "Input string does not correlate to any existing game state");
        }
    }
}
