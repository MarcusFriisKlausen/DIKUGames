using NUnit.Framework;
using Breakout;
using Breakout.Blocks;
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
        BlueBlock blueBlock;
        BrownBlock brownBlock;
        DarkGreenBlock darkGreenBlock;
        GreenBlock greenBlock;
        GreyBlock greyBlock;
        OrangeBlock orangeBlock;
        PurpleBlock purpleBlock;
        RedBlock redBlock;
        TealBlock tealBlock;
        YellowBlock yellowBlock;

        [SetUp]
        public void Setup(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            blueBlock = new BlueBlock(new DynamicShape(
                        new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                        new Vec2F(1f/12f, 1f/25f)), new Image(
                            Path.Combine(
                                "Assets", "Images", "deep-bronze-square.png")));
            brownBlock = new BrownBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
            darkGreenBlock = new DarkGreenBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
            greenBlock = new GreenBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
            greyBlock = new GreyBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
            orangeBlock = new OrangeBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
            purpleBlock = new PurpleBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
            redBlock = new RedBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
            tealBlock = new TealBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
            yellowBlock = new YellowBlock(new DynamicShape(
                            new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
                            new Vec2F(1f/12f, 1f/25f)), new Image(
                                Path.Combine(
                                    "Assets", "Images", "deep-bronze-square.png")));
        }

        [Test]
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
        }

        // public void testIsBrokenImageCorrect() {
            
        //     Assert.That(blueBlock.brokenImage, 
        //         Is.EqualTo(blueBlock.BrokenImage));
        //     Assert.That(brownBlock.brokenImage, 
        //         Is.EqualTo(brownBlock.BrokenImage));
        //     Assert.That(darkGreenBlock.brokenImage, 
        //         Is.EqualTo(darkGreenBlock.BrokenImage));
        //     Assert.That(greenBlock.brokenImage, 
        //         Is.EqualTo(greenBlock.BrokenImage));
        //     Assert.That(greyBlock.brokenImage, 
        //         Is.EqualTo(greyBlock.BrokenImage));
        //     Assert.That(orangeBlock.brokenImage, 
        //         Is.EqualTo(orangeBlock.BrokenImage));
        //     Assert.That(purpleBlock.brokenImage, 
        //         Is.EqualTo(purpleBlock.BrokenImage));
        //     Assert.That(redBlock.brokenImage, 
        //         Is.EqualTo(redBlock.BrokenImage));
        //     Assert.That(TealBlock.brokenImage, 
        //         Is.EqualTo(TealBlock.BrokenImage));
        //     Assert.That(yellowBlock.brokenImage, 
        //         Is.EqualTo(yellowBlock.BrokenImage));
            
        // }
    }