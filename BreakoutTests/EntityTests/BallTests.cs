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
public class BallTests {
    NormalBlock normalBlock;
    InvisibleBlock invisBlock;
    UnbreakableBlock unbreakableBlock;
    PowerUpBlock powerUpBlock;
    EntityContainer<Block> blockCont;


    Ball ball;
    EntityContainer<Ball> ballCont;

    Player player;

    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();

        var shape = new DynamicShape(new Vec2F((1f/12f), 1f), new Vec2F(1f/12f, 1f/25f));
        var image = new Image(Path.Combine("Assets", "Images", "darkgreen-block.png"));
        var brokenImage = new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png"));
        normalBlock = new NormalBlock(shape, image, brokenImage);
        blockCont = new EntityContainer<Block>();
        blockCont.AddEntity(normalBlock);


        ballCont = new EntityContainer<Ball>();
        ball = new Ball(
            new DynamicShape(new Vec2F(0.485f, 0.15f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));
        ballCont.AddEntity(ball);

        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }

    [TestCase(0.05f)]
    [TestCase(0.5f)]
    [TestCase(1.0f)]
    public void TestLosingHealthFalse(float i){
        ball.Shape.Position.Y = i;
        int exp = 0; //LosingHealth should return 0 as i > 0.0.
        int res = ball.LosingHealth();
        Assert.That(res, Is.EqualTo(exp));
    }

    [TestCase(-0.01f)]
    [TestCase(-0.5f)]
    [TestCase(-1.0f)]
    public void TestLosingHealthTrue(float i){
        ball.Shape.Position.Y = i;
        int exp = 1; //LosingHealth should return 1 as i < 0.0.
        int res = ball.LosingHealth();
        Assert.That(res, Is.EqualTo(exp));
    }
    

    [Test]
    public void TestMoveNoDir(){
        Vec2F expected = ball.Shape.Position;
        ball.Move(blockCont, player);
        Vec2F result = ball.Shape.Position; 
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestDoubleSize(){
        float expectedX = ball.Shape.Extent.X * 2.0f;
        float expectedY = ball.Shape.Extent.Y * 2.0f;
        ball.DoubleSize();
        float resultX = ball.Shape.Extent.X;
        float resultY = ball.Shape.Extent.Y;
        Assert.That(resultX, Is.EqualTo(expectedX));
        Assert.That(resultY, Is.EqualTo(expectedY));
    }
}