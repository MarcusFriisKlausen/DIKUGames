using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;
/// <summary>
/// This class is responsible for creating an invisible block. it also contains functions for 
/// making it visible again and lose health.
/// </summary>
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

    /// <summary>
    /// Makes the block visible by setting the bool to be true
    /// <summary>
    public void Visiblize() {
        Image = blockImage;
        this.Visible = true;
        CanBeDestroyed = true;
    }

    /// <summary>
    /// Decreases health by one
    /// <summary>
    public override void LoseHealth() {
        if (CanBeDestroyed) {
            this.health = health - 1;
        }
    }
}