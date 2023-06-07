using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using NUnit.Framework;
using Breakout;

public class TimerTest{
    Breakout.Timer timer;

    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        timer = new Breakout.Timer(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));
    }

    [TestCase(100)]
    [TestCase(200)]
    [TestCase(300)]
    public void TestSetTime(int t){
        int exp = t;
        timer.SetTime(t);
        int res = timer.MaxTimeSeconds;
        Assert.That(exp, Is.EqualTo(res));
    }

    [Test]
    public void TestReducedTime(){
        int exp = 10;
        timer.ReduceTime();
        int res = timer.ReducedTime;
        Assert.That(exp, Is.EqualTo(res));
    }

    [Test]
    public void TestMoreTime(){
        int exp = -10;
        timer.MoreTime();
        int res = timer.MoreTime_;
        Assert.That(exp, Is.EqualTo(res));
    }

}