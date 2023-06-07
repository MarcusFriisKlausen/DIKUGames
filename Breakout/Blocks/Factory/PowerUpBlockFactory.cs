using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks.Factory;
/// <summary>
/// This class is responsible for creating a factory for power up blocks.
/// </summary>
public class PowerUpBlockFactory : BlockFactory {
    public PowerUpBlockFactory() {
    }

    public override Block CreateBlock(DynamicShape shape, IBaseImage img, IBaseImage brokenImg) {
        PowerUpBlock block = new PowerUpBlock(shape, img, brokenImg);
        return block;
    }

    public override Block CreateSpecialBlock(NormalBlock normalBlock) {
        PowerUpBlock specialBlock = 
            new PowerUpBlock(
                normalBlock.Shape.AsDynamicShape(), normalBlock.Image, normalBlock.BrokenImage
            );
        return specialBlock;
    }
}