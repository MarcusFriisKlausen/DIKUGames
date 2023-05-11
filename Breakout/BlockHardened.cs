using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout;
public class BlockHardened : Block {
    public bool CanBeDestoyed {get;}
    public int MaxHealth {get { return this.maxHealth; }}
    public int Health {get { return this.health; } set {}}
    private int maxHealth;
    private int health;
    private IBaseImage brokenImage;
    public IBaseImage Image { get { return Image; } set {}}
    public IBaseImage BrokenImage { get { return brokenImage; } set {}}
    public BlockHardened(DynamicShape shape, IBaseImage image, IBaseImage brokenImage) : base(shape, image, brokenImage) {
        this.brokenImage = brokenImage;
        this.Image = image;
        this.maxHealth = 4;
        this.health = this.maxHealth;
        this.CanBeDestoyed = true;
    }

    public void ToBrokenBlock(){
            if (Health == (MaxHealth/2)){
                this.Image = this.BrokenImage;
            }
        }
    public void LoseHealth() {
        Health --;
    }
}