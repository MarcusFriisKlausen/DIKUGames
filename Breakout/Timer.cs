using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout;

public class Timer {
    private int timeSeconds;
    private int maxTimeSeconds;
    public int TimeSeconds { get { return timeSeconds; } }
    private Text display;
    public Timer (int maxTime, Vec2F position, Vec2F extent) {
        maxTimeSeconds = maxTime;
        timeSeconds = 0;
        display = new Text ("TIME: " + ((maxTime - timeSeconds).ToString()), position, extent);
        display.SetColor(255, 255, 255, 255);
    }

    public void timeGameOver() {
        if (timeSeconds > maxTimeSeconds) {
            GameEvent gameOver = new GameEvent();
                                    gameOver.EventType = GameEventType.GameStateEvent;
                                    gameOver.Message = "GAME_LOST";
                                    BreakoutBus.GetBus().RegisterEvent(gameOver);
        }
    }
}