using Galaga.GalagaStates;
using DIKUArcade.EvenBus;

namespace galagaTests;
[TestFixture]
public class StateMachineTest{
    private StateMachine stateMachine;

    [SetUp]
    public void InitiateStateMachine() {
        DIKUArcade.Window.CreateOpenGLContext();
        GalagaBus.CreateBus();
        stateMachine = new StateMachine();
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }

    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestEventGamePaused() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_PAUSED"
            }
        );
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }

}