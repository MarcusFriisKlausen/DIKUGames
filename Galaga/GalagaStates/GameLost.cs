using DIKUArcade;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.GalagaStates;
    public class GameLost : IGameState{
        private static GameLost instance = null;
        private Text levelDisplay;
        private Text gameOver;
        public static GameLost GetInstance() {
            if (GameLost.instance == null) {
                GameLost.instance = new GameLost();
                GameLost.instance.ResetState();
            }
            return GameLost.instance;
        }
        public void ResetState() {
            gameOver = new Text ("GAME OVER", new Vec2F(0.275f, 0.2f), 
            new Vec2F(0.5f, 0.5f));
            gameOver.SetColor(255, 255, 1, 1);
            levelDisplay = GameRunning.GetInstance().LevelDisplay;
            levelDisplay.SetColor(255, 255, 255, 255);
        }

        public void RenderState() {
            gameOver.RenderText();
            levelDisplay.RenderText();
        }

        public void UpdateState() {
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        }
    }