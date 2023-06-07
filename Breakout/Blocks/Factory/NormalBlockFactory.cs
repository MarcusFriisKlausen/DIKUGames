using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks.Factory;
/// <summary>
/// This class is responsible for creating a factory for normal blocks.
/// </summary>
public class NormalBlockFactory : BlockFactory {
    public NormalBlockFactory() {
    }

    public override Block CreateBlock(DynamicShape shape, IBaseImage img, IBaseImage brokenImg) {
        NormalBlock block = new NormalBlock(shape, img, brokenImg);
        return block;
    }
}