using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout;

public class Timer {
    private int timeSeconds;
    private int maxTimeSeconds;
    public int MaxTimeSeconds{get {return maxTimeSeconds;}}
    public int TimeSeconds { get { return timeSeconds; } }
    private int reducedTime;
    private int moreTime;
    private Text? display;
    private Vec2F pos; 
    private Vec2F ext;
    public Timer (Vec2F position, Vec2F extent) {
        timeSeconds = 0;
        pos = position;
        ext = extent;
    }

    public void TurnOffReset() {
        display = null;
    }
    
    public void SetTime(int time) {
        timeSeconds = 0;
        reducedTime = 0;
        maxTimeSeconds = time;
        display = new Text ("TIME: " + ((maxTimeSeconds - timeSeconds).ToString()), pos, ext);
        display.SetColor(255, 255, 255, 255);
    }

    public void ReduceTime() {
        this.reducedTime += 10;
    }
    public void MoreTime(){
        this.moreTime -= 10;
    }

    public void TimeGameOver() {
        if (timeSeconds >= maxTimeSeconds) {
            GameEvent gameOver = new GameEvent();
                                    gameOver.EventType = GameEventType.GameStateEvent;
                                    gameOver.Message = "GAME_LOST";
                                    BreakoutBus.GetBus().RegisterEvent(gameOver);
        }
    }

    public void RenderTime () {
        if (display is not null) {   
            display.RenderText();
        }
    }

    public void TimeUpdate() {
        if (display is not null) {  
            timeSeconds = (int)StaticTimer.GetElapsedSeconds() + reducedTime + moreTime;
            display.SetText("TIME: " + ((maxTimeSeconds - timeSeconds).ToString()));
        }
    }
}