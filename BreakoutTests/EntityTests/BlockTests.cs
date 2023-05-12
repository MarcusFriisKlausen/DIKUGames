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
        public void TestLosingHealthBlock(){
            block.LoseHealth();
            int expectedHP = block.MaxHealth - 1;
            Assert.AreEqual(block.Health, expectedHP);
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