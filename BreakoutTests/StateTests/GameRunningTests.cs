//using NUnit.Framework;
//using Breakout;
//using DIKUArcade.Entities;
//using DIKUArcade.Graphics;
//using DIKUArcade.Math;
//using DIKUArcade.Events;
//using DIKUArcade.Input;
//using DIKUArcade.State;
//using DIKUArcade.Physics;
//using Breakout.Effects;
//using Breakout.Blocks;
//using DIKUArcade.Timers;
//using Breakout.Hazards;
//using Breakout.PowerUps;
//using Breakout.BreakoutStates;
//
//namespace TestStates;
//
//public class GameRunning{
//    GameRunning gameRunning;
//
//    [SetUp]
//    public void SetUp(){
//        DIKUArcade.GUI.Window.CreateOpenGLContext();
//
//        gameRunning = GameRunning.GetInstance();
//
//    }
//
//    [Test]
//    public void TestRemoveEffect() {
//        EntityContainer<BlockEffect> container  = new EntityContainer<BlockEffect>{};
//        ExtraLife powerUp = new ExtraLife((new DynamicShape
//                    (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f))), 
//                    new Image(Path.Combine("Assets", "Images", "LifePickup.png")));
//        container.AddEntity(powerUp);
//        var exp = new EntityContainer<BlockEffect>{};
//        var res = gameRunning.GetInstance().RemoveEffects(container);
//        Assert.That(res, Is.EqualTo(exp));   
//    }
//
//    [Test]
//    public void TestRemoveBlocks() {
//        EntityContainer<Block> container  = new EntityContainer<Block>{};
//        container.AddEntity(new NormalBlock(
//                new DynamicShape(new Vec2F((1f/12f), 1f), new Vec2F(1f/12f, 1f/25f)), 
//                new Image(Path.Combine("Assets", "Images", "yellow-block.png")), 
//                new Image(Path.Combine("Assets", "Images", "yellow-block-damaged.png"))));
//
//        container.DeleteEntity();
//        var exp = new EntityContainer<Block>{};
//        var res = gameRunning.RemoveBlocks(container);
//        Assert.That(res, Is.EqualTo(exp));   
//    }
//
//    [Test]
//    public void TestDoubleSize(){
//        float expectedX = gameRunning.ball.Shape.Extent.X * 2.0f;
//        float expectedY = gameRunning.ball.Shape.Extent.Y * 2.0f;
//        gameRunning.DoubleSize();
//        float resultX = ball.Shape.Extent.X;
//        float resultY = ball.Shape.Extent.Y;
//        Assert.That(resultX, Is.EqualTo(expectedX));
//        Assert.That(resultY, Is.EqualTo(expectedY));
//    }
//
//
//}