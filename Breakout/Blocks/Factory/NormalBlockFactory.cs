using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks.Factory;

public class NormalBlockFactory : BlockFactory {
    public NormalBlockFactory() {}

    public override Block CreateBlock(DynamicShape shape, IBaseImage img, IBaseImage brokenImg) {
        NormalBlock block = new NormalBlock(shape, img, brokenImg);
        return block;
    }
}