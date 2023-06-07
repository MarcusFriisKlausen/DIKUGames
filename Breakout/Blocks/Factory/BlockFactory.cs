using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks.Factory;
/// <summary>
/// This class creates a block factory.
/// </summary>
public abstract class BlockFactory{
    public BlockFactory() {
    }

    public abstract Block CreateBlock(DynamicShape shape, IBaseImage img, IBaseImage brokenImg);

    public virtual Block CreateSpecialBlock(NormalBlock normalBlock) {
        return normalBlock;
    }
}