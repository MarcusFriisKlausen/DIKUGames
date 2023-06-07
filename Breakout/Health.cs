using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
/// <summary>
/// This class is responsible for rendering the players health, and updating the health when
/// health is either lost or gained.
/// </summary>
public class Health{
    private int value = 3;
    public int Value {
        get { return value;
        }
    }
    public bool Invincible = false;
    private EntityContainer<Entity> activeImages = new EntityContainer<Entity>();
   

    public Health() {
        this.activeImages.AddEntity(
            new Entity(new StationaryShape(
                new Vec2F(0.85f, 0.02f),
                new Vec2F(0.05f, 0.05f)),
                new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
        this.activeImages.AddEntity(
            new Entity(new StationaryShape(
                new Vec2F(0.9f, 0.02f),
                new Vec2F(0.05f, 0.05f)),
                new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
        this.activeImages.AddEntity(
            new Entity(new StationaryShape(
                new Vec2F(0.95f, 0.02f),
                new Vec2F(0.05f, 0.05f)),
                new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
        
    }

    public void RenderHealth() {
            activeImages.RenderEntities();
    }


    public void GainHealth() {
        if (value != 3) {
            value++;
        }
        if (value == 3) {
            activeImages.ClearContainer();
            this.activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.85f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
            this.activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.9f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
            this.activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.95f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
        
        }
        if (value == 2){
            activeImages.ClearContainer();
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.85f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.9f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.95f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
        } else if (value == 1) {
            activeImages.ClearContainer();
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.85f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.9f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.95f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
        } else if (value == 0) {
            activeImages.ClearContainer();
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.85f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.9f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.95f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
        }
    }

    public void LoseHealth(){
        if(!Invincible){
            value--;
        }
        if (value == 2){
            activeImages.ClearContainer();
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.85f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.9f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.95f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
        } else if (value == 1) {
            activeImages.ClearContainer();
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.85f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.9f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.95f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
        } else if (value == 0) {
            activeImages.ClearContainer();
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.85f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.9f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
            activeImages.AddEntity(
                new Entity(new StationaryShape(
                    new Vec2F(0.95f, 0.02f),
                    new Vec2F(0.05f, 0.05f)),
                    new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
        }
    }

}                         