using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;

namespace Breakout;
public class Health{
    public bool gameOver = false;
    private int value = 3;
    public int Value {
        get {return value;}
    }
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

    public void LoseHealth(){
        value--;
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
        Console.WriteLine(value);
    }
}                         