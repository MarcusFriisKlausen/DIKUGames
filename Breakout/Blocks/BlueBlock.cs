using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks{
    public class BlueBlock : Entity, IBlock {
        IBaseImage image = new Image(Path.Combine("Assets", "Images", "blue-block.png"));
        IBaseImage brokenImage = new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png"));

        public BlueBlock(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
                
            }
    }
}