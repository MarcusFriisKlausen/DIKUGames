using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Effects;
using Breakout.PowerUps;

namespace Breakout.Blocks;
/// <summary>
/// This class is responsible for creating a power up block and making it drop.
/// </summary>
public class PowerUpBlock : Block {
    public bool Visible;
    private List<BlockEffect> powerUpsLst;
    private IBaseImage brokenImage;    
    public PowerUpBlock(DynamicShape shape, IBaseImage image, IBaseImage brokenImage) : 
        base(shape, image, brokenImage) {
            this.brokenImage = brokenImage;
            this.powerUpsLst = new List<BlockEffect>{
                (new ExtraLife(new DynamicShape
                    (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f)), 
                    new Image(Path.Combine("Assets", "Images", "LifePickup.png")))),
                (new Wide(new DynamicShape
                    (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f)), 
                    new Image(Path.Combine("Assets", "Images", "WidePowerUp.png")))),
                (new MoreTime(new DynamicShape
                    (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f)), 
                    new Image(Path.Combine("Assets", "Images", "clock-up.png")))),
                (new Invincible(new DynamicShape
                    (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f)), 
                    new Image(Path.Combine("Assets", "Images", "ToughenUp.png")))),
                (new DoubleSize(new DynamicShape
                    (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f)), 
                    new Image(Path.Combine("Assets", "Images", "BigPowerUp.png"))))
            };
    }

    public BlockEffect Drop(Block block) {
        int powerUpRand = new Random().Next(powerUpsLst.Count());
        BlockEffect powerUp = powerUpsLst[powerUpRand];
        powerUp.Shape.Position = block.Shape.Position;
        return powerUp;
    }

    public override void DropEffect() {
        IngameEffect = this.Drop(this);        
    }
}