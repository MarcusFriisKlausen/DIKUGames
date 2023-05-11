using NUnit.Framework;
using Breakout;
using Breakout.Blocks;
using Breakout.BreakoutStates;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Input;


namespace TestPlayer;
    [TestFixture]
    public class Tests{
        Player player;
        
        [SetUp]
        public void Setup(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            
            player = new Player(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        }

        [Test]
        //Testing if the player is in the window
        public void TestPlayerIsInWindow(){
            Assert.That(player.Shape.Position.X, Is.EqualTo(0.45f));
            Assert.That(player.Shape.Position.Y, Is.EqualTo(0.1f));
        }

        [Test]
        // Moving the player to the left for a sufficiently long time, and checking
        // that is is placed by the border of the window, but still inside
        public void TestPlayerCantMoveOutOfWindow(){
             //Arrange
            GameEvent goLeft = new GameEvent();
            goLeft.EventType = GameEventType.PlayerEvent;
            goLeft.Message = KeyboardKey.Left.ToString();

            float posX = player.Shape.Position.X;
            //Act
            player.ProcessEvent(goLeft);
            for (int i = 0; i < 100; i++) {
                player.Move();
            }

            Assert.That(player.Shape.Position.X, Is.EqualTo(0f));
            Assert.That(player.Shape.Position.Y, Is.EqualTo(0.1f));
        }

        [Test]
        // Testing if the player can move left
        public void TestPlayerMoveLeft(){
            //Arrange
            GameEvent goLeft = new GameEvent();
            goLeft.EventType = GameEventType.PlayerEvent;
            goLeft.Message = KeyboardKey.Left.ToString();

            float posX = player.Shape.Position.X;
            //Act
            player.ProcessEvent(goLeft);
            player.Move();

            float newPosX = player.Shape.Position.X;
            //Assert
            Assert.That(posX - player.GetMS(), Is.EqualTo(newPosX));
        }
        
        [Test]
        //Testing if the player can move right
        public void TestPlayerMoveRight(){
            //Arrange
            GameEvent goRight = new GameEvent();
            goRight.EventType = GameEventType.PlayerEvent;
            goRight.Message = KeyboardKey.Right.ToString();

            float posX = player.Shape.Position.X;
            //Act
            player.ProcessEvent(goRight);
            player.Move();

            float newPosX = player.Shape.Position.X;
            //Assert
            Assert.That(posX + player.GetMS(), Is.EqualTo(newPosX));
        }
    }