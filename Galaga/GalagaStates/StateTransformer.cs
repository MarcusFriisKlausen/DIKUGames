namespace Galaga.GalagaStates {
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
        public static string TransformStateToString(GameStateType state) {
            switch(state) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                
                case GameStateType.GameLost:
                    return "GAME_LOST";
                default:
                    throw new System.ArgumentException("Input is not an existing game state");
            }
        }
    }
}
