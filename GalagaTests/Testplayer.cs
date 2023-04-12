using DIKUArcade.Graphics;
using DIKUArcade.Input;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Events;

namespace galagaTests;
    [TestFixture]
    public class TestPlayer{
        [SetUp]
        public void Setup(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GalagaBus.GetBus();
            
            Player player = new Player(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        }

        private Player player;

        [Test]
        public void TestGetPosition(){
            //Act
            Vec2F pos = player.Shape.Position;
            //Assert
            Assert.AreEqual(pos, player.GetPosition());
        }

        [Test]
        public void TestSetMoveLeft() {
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
            Assert.AreEqual(posX - player.GetMS(), newPosX);
        }
    }