using galagaTests.Galagastates;

namespace galagaTests;
[TestFixture]
public class TestTransformToString{

    [SetUp]
    public void SetUp(){
        stateTrans = new StateTransformer();
    }

    [Test]
    public void TestRunning(){
        a = stateTrans.TransformStateToString(GameRunning);
        Assert.AreEqual("GAME_RUNNING", a);
    }

    [Test]
    public void TestPaused(){
        a = stateTrans.TransformStateToString(GamePaused);
        Assert.AreEqual("GAME_PAUSED", a);
    }

    [Test]
    public void TestMenu(){
        a = stateTrans.TransformStateToString(MainMenu);
        Assert.AreEqual("MAIN_MENU", a);
    }
}