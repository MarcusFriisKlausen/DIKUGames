using NUnit.Framework;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Physics;
using Breakout.Effects;
using Breakout.Blocks;
using DIKUArcade.Timers;
using Breakout.Hazards;
using Breakout.BreakoutStates;

namespace TestStates;

[TestFixture]
public class GamePausedTest{
    GamePaused gamePaused;

    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        gamePaused = new GamePaused();
    }

    [Test]
    public void TestHandleKeyEventRelease(){
        var exp = gamePaused.ActiveMenuButton;
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var res = gamePaused.ActiveMenuButton;
        Assert.That(res, Is.EqualTo(exp));
    }

    [Test]
    public void TestHandleKeyEventUp(){
        var exp = gamePaused.ActiveMenuButton;
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        var res = gamePaused.ActiveMenuButton;
        Assert.That(res, Is.EqualTo(exp));
    }

    [Test]
    public void TestHandleKeyEventDown(){
        var exp = gamePaused.ActiveMenuButton + 1;
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        var res = gamePaused.ActiveMenuButton;
        Assert.That(res, Is.EqualTo(exp));
    }
}