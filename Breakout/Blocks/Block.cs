using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.Hazards;
using Breakout.Effects;
using DIKUArcade.Math;

namespace Breakout;
public abstract class Block : Entity{
    public bool CanBeDestroyed {get; set;}
    public int Health {get { return this.health; } set {}}
    private int maxHealth;
    private int health;
    private int value;
    public int Value { get {return value; } }
    private HazardDrop drops;
    public BlockEffect? ingameEffect;
    private IBaseImage brokenImage;
    public IBaseImage BrokenImage { get { return brokenImage; } set {}}
    public Block(DynamicShape shape, IBaseImage? image, IBaseImage brokenImage) : 
        base(shape, image) {
            this.value = 10;
            this.brokenImage = brokenImage;
            this.Image = image;
            this.maxHealth = 1;
            this.health = this.maxHealth;
            this.CanBeDestroyed = true;

            drops = new HazardDrop();

            drops.AddHazards(
                new List<BlockEffect>{
                    (new LoseLife(new DynamicShape
                    (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f)), 
                    new Image(Path.Combine("Assets", "Images", "LoseLife.png"))))
                }
            );
            drops.AddHazards(
                new List<BlockEffect>{
                    (new ReduceTime(new DynamicShape
                    (new Vec2F(0f, 0f), new Vec2F(0.03f, 0.03f)), 
                    new Image(Path.Combine("Assets", "Images", "clock-down.png"))))
                }
            );
    }

    private void ToBrokenBlock(){
        if (health == maxHealth/2){
            this.Image = this.brokenImage;
        }
    }

    public virtual void DropEffect() {
        drops.SetRandomInt();
        if (drops.rand == 4) {
            ingameEffect = drops.Drop(this);
        }          
    }

    public virtual void LoseHealth() {
        if (CanBeDestroyed) {
            this.health = health - 1;
            if (this.health == 0) {
                DropEffect();
            }
            ToBrokenBlock();
        }
    }
}