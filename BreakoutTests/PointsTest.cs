using DIKUArcade.Graphics;
using DIKUArcade.Math;
using NUnit.Framework;
using Breakout;

public class PointsTest{
    Points points;
    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        points = new Points(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));
    }

    [Test]
    public void TestIncrementScore(){
        int exp = 10; // 0 + 10 = 10 = score
        points.IncrementScore(10);
        int res = points.Count;
        Assert.That(exp, Is.EqualTo(res));
    }
}