using Galaga;
using Galaga.Squadron;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Galaga.GalagaStates;

namespace galagaTests;
    [TestFixture]
    public class TestISquadron{
        [SetUp]
        public void Setup(){
            List<Image> images = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            
            List<Image> enrageStride = ImageStride.CreateStrides
                (2, Path.Combine("Assets","Images", "RedMonster.png"));

            squadron.CreateEnemies(images, enrageStride);
        }

        private ISquadron squadron = new SquadronFormV();
        private GameRunning gameRunning = new GameRunning();

        [Test]
        public void TestMaxEnemies() {
            //Assert
            Assert.AreEqual(squadron.MaxEnemies, 5);
        }
        
        [Test]
        public void TestEnemyCount() {
            // Act
            EntityContainer<Enemy> enemies1 = squadron.Enemies;
            enemies1.Iterate(enemy => {
                    enemy.DeleteEntity();
                }
            );

            EntityContainer<Enemy> enemies2 = gameRunning.RemoveEntities(enemies1);
            //Assert
            Assert.AreEqual(enemies1.CountEntities(), 5);
            Assert.AreEqual(enemies2.CountEntities(), 0);
        }
    }