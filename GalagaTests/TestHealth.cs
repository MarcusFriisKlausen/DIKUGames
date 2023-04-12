using Galaga;
using DIKUArcade.Math;

namespace galagaTests;
    [TestFixture]
    public class TestHealth{
        [SetUp]
        public void Setup(){
            // Health health = new Health(new Vec2F(0,0), new Vec2F(0,0));
        }

        private Health health = new Health(new Vec2F(0,0), new Vec2F(0,0));

        [Test]
        public void TestLoseHealth(){
            //Act
            health.LoseHealth();
            //Assert
            Assert.AreEqual(2, health.HEALTH);
        }
        [Test]
        public void TestHealthToString(){
            //Act
            health.ToString();
            //Assert
            Assert.AreEqual("3", health.ToString());
        }
    }