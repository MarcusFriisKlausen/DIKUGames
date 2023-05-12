using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout;
public class BlockUnbreakable : Block {
    private int maxHealth;
    private int health;
    private IBaseImage brokenImage;
    public BlockUnbreakable(DynamicShape shape, IBaseImage image, IBaseImage brokenImage) : base(shape, image, brokenImage) {
        this.brokenImage = brokenImage;
        this.Image = image;
        this.maxHealth = 1;
        this.health = this.maxHealth;
        this.CanBeDestroyed = false;
    }
    
    
    
}  