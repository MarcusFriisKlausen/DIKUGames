using DIKUArcade.Graphics;
using DIKUArcade.Entities;
namespace Breakout.Blocks.Factory;

public class InvisibleBlockFactory : BlockFactory {
    public InvisibleBlockFactory() {}

    public override Block CreateBlock(DynamicShape shape, IBaseImage img, IBaseImage brokenImg) {
        InvisibleBlock block = new InvisibleBlock(shape, img, brokenImg);
        return block;
    }

    public override Block CreateSpecialBlock(NormalBlock normalBlock) {
        InvisibleBlock specialBlock = 
            new InvisibleBlock(
                normalBlock.Shape.AsDynamicShape(), normalBlock.Image, normalBlock.BrokenImage
            );
        return specialBlock;
    }
}