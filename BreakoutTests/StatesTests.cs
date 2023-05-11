using NUnit.Framework;
using Breakout;
using Breakout.Blocks;
using Breakout.BreakoutStates;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Utilities;
using System.Collections.Generic;


namespace TestStates;
    [TestFixture]
    public class Tests{
        string a;
        string b;
        StateMachine stateMachine;
        [SetUp]
        public void Setup(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            a = "";
            b = "";
            stateMachine = new StateMachine();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        //Testing if TransformStateToString is able to transform a state to a string
        public void TestTransformStateToStringRunning(){
            a = StateTransformer.TransformStateToString(GameStateType.GameRunning);
            b = "GAME_RUNNING";
            Assert.That(b, Is.EqualTo(a));
        }
    
        [Test]
        //Testing if TransformStateToString is able to transform a state to a string
        public void TestTransformStateToStringPaused(){
            a = StateTransformer.TransformStateToString(GameStateType.GamePaused);
            b = "GAME_PAUSED";
            Assert.That(b, Is.EqualTo(a));
        }
    
        [Test]
        //Testing if TransformStateToString is able to transform a state to a string
        public void TestTransformStateToStringMenu(){
            a = StateTransformer.TransformStateToString(GameStateType.MainMenu);
            b = "MAIN_MENU";
            Assert.That(b, Is.EqualTo(a));
        }
    }