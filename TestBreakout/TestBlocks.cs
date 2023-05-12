using NUnit.Framework;
using Breakout;
using Breakout.BreakoutStates;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Utilities;
using System.Collections.Generic;


namespace TestBlocks;
    [TestFixture]
    public class Tests {
        BlockHardened hardened;
        Block block;
        BlockUnbreakable unbreakable;
        [SetUp]
        public void Setup(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            DynamicShape blockShape = new DynamicShape(new Vec2F(((1f/12f)), 
            (0.1f)), new Vec2F(1f/12f, 1f/25f)); 
            hardened = new BlockHardened(blockShape, new Image(Path.Combine("Assets", "Images", "brown-block.png")), 
                new Image(Path.Combine("Assets", "Images", "brown-block-damaged.png")));
            unbreakable = new BlockUnbreakable(blockShape, new Image(Path.Combine("Assets", "Images", "brown-block.png")), 
                new Image(Path.Combine("Assets", "Images", "brown-block-damaged.png")));
            block = new BlockHardened(blockShape, new Image(Path.Combine("Assets", "Images", "brown-block.png")), 
                new Image(Path.Combine("Assets", "Images", "brown-block-damaged.png")));
        }

        [Test]
<<<<<<< Updated upstream:TestBreakout/TestBlocks.cs
        // Testing if the blocks have the correct image
        public void testIsImageCorrect() {
            
            Assert.That(blueBlock.image, Is.EqualTo(blueBlock.Image));
            Assert.That(brownBlock.image, Is.EqualTo(brownBlock.Image));
            Assert.That(darkGreenBlock.image, Is.EqualTo(darkGreenBlock.Image));
            Assert.That(greenBlock.image, Is.EqualTo(greenBlock.Image));
            Assert.That(greyBlock.image, Is.EqualTo(greyBlock.Image));
            Assert.That(orangeBlock.image, Is.EqualTo(orangeBlock.Image));
            Assert.That(purpleBlock.image, Is.EqualTo(purpleBlock.Image));
            Assert.That(redBlock.image, Is.EqualTo(redBlock.Image));
            Assert.That(tealBlock.image, Is.EqualTo(tealBlock.Image));
            Assert.That(yellowBlock.image, Is.EqualTo(yellowBlock.Image));
=======
        public void TestLosingHealthBlock(){
            block.LoseHealth();
            int expectedHP = block.MaxHealth - 1;
            Assert.AreEqual(block.Health, expectedHP);
>>>>>>> Stashed changes:BreakoutTests/EntityTests/BlockTests.cs
        }

        [Test]
        public void TestLosingHealthHardened(){
            System.Console.WriteLine(hardened.health);
            hardened.LoseHealth();
            int expectedHP = 1;
            Assert.AreEqual(hardened.Health, expectedHP);
        }

        [Test]
        public void TestLosingHealthUnbreakable(){
            unbreakable.LoseHealth();
            int expectedHP = unbreakable.MaxHealth;
            Assert.AreEqual(unbreakable.Health, unbreakable.MaxHealth);
        }
    }