using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.Hazards;
using Breakout.Effects;
using DIKUArcade.Math;

namespace Breakout.Blocks.Factory;
public abstract class BlockFactory{
    public BlockFactory(){}

    public abstract Block CreateBlock(DynamicShape shape, IBaseImage img, IBaseImage brokenImg);

    public virtual Block CreateSpecialBlock(NormalBlock normalBlock) {
        return normalBlock;
    }
}