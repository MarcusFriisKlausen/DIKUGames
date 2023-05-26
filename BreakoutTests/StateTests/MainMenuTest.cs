using NUnit.Framework;
using Breakout;
using Breakout.BreakoutStates;
using DIKUArcade.Input;

public class MainMenuTest {
    MainMenu mainMenu;
    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        mainMenu = new MainMenu();
    }

    public void TestHandleKeyEventRelease(){
        var exp = mainMenu.ActiveMenuButton;
        mainMenu.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var res = mainMenu.ActiveMenuButton;
        Assert.That(res, Is.EqualTo(exp));
    }

    public void TestHandleKeyEventUp(){
        var exp = mainMenu.ActiveMenuButton;
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        var res = mainMenu.ActiveMenuButton;
        Assert.That(res, Is.EqualTo(exp));
    }

    public void TestHandleKeyEventDown(){
        var exp = mainMenu.ActiveMenuButton - 1;
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        var res = mainMenu.ActiveMenuButton;
        Assert.That(res, Is.EqualTo(exp));
    }
}