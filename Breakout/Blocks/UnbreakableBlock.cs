using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;
public class UnbreakableBlock : Block {
    private IBaseImage brokenImage;    
    public UnbreakableBlock(DynamicShape shape, IBaseImage image, IBaseImage brokenImage) : 
        base(shape, image, brokenImage) {
            this.brokenImage = brokenImage;
            this.Image = image;
            this.CanBeDestroyed = false;
    }
}