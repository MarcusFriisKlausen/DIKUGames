using galagaTests.Health;

namespace galagaTests;
    [TestFixture]
    public class TestEnemy{
        [SetUp]
        public void Setup(){
            health = 5;
        }

        [Test]
        public void TestLoseHealth(){
            //Act
            health.LoseHealth();
            //Assert
            Assert.AreEqual(4, health);
        }
        [Test]
        public void TestHealthToString(){
            //Act
            health.ToString();
            //Assert
            Assert.AreEqual("5", health);
        }
    }