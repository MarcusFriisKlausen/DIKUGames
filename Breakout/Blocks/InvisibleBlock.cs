using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;
public class InvisibleBlock : Block {
    public bool Visible;
    private int health;
    private IBaseImage? blockImage;
    private IBaseImage brokenImage;    
    public InvisibleBlock(DynamicShape shape, IBaseImage image, IBaseImage brokenImage) : 
        base(shape, image, brokenImage) {
            this.brokenImage = brokenImage;
            this.Image = null;
            this.CanBeDestroyed = false;
            this.blockImage = image;
            this.Visible = false;
    }

    public void Visiblize() {
        Image = blockImage;
        this.Visible = true;
        CanBeDestroyed = true;
    }

    public override void LoseHealth() {
        if (CanBeDestroyed) {
            this.health = health - 1;
        }
    }
}