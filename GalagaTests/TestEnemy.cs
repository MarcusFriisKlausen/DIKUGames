using Galaga;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;

namespace galagaTests;
    [TestFixture]
    public class TestEnemy{
        [SetUp]
        public void Setup() {
            enemy = new Enemy(new DynamicShape(
                                new Vec2F(0.45f, 0.9f - 0.1f), 
                                new Vec2F(0.1f, 0.1f)),
                                new ImageStride(80, images));
        }

        private Enemy enemy;
        private List<Image> enrageStride = 
            ImageStride.CreateStrides(2, Path.Combine("Assets","Images", "RedMonster.png"));
        private List<Image> images = 
            ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));

        [Test]
        public void TestIsEnraged(){
            //Act
            enemy.Enrage(enrageStride);
            //Assert
            Assert.AreEqual(true, enemy.IsEnraged);
        }
        [Test]
        public void TestEnragedMovementSpeed(){
            //Act
            enemy.Enrage(enrageStride);
            //Assert
            Assert.AreEqual(0.0009f, enemy.MOVEMENT_SPEED);
        }
        [Test]
        public void TestIncreasesMovementSpeed(){
            //Act
            enemy.IncreaseMS(1);
            //Assert
            Assert.AreEqual(0.0004f, enemy.MOVEMENT_SPEED);
        }
        [Test]
        public void TestEnemyStridesRedImage(){
            //Act
            enemy.Enrage(enrageStride);
            //Assert
            Assert.AreEqual(enemy.Image, enrageStride);
        }
    }
