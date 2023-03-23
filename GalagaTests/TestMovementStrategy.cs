using galagaTests.IMovementStrategy;
using DIKUArcade.Math;
using Galaga;

namespace galagaTests;
    [TestFixture]
    public class Tests{
        [SetUp]
        public void Setup(){
            List<Image> images = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));

            Enemy enemy = new Enemy(new DynamicShape(
                            new Vec2F(0.45f, 0.9f - (float)i * 0.1f), 
                            new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, images));;
            
            NoMove noMove = new NoMove();
            Down down = new Down();
            ZigZagDown zigZag = new ZigZagDown();
        }

        [Test]
        public void TestNoMove(){
            //Arrange
            float startY = enemy.startPos.Y;
            float posY = enemy.Shape.Position.Y;
            //Act
            noMove.MoveEnemy(enemy);
            //Assert
            Assert.AreEqual(startY, posY);
        }
        [Test]
        public void TestMoveDown(){
            //Arrange
            float y = enemy.Shape.Position.Y;
            //Act
            down.MoveEnemy(enemy);
            float newY = enemy.Shape.Position.Y;
            //Assert
            Assert.AreEqual(enemy.Shape.Position.Y - enemy.MOVEMENT_SPEED, newY);
        }
        [Test]
        public void TestZigZagDown(){
            //Arrange
            float y_0 = enemy.StartPos.Y;
            float y_1 = enemy.Shape.Position.Y - enemy.MOVEMENT_SPEED;
            float x = enemy.StartPos.X;
            //Act
            zigZag.MoveEnemy(enemy);
            float newX = enemy.Position;
            //Assert
            Assert.AreEqual(enemy.StartPos.X + (float)(0.05f * Math.Sin((2.0 * Math.PI * 
                (y_0 - y_1)) / 0.045f)), newX);
        }
    }