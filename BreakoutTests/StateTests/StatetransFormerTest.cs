using NUnit.Framework;
using Breakout.BreakoutStates;
using Breakout;

namespace TestStates;
public class StateTransformerTest{

    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }

    [Test]
    public void TestTransformStringToStateGameRunning(){
        var exp = GameStateType.GameRunning;
        var res = StateTransformer.TransformStringToState("GAME_RUNNING");
        Assert.That(res, Is.EqualTo(exp));
    }

    [Test]
    public void TestTransformStringToStateGamePaused(){
        var exp = GameStateType.GamePaused;
        var res = StateTransformer.TransformStringToState("GAME_PAUSED");
        Assert.That(res, Is.EqualTo(exp));
    }

    [Test]
    public void TestTransformStringToStateGameMainMenu(){
        var exp = GameStateType.MainMenu;
        var res = StateTransformer.TransformStringToState("MAIN_MENU");
        Assert.That(res, Is.EqualTo(exp));
    }

    [Test]
    public void TestTransformStringToStateGameLost(){
        var exp = GameStateType.GameLost;
        var res = StateTransformer.TransformStringToState("GAME_LOST");
        Assert.That(res, Is.EqualTo(exp));
    }
}