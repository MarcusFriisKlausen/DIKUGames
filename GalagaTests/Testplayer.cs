using galagaTests.Player;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using Galaga;

namespace galagaTests;
    [TestFixture]
    public class TestPlayer{
        [SetUp]
        public void Setup(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GalagaBus.CreateBus();
            
            Player player = new player(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        }

        [Test]
        public void TestGetPosition(){
            //Act
            Vec2F pos = player.shape.Position;
            //Assert
            Assert.AreEqual(pos, player.GetPosition());
        }

        [Test]
        public void TestSetMoveLeft() {
            //Arrange
            GameEvent goLeft = new GameEvent();
            goLeft.EventType = GameEventType.PlayerEvent;
            goLeft.Message = KeyboardKey.Left.ToString();

            Vec2F posX = player.shape.Position.X;
            //Act
            player.ProcessEvent(goLeft);
            player.Move();

            Vec2F newPosX = player.shape.Position.X;
            //Assert
            Assert.AreEqual(posX - player.MOVEMENT_SPEED, newPosX);
        }
    }