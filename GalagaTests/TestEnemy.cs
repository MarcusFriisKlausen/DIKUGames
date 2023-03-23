using galagaTests.Enemy;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using Galaga;

namespace galagaTests;
    [TestFixture]
    public class TestEnemy{
        [SetUp]
        public void Setup() {
            List<Image> images = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            
            List<Image> enrageStride = ImageStride.CreateStrides
                (2, Path.Combine("Assets","Images", "RedMonster.png"));

            Enemy enemy = new Enemy(new DynamicShape(
                            new Vec2F(0.45f, 0.9f - (float)i * 0.1f), 
                            new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, images));;
        }

        [Test]
        public void TestIsEnraged(){
            //Act
            enemy.Enrage(enrageStride);
            //Assert
            Assert.AreEqual(true, enemy.isEnraged);
        }
        [Test]
        public void TestEnragedMovementSpeed(){
            //Act
            enemy.Enrage(enrageStride);
            //Assert
            Assert.AreEqual(0.0009f, enemy.movement_speed);
        }
        [Test]
        public void TestIncreasesMovementSpeed(){
            //Act
            enemy.IncreaseMS();
            //Assert
            Assert.AreEqual(0.0004f, enemy.movement_speed);
        }
        [Test]
        public void TestEnemyStridesRedImage(){
            //Act
            enemy.Enrage(enrageStride);
            //Assert
            Assert.AreEqual(Image, enrageStride);
        }
    }
