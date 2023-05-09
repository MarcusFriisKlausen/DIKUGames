using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;
public class PurpleBlock : Entity, IBlock {
    private IBaseImage BrokenImage;
    public IBaseImage image { get { return this.Image; } }
    public IBaseImage brokenImage { get { return this.BrokenImage; } }
    public PurpleBlock(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Image = new Image(Path.Combine("Assets", "Images", "purple-block.png"));
        this.BrokenImage = new Image(Path.Combine("Assets", "Images", "purple-block-damaged.png"));
    }
}