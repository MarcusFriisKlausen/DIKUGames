using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks.Factory;

public class UnbreakableBlockFactory : BlockFactory {
    public UnbreakableBlockFactory() {}

    public override Block CreateBlock(DynamicShape shape, IBaseImage img, IBaseImage brokenImg) {
        UnbreakableBlock block = new UnbreakableBlock(shape, img, brokenImg);
        return block;
    }

    public override Block CreateSpecialBlock(NormalBlock normalBlock) {
        UnbreakableBlock specialBlock = 
            new UnbreakableBlock(
                normalBlock.Shape.AsDynamicShape(), normalBlock.Image, normalBlock.BrokenImage
            );
        return specialBlock;
    }
}