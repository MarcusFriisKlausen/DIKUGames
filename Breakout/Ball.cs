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
    private Vec2F movementVec;
    public Ball(DynamicShape shape, IBaseImage image) 
    : base(shape, image) {
        Image = base.Image;
        shape.Direction = new Vec2F(MOVEMENT_SPEED * 0.5f, MOVEMENT_SPEED* 1f);
        movementVec = shape.Direction;
    }

    public EntityContainer<Entity> RemoveEntities(EntityContainer<Block> container){
        var count = container.CountEntities();
        EntityContainer<Entity> newCont = new EntityContainer<Entity>(count);
        foreach (Entity ent in container) {
            if (!ent.IsDeleted()) {
                newCont.AddEntity(ent);
            }
        }
        return newCont;
    }

    private void ChangeDir(EntityContainer<Block> cont, Player p) {
        ChangeDirRightLeft(cont, p);
        ChangeDirUpDown(cont, p);

        RemoveEntities(cont);

        var pShape = p.Shape;
        var dataPLeft = CollisionDetection.Aabb(
            Shape.AsDynamicShape(), new DynamicShape(new Vec2F(
                pShape.Position.X, (pShape.Position.Y + pShape.Position.Y / 2f)), new Vec2F(
                pShape.Extent.X / 2f, pShape.Extent.Y)));
        var dataPRight = CollisionDetection.Aabb(
            Shape.AsDynamicShape(), new DynamicShape(new Vec2F(
                (pShape.Position.X + p.Shape.Extent.X / 2f), 
                (pShape.Position.Y + pShape.Position.Y / 2f)), 
                new Vec2F((pShape.Extent.X / 2f), pShape.Extent.Y)));
                
        if (dataPLeft.Collision == true){
            // Send mod venstre
            if (movementVec.X > 0f) {
                movementVec = new Vec2F(movementVec.X*(-1), movementVec.Y*(-1f));
            } else {
                movementVec = new Vec2F(movementVec.X, movementVec.Y*(-1f));
            }
        } else if (dataPRight.Collision == true) { 
            if (movementVec.X < 0f){
                movementVec = new Vec2F(movementVec.X*(-1), movementVec.Y*(-1f));
            } else {
                movementVec = new Vec2F(movementVec.X, movementVec.Y*(-1));
            }
        }
    }

    public void Move(EntityContainer<Block> cont, Player p) {
        ChangeDir(cont, p);
        Shape.MoveX(movementVec.X);
        Shape.MoveY(movementVec.Y);
    }

    // Changes the balls horizontal direction
    private void ChangeDirRightLeft(EntityContainer<Block> cont, Player p) {
        cont.Iterate(ent => {    
            var entShape = ent.Shape;
            var dataEnt = CollisionDetection.Aabb(Shape.AsDynamicShape(), entShape);
            if (dataEnt.Collision){
                if (dataEnt.CollisionDir == CollisionDirection.CollisionDirRight
                    || dataEnt.CollisionDir == CollisionDirection.CollisionDirLeft) {
                        if (ent.CanBeDestoyed){
                            ent.LoseHealth();
                        }
                        if (ent.Health == 0) {    
                            ent.DeleteEntity();
                        }
                        Shape.AsDynamicShape().Direction = new Vec2F(
                            Shape.AsDynamicShape().Direction.X*(-1), 
                            Shape.AsDynamicShape().Direction.Y);
                }
            }    
        });
    }

    // Changes the balls vertical direction 
    private void ChangeDirUpDown(EntityContainer<Block> cont, Player p) {
        cont.Iterate(ent => {    
            var entShape = ent.Shape;
            var dataEnt = CollisionDetection.Aabb(Shape.AsDynamicShape(), entShape);
            if (dataEnt.Collision){
                if (dataEnt.CollisionDir == CollisionDirection.CollisionDirUp
                    || dataEnt.CollisionDir == CollisionDirection.CollisionDirDown) {
                        if (ent.CanBeDestoyed){
                            ent.LoseHealth();
                        }
                        if (ent.Health == 0) {
                            ent.DeleteEntity();
                        }
                        Shape.AsDynamicShape().Direction = new Vec2F(
                            Shape.AsDynamicShape().Direction.X, 
                            Shape.AsDynamicShape().Direction.Y*(-1));
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
                
        if (dataPLeft.Collision){
            // Send bolden mod venstre
            if (Shape.AsDynamicShape().Direction.X >= 0f) {
                Shape.AsDynamicShape().Direction = 
                new Vec2F(Shape.AsDynamicShape().Direction.X*(-1), 
                Shape.AsDynamicShape().Direction.Y*(-1f));
            } else {
                Shape.AsDynamicShape().Direction = 
                new Vec2F(Shape.AsDynamicShape().Direction.X, 
                Shape.AsDynamicShape().Direction.Y*(-1f));
            }
            // Send bolden mod højre
        } else if (dataPRight.Collision) { 
            if (Shape.AsDynamicShape().Direction.X < 0f){
                Shape.AsDynamicShape().Direction = 
                new Vec2F(Shape.AsDynamicShape().Direction.X*(-1), 
                Shape.AsDynamicShape().Direction.Y*(-1f));
            } else {
                Shape.AsDynamicShape().Direction = 
                new Vec2F(Shape.AsDynamicShape().Direction.X, 
                Shape.AsDynamicShape().Direction.Y*(-1));
            }
        }
    }

    // Makes sure the ball is unable to leave the game window
    private void HandleWallCollision() {
        if (Shape.Position.X <= 0.0f || Shape.Position.X >= 1.0f - Shape.Extent.X) {
            Shape.AsDynamicShape().Direction = 
            new Vec2F(Shape.AsDynamicShape().Direction.X*(-1), Shape.AsDynamicShape().Direction.Y);
        }
        if (Shape.Position.Y >= 1.0f - Shape.Extent.Y) {
            Shape.AsDynamicShape().Direction = 
            new Vec2F(Shape.AsDynamicShape().Direction.X, Shape.AsDynamicShape().Direction.Y*(-1));
        }
    }
}
