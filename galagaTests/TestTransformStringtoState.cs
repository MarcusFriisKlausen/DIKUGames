using Galaga.GalagaStates;
using DIKUArcade.State;

namespace galagaTests;
[TestFixture]
public class TestTransformToState{

    [SetUp]
    public void SetUp(){
    }

    private GameStateType a;
    private GameStateType gameRunning = GameStateType.GameRunning;
    private GameStateType gamePaused = GameStateType.GamePaused;
    private GameStateType mainMenu = GameStateType.MainMenu;


    [Test]
    public void TestRunning(){
        a = StateTransformer.TransformStringToState("GAME_RUNNING");
        Assert.AreEqual(gameRunning, a);
    }

    [Test]
    public void TestPaused(){
        a = StateTransformer.TransformStringToState("GAME_PAUSED");
        Assert.AreEqual(gamePaused, a);
    }

    [Test]
    public void TestMenu(){
        a = StateTransformer.TransformStringToState("MAIN_MENU");
        Assert.AreEqual(mainMenu, a);
    }
}