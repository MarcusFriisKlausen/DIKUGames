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
using Breakout.Blocks;
using Breakout.Effects;
using Breakout.PowerUps;
using Breakout.Blocks.Factory;


namespace TestBlockFactory;
[TestFixture]
public class UnbreakableBlockFactoryTest{

    DynamicShape shape;
    IBaseImage image;
    IBaseImage brokenImage;
    UnbreakableBlockFactory factory;
    [SetUp]
    public void SetUp(){
        factory = new UnbreakableBlockFactory();
        shape = new DynamicShape(new Vec2F((1f/12f), 1f), new Vec2F(1f/12f, 1f/25f));
        image = new Image(Path.Combine("Assets", "Images", "darkgreen-block.png"));
        brokenImage = new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png"));
    }

    [Test]
    public void CreateBlockTest(){
        DynamicShape expShape = shape;
        IBaseImage expImage = image;
        IBaseImage expBrokenImage = brokenImage;
        Block resultBlock = factory.CreateBlock(shape, image, brokenImage);
        Assert.That(expShape, Is.EqualTo(resultBlock.Shape));
        Assert.That(expImage, Is.EqualTo(resultBlock.Image));
        Assert.That(expBrokenImage, Is.EqualTo(resultBlock.BrokenImage));
    }

    [Test]
    public void CreateSpecialBlockTest(){
        NormalBlock normalBlock = new NormalBlock(shape, image, brokenImage);
        Block resultBlock = factory.CreateSpecialBlock(normalBlock);
        DynamicShape expShape = shape;
        IBaseImage expImage = image;
        IBaseImage expBrokenImage = brokenImage;
        Assert.That(expImage, Is.EqualTo(resultBlock.Image));
        Assert.That(expShape, Is.EqualTo(resultBlock.Shape));
        Assert.That(expImage, Is.EqualTo(resultBlock.Image));
        Assert.That(expBrokenImage, Is.EqualTo(resultBlock.BrokenImage));
        Assert.That(normalBlock.GetType(), Is.Not.EqualTo(resultBlock.GetType()));
    }
}