using galagaTests.ISquadron;
using Galaga;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace galagaTests;
    [TestFixture]
    public class TestEnemy{
        [SetUp]
        public void Setup(){
            List<Image> images = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            
            List<Image> enrageStride = ImageStride.CreateStrides
                (2, Path.Combine("Assets","Images", "RedMonster.png"));

            ISquadron squadron = new SquadronFromV();
            squadron.CreateEnemies(images, enrageStride);
        }

        [Test]
        public void TestMaxEnemies() {
            //Assert
            Assert.AreEqual(squadron.MaxEnemies, 5);
        }
        
        [Test]
        public void TestEnemyCount() {
            // Act
            EntityContainer<Enemy> enemies1 = squadron.Enemies;
            enemies[0].DeleteEntity();
            enemies[3].DeleteEntity();
            EntityContainer<Enemy> enemies2 = RemoveEntities(enemies);
            //Assert
            Assert.AreEqual(enemies1.CountEntities(), 5);
            Assert.AreEqual(enemies2.CountEntities(), 3);
        }
    }