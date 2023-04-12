using Galaga;
using Galaga.GalagaStates;

namespace galagaTests;
[TestFixture]
public class TestTransformToString{

    [SetUp]
    public void SetUp(){
    }

    private string a = "";

    [Test]
    public void TestRunning(){
        a = StateTransformer.TransformStateToString(GameStateType.GameRunning);
        Assert.AreEqual("GAME_RUNNING", a);
    }

    [Test]
    public void TestPaused(){
        a = StateTransformer.TransformStateToString(GameStateType.GamePaused);
        Assert.AreEqual("GAME_PAUSED", a);
    }

    [Test]
    public void TestMenu(){
        a = StateTransformer.TransformStateToString(GameStateType.MainMenu);
        Assert.AreEqual("MAIN_MENU", a);
    }
}