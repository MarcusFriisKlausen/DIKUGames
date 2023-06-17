using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;
/// <summary>
/// This class is responsible for creating an unbreakable block.
/// </summary>
public class UnbreakableBlock : Block {
    public UnbreakableBlock(DynamicShape shape, IBaseImage image, IBaseImage brokenImage) : 
        base(shape, image, brokenImage) {
            this.brokenImage = brokenImage;
            this.Image = image;
            this.CanBeDestroyed = false;
    }
}