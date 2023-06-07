using NUnit.Framework;
using Breakout;
using Breakout.BreakoutStates;
using DIKUArcade.Input;

namespace TestStates;
public class GameLostTest {
    GameLost gameLost;
    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        gameLost = new GameLost();
    }
    
    [Test]
    public void TestHandleKeyEventUp(){
        var exp = gameLost.ActiveMenuButton - 1;
        gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        var res = gameLost.ActiveMenuButton;
        Assert.That(exp, Is.EqualTo(res));
    }

    [Test]
    public void TestHandleKeyEventDown(){
        var exp = gameLost.ActiveMenuButton;
        gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        var res = gameLost.ActiveMenuButton;
        Assert.That(exp, Is.EqualTo(res));
    }
}