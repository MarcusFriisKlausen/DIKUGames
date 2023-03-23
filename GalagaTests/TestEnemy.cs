/* using System.Collections.Generic;
using NUnit.Framework.Constraints;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga;

namespace GalagaTest {
    [TestFixture]
    public class TestEnemy {
        private List<Image> enemyStride;
        private List<Image> alternativeEnemyStride;
        private Enemy enemy;
        
        [SetUp]
        public void InitializeEnemy() {
            enemyStride = new List<Image>();
            alternativeEnemyStride = new List<Image>();
            Isquadron.CreateEnemies(enemyStride, alternativeEnemyStride);
            enemy = new Enemy(new DynamicShape(0.0f, 0.0f, 1.0f, 1.0f), 
                              enemyStride, alternativeEnemyStride);
        }
        
        [Test]
        public void TestIsEnraged() {
            enemy.Enrage();
            Assert.That(enemy.ImageStride, Is.EqualTo(enemy.AlternativeImageStride));
        }
    }
} */