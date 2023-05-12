using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout;
public class Block : Entity, IBlock {
    public bool CanBeDestroyed {get; set;}
    public int Health {get { return this.health; } set {}}
    private int maxHealth;
    private int health;
    public int value = 10;
    public int Value { get {return value; } }
    private IBaseImage brokenImage;

    public IBaseImage BrokenImage { get { return brokenImage; } set {}}
    public Block(DynamicShape shape, IBaseImage image, IBaseImage brokenImage) : base(shape, image) {
        this.brokenImage = brokenImage;
        this.Image = image;
        this.maxHealth = 1;
        this.health = this.maxHealth;
        this.CanBeDestroyed = true;
    }

    private void ToBrokenBlock(){
        if (health == maxHealth/2){
            this.Image = this.brokenImage;
        }
    }
    public void LoseHealth() {
        if (CanBeDestroyed){
            this.health = health - 1;
            ToBrokenBlock();
        }
        
    }
}