using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;
/// <summary>
/// This class is responsible for creating a normal block. It also contains functions for 
/// converting a normal block to a hazard block and a power up block.
/// </summary>
public class NormalBlock : Block {
    private IBaseImage? blockImage;
    private IBaseImage brokenImage;    
    public NormalBlock(DynamicShape shape, IBaseImage? image, IBaseImage brokenImage) : 
        base(shape, image, brokenImage) {
            this.brokenImage = brokenImage;
            this.Image = image;
            this.blockImage = image;
    }
}