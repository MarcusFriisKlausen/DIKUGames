using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Hazards;
using Breakout.Effects;
using DIKUArcade.Math;

namespace Breakout.Blocks;
public class NormalBlock : Block {
    private IBaseImage? blockImage;
    private IBaseImage brokenImage;    
    public NormalBlock(DynamicShape shape, IBaseImage? image, IBaseImage brokenImage) : 
        base(shape, image, brokenImage) {
            this.brokenImage = brokenImage;
            this.Image = image;
            this.blockImage = image;
    }

    public InvisibleBlock ToInvisible() {
        return new InvisibleBlock(Shape.AsDynamicShape(), Image, brokenImage);
    }

    public UnbreakableBlock ToUnbreakable() {
        return new UnbreakableBlock(Shape.AsDynamicShape(), Image, brokenImage);
    }

    public PowerUpBlock ToPowerUp() {
        return new PowerUpBlock(Shape.AsDynamicShape(), Image, brokenImage);
    }
}