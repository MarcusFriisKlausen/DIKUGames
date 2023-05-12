using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout;
public class BlockHardened : Block {
   
    private int maxHealth;
    public int health;
    private IBaseImage brokenImage;
    
    public BlockHardened(DynamicShape shape, IBaseImage image, IBaseImage brokenImage) : base(shape, image, brokenImage) {
        this.brokenImage = brokenImage;
        this.Image = image;
        this.CanBeDestroyed = true;
        this.maxHealth = base.Health + 1;
        this.health = maxHealth;
        this.value = 20;
    }

    //public void IncreaseHealth(int h) {
    //    maxHealth += h;
    //    health = maxHealth;
    //}
    
    
}