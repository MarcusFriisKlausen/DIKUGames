using galagaTests.Galagastates;

namespace galagaTests;
[TestFixture]
public class TestTransformToState{

    [SetUp]
    public void SetUp(){
        stateTrans = new StateTransformer();
    }

    [Test]
    public void TestRunning(){
        a = stateTrans.TransformStringToState("GAME_RUNNING");
        Assert.AreEqual(GameRunning, a);
    }

    [Test]
    public void TestPaused(){
        a = stateTrans.TransformStringToState("GAME_PAUSED");
        Assert.AreEqual(GamePaused, a);
    }

    [Test]
    public void TestMenu(){
        a = stateTrans.TransformStringToState("MAIN_MENU");
        Assert.AreEqual(MainMenu, a);
    }
}