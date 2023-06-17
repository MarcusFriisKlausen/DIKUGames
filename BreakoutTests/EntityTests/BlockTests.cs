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


namespace TestBlocks;
[TestFixture]
public class BlockTests {
    NormalBlock normalBlock;
    InvisibleBlock invisBlock;
    UnbreakableBlock unbreakableBlock;
    PowerUpBlock powerUpBlock;

    [SetUp]
    public void SetUp(){
        var shape = new DynamicShape(new Vec2F((1f/12f), 1f), new Vec2F(1f/12f, 1f/25f));
        var image = new Image(Path.Combine("Assets", "Images", "darkgreen-block.png"));
        var brokenImage = new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png"));
        normalBlock = new NormalBlock(shape, image, brokenImage);
        invisBlock = new InvisibleBlock(shape, image, brokenImage);
        unbreakableBlock = new UnbreakableBlock(shape, image, brokenImage);
        powerUpBlock = new PowerUpBlock(shape, image, brokenImage);
    }


    [Test]
    public void TestValue() {
        int expected = 10;
        int actualNormal = normalBlock.Value;
        Assert.That(actualNormal, Is.EqualTo(expected));

        int actualInvis = invisBlock.Value;
        Assert.That(actualInvis, Is.EqualTo(expected));

        int actualPowerUp = powerUpBlock.Value;
        Assert.That(actualPowerUp, Is.EqualTo(expected));
    }

    [Test]
    public void TestHealth() {
        int expected = 1;
        int actualNormal = normalBlock.Health;
        Assert.That(actualNormal, Is.EqualTo(expected));

        int actualInvis = invisBlock.Health;
        Assert.That(actualInvis, Is.EqualTo(expected));

        int actualPowerUp = powerUpBlock.Health;
        Assert.That(actualPowerUp, Is.EqualTo(expected));
    }
    
    [Test]
    public void TestLoseHealthTrueNormal() {
        int expected = 0; // health: 1 - 1 = 0
        normalBlock.LoseHealth();
        int actual = normalBlock.Health;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestLoseHealthTruePowerUp() {
        int expected = 0; // health: 1 - 1 = 0
        powerUpBlock.LoseHealth();
        int actual = powerUpBlock.Health;
        Assert.That(actual, Is.EqualTo(expected));
    }


    [Test]
    public void TestIngameEffectNull() {
        BlockEffect? expected = null;
        BlockEffect? actualNormal = normalBlock.IngameEffect;
        Assert.That(actualNormal, Is.EqualTo(expected));

        BlockEffect? actualInvis = invisBlock.IngameEffect;
        Assert.That(actualInvis, Is.EqualTo(expected));

        BlockEffect? actualPowerUp = powerUpBlock.IngameEffect;
        Assert.That(actualPowerUp, Is.EqualTo(expected));
    }
    
    [Test]
    public void TestLoseHealthTrueUnbreakable() {
        int expected = 1;
        unbreakableBlock.LoseHealth();
        int actual = unbreakableBlock.Health;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestLoseHealthTrueInvisible() {
        int expected = 1;
        invisBlock.LoseHealth();
        int actual = invisBlock.Health;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestVisiblize(){
        var exp = true;
        invisBlock.Visiblize();
        var resVis = invisBlock.Visible;
        var resDest = invisBlock.CanBeDestroyed;
        Assert.That(resVis & resDest, Is.EqualTo(exp));
    }
}