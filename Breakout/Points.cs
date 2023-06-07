using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
/// <summary>
/// This class is responsible for rendering, updating and incrementing the score
/// </summary>
public class Points {
    private int count;
    public int Count { get { return count; } }
    private Text display;
    public Points (Vec2F position, Vec2F extent) {
        count = 0;
        display = new Text ("SCORE: " + (count.ToString()), position, extent);
        display.SetColor(255, 255, 255, 255);
    }

    public void IncrementScore(int points) {
        count = count + points;
    }

    public void PointsUpdate() {
        display.SetText("SCORE: " + count.ToString());
    }

    public void RenderPoints () {
            display.RenderText();
    }

    public override string ToString() {
        return count.ToString();
    }
}