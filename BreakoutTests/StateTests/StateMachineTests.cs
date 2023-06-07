//using NUnit.Framework;
//using Breakout.BreakoutStates;
//using Breakout;
//
//
//namespace TestStates;
//public class StateMachineTest{
//    StateMachine stateMachine;
//
//    [SetUp]
//    public void SetUp(){
//        DIKUArcade.GUI.Window.CreateOpenGLContext();
//        stateMachine = new StateMachine();
//    }
//
//    [Test]
//    public void TestSwitchStateGameRunning(){
//        var exp = GameRunning.GetInstance();
//        stateMachine.SwitchState(GameStateType.GameRunning);
//        var res = stateMachine.ActiveState;
//        Assert.That(res, Is.EqualTo(exp));
//    }
//    
//    [Test]
//    public void TestSwitchStateGamePaused(){
//        var exp = GamePaused.GetInstance();
//        stateMachine.SwitchState(GameStateType.GamePaused);
//        var res = stateMachine.ActiveState;
//        Assert.That(res, Is.EqualTo(exp));
//    }
//
//    [Test]
//    public void TestSwitchStateMainMenu(){
//        var exp = MainMenu.GetInstance();
//        stateMachine.SwitchState(GameStateType.MainMenu);
//        var res = stateMachine.ActiveState;
//        Assert.That(res, Is.EqualTo(exp));
//    }
//
//    [Test]
//    public void TestSwitchStateGameLost(){
//        var exp = GameLost.GetInstance();
//        stateMachine.SwitchState(GameStateType.GameLost);
//        var res = stateMachine.ActiveState;
//        Assert.That(res, Is.EqualTo(exp));
//    }
//}