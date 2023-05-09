using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using System;


namespace Breakout;
public class Ball : Entity {
    private float MOVEMENT_SPEED = 0.01f;
    // private Vec2F Shape.AsDynamicShape().Direction;
    public Ball(DynamicShape shape, IBaseImage image) 
    : base(shape, image) {
        Image = base.Image;
        shape.Direction = new Vec2F(MOVEMENT_SPEED * 0.5f, MOVEMENT_SPEED* 1f);
        // Shape.AsDynamicShape().Direction = shape.Direction;
    }

    public EntityContainer<Entity> RemoveEntities(EntityContainer<Entity> container){
            var count = container.CountEntities();
            EntityContainer<Entity> newCont = new EntityContainer<Entity>(count);

            foreach (Entity ent in container) {
                if (!ent.IsDeleted()) {
                    newCont.AddEntity(ent);
                }
            }
            return newCont;
        }

    private void ChangeDir(EntityContainer<Entity> cont, Player p) {
        ChangeDirRightLeft(cont, p);
        ChangeDirUpDown(cont, p);

        RemoveEntities(cont);

        HandlePaddleCollision(p);
        HandleWallCollision();
    }

    public void Move(EntityContainer<Entity> cont, Player p) {
        ChangeDir(cont, p);
        Shape.MoveX(Shape.AsDynamicShape().Direction.X);
        Shape.MoveY(Shape.AsDynamicShape().Direction.Y);
    }

    // Changes the balls horizontal direction
    private void ChangeDirRightLeft(EntityContainer<Entity> cont, Player p) {
        cont.Iterate(ent => {    
            var entShape = ent.Shape;
            var dataEnt = CollisionDetection.Aabb(Shape.AsDynamicShape(), entShape);
            if (dataEnt.Collision){
                if (dataEnt.CollisionDir == CollisionDirection.CollisionDirRight
                    || dataEnt.CollisionDir == CollisionDirection.CollisionDirLeft) {
                    Console.WriteLine(dataEnt.CollisionDir);
                    ent.DeleteEntity();
                    Shape.AsDynamicShape().Direction = new Vec2F(Shape.AsDynamicShape().Direction.X*(-1), Shape.AsDynamicShape().Direction.Y);
                }
            }    
        });
    }

    // Changes the balls vertical direction 
    private void ChangeDirUpDown(EntityContainer<Entity> cont, Player p) {
        cont.Iterate(ent => {    
            var entShape = ent.Shape;
            var dataEnt = CollisionDetection.Aabb(Shape.AsDynamicShape(), entShape);
            if (dataEnt.Collision){
                if (dataEnt.CollisionDir == CollisionDirection.CollisionDirUp
                    || dataEnt.CollisionDir == CollisionDirection.CollisionDirDown) {
                    Console.WriteLine(dataEnt.CollisionDir);
                    ent.DeleteEntity();
                    Shape.AsDynamicShape().Direction = new Vec2F(Shape.AsDynamicShape().Direction.X, Shape.AsDynamicShape().Direction.Y*(-1));
                } 
            }    
        });
    }

    private void HandlePaddleCollision(Player p) {
        var pShape = p.Shape;
        var dataPLeft = CollisionDetection.Aabb(
            Shape.AsDynamicShape(), new DynamicShape(new Vec2F(
                pShape.Position.X, (pShape.Position.Y - pShape.Position.Y / 10f)), new Vec2F(
                pShape.Extent.X / 2f, pShape.Extent.Y)));
        var dataPRight = CollisionDetection.Aabb(

            Shape.AsDynamicShape(), new DynamicShape(new Vec2F(
                (pShape.Position.X + p.Shape.Extent.X / 2f), 
                (pShape.Position.Y - pShape.Position.Y / 10f)), 
                new Vec2F((pShape.Extent.X / 2f), pShape.Extent.Y)));
                
        if (dataPLeft.Collision == true){
            // Send bolden mod venstre
            if (Shape.AsDynamicShape().Direction.X >= 0f) {
                Shape.AsDynamicShape().Direction = new Vec2F(Shape.AsDynamicShape().Direction.X*(-1), Shape.AsDynamicShape().Direction.Y*(-1f));
            } else {
                Shape.AsDynamicShape().Direction = new Vec2F(Shape.AsDynamicShape().Direction.X, Shape.AsDynamicShape().Direction.Y*(-1f));
            }
            // Send bolden mod højre
        } else if (dataPRight.Collision == true) { 
            if (Shape.AsDynamicShape().Direction.X < 0f){
                Shape.AsDynamicShape().Direction = new Vec2F(Shape.AsDynamicShape().Direction.X*(-1), Shape.AsDynamicShape().Direction.Y*(-1f));
            } else {
                Shape.AsDynamicShape().Direction = new Vec2F(Shape.AsDynamicShape().Direction.X, Shape.AsDynamicShape().Direction.Y*(-1));
            }
        }
    }

    // Makes sure the ball is unable to leave the game window
    private void HandleWallCollision() {
        if (Shape.Position.X <= 0.0f || Shape.Position.X >= 1.0f - Shape.Extent.X) {
            Shape.AsDynamicShape().Direction = new Vec2F(Shape.AsDynamicShape().Direction.X*(-1), Shape.AsDynamicShape().Direction.Y);
        }
        if (Shape.Position.Y >= 1.0f - Shape.Extent.Y) {
            Shape.AsDynamicShape().Direction = new Vec2F(Shape.AsDynamicShape().Direction.X, Shape.AsDynamicShape().Direction.Y*(-1));
        }
    }
}