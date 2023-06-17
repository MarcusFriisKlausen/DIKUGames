using NUnit.Framework;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Physics;
using Breakout.Effects;
using Breakout.Blocks;
using DIKUArcade.Timers;
using Breakout.Hazards;
using Breakout.PowerUps;
using Breakout.BreakoutStates;

namespace TestStates;

public class GameRunningTest{
   GameRunning gameRunning;

   [SetUp]
   public void SetUp(){
       DIKUArcade.GUI.Window.CreateOpenGLContext();
       gameRunning = GameRunning.GetInstance();
   }

   [Test]
   public void TestRemoveEffect() {
       EntityContainer<BlockEffect> container  = new EntityContainer<BlockEffect>{};
       ExtraLife powerUp = new ExtraLife((new DynamicShape
                   (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f))), 
                   new Image(Path.Combine("Assets", "Images", "LifePickup.png")));
       container.AddEntity(powerUp);

        container.Iterate(block => {
            block.DeleteEntity(); 
        });

       var exp = new EntityContainer<BlockEffect>{};
       var res = gameRunning.RemoveEffects(container);
       Assert.That(res, Is.EqualTo(exp));   
   }

   [Test]
   public void TestRemoveBlocks() {
       EntityContainer<Block> container  = new EntityContainer<Block>{};
       container.AddEntity(new NormalBlock(
               new DynamicShape(new Vec2F((1f/12f), 1f), new Vec2F(1f/12f, 1f/25f)), 
               new Image(Path.Combine("Assets", "Images", "yellow-block.png")), 
               new Image(Path.Combine("Assets", "Images", "yellow-block-damaged.png"))));

       
        container.Iterate(block => {
            block.DeleteEntity(); 
        });

       var exp = new EntityContainer<Block>{};
       var res = gameRunning.RemoveBlocks(container);
       Assert.That(res, Is.EqualTo(exp));   
   }

   [Test]
   public void TestDoubleSize(){
       float expectedX = gameRunning.Ball.Shape.Extent.X * 2.0f;
       float expectedY = gameRunning.Ball.Shape.Extent.Y * 2.0f;
       gameRunning.DoubleSize();
       float resultX = gameRunning.Ball.Shape.Extent.X;
       float resultY = gameRunning.Ball.Shape.Extent.Y;
       Assert.That(resultX, Is.EqualTo(expectedX));
       Assert.That(resultY, Is.EqualTo(expectedY));
   }

    [Test]
    public void TestRemoveBall(){
        var ball = gameRunning.Ball;
        ball.DeleteEntity();
        gameRunning.UpdateState();

        var exp = (new EntityContainer<Block>{}).CountEntities();
        var res = gameRunning.BallCont.CountEntities();
        Assert.That(res, Is.EqualTo(exp));
    }

    [Test]
    public void TestPlayerConstruction(){
        var player = gameRunning.Player;

        var expExtentX = player.Shape.Extent.X;
        var resExtentX = 0.15f;
        var expExtentY = player.Shape.Extent.Y;
        var resExtentY = 0.02f;
        Assert.That(resExtentX, Is.EqualTo(expExtentX));
        Assert.That(resExtentY, Is.EqualTo(expExtentY));

        var expPosX = player.Shape.Position.X;
        var resPosX = 0.425f;
        var expPosY = player.Shape.Position.Y;
        var resPosY = 0.1f;
        Assert.That(resPosX, Is.EqualTo(expPosX));
        Assert.That(resPosY, Is.EqualTo(expPosY));
    }

    [Test]
    public void TestHealthGameOver() {
        var player = gameRunning.Player;
        player.Health.LoseHealth();
        player.Health.LoseHealth();
        player.Health.LoseHealth();
        gameRunning.UpdateState();
    }

}