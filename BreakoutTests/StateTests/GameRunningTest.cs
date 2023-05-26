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

public class GameRunning{
    GameRunning gameRunning;

    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        gameRunning = new GameRunning();
    }

    // [Test]
    // public void TestRemoveEffect() {
    //     EntityContainer<BlockEffect> container  = new EntityContainer<BlockEffect>{};
    //     container.AddEntity((new ExtraLife(new DynamicShape
    //                 (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f)), 
    //                 new Image(Path.Combine("Assets", "Images", "LifePickup.png")))));
// 
    //     container.DeleteEntity();
    //     var exp = new EntityContainer<BlockEffect>{};
    //     var res = gameRunning.RemoveEffects(container);
    //     Assert.That(res, Is.EqualTo(exp));   
    // }
// 
    // [Test]
    // public void TestRemoveBlocks() {
    //     EntityContainer<Block> container  = new EntityContainer<Block>{};
    //     container.AddEntity(new NormalBlock(
    //             new DynamicShape(new Vec2F((1f/12f), 1f), new Vec2F(1f/12f, 1f/25f)), 
    //             new Image(Path.Combine("Assets", "Images", "yellow-block.png")), 
    //             new Image(Path.Combine("Assets", "Images", "yellow-block-damaged.png"))));
// 
    //     container.DeleteEntity();
    //     var exp = new EntityContainer<Block>{};
    //     var res = gameRunning.RemoveBlocks(container);
    //     Assert.That(res, Is.EqualTo(exp));   
    // }
}