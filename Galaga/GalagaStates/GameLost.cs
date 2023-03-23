using DIKUArcade;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.GalagaStates;
    public class GameLost : IGameState{
        private static GameLost instance = null;
        private Text levelDisplay;
        public static GameLost GetInstance() {
            if (GameLost.instance == null) {
                GameLost.instance = new GameLost();
                GameLost.instance.ResetState();
            }
            return GameLost.instance;
        }
        public void ResetState() {
            levelDisplay = GameRunning.GetInstance().LevelDisplay;
            levelDisplay.SetColor(255, 255, 255, 255);
        }

        public void RenderState() {
            levelDisplay.RenderText();
        }

        public void UpdateState() {
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        }
    }