using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using DIKUArcade.Physics;
using Breakout.BreakoutStates;
using Breakout.Blocks;

namespace Breakout;
public class Ball : Entity, IGameEventProcessor {
    private GameEvent doubleSizeBack;
    private double? doubleSizeTimeStop;
    private bool isDoubleSize = false;
    private float MOVEMENT_SPEED = 0.015f;
    private GameEventBus eventBus;
    public Ball(DynamicShape shape, IBaseImage image) 
    : base(shape, image) {
        Image = base.Image;
        shape.Direction = new Vec2F(MOVEMENT_SPEED * 0f, MOVEMENT_SPEED * 0f);

        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.TimedEvent, this);
    }
    
    private float DirAngle() {
        return (float)(Math.Atan(Shape.AsDynamicShape().Direction.Y 
        / Shape.AsDynamicShape().Direction.X)*(180/Math.PI));
    }

    private Vec2F CalcDir(float angle) {
        return new Vec2F(
            Shape.AsDynamicShape().Direction.X*(float)Math.Cos(((angle*(180/Math.PI))))
            - Shape.AsDynamicShape().Direction.Y*(float)Math.Sin((angle*(180/Math.PI))), 
            Shape.AsDynamicShape().Direction.X*(float)Math.Sin(((angle*(180/Math.PI))))
            + Shape.AsDynamicShape().Direction.Y*(float)Math.Cos((angle*(180/Math.PI)))
        );
    }

    private void SetDirection(float x, float y) {
        Shape.AsDynamicShape().Direction = new Vec2F(x, y);
    }

    private void ChangeDir(EntityContainer<Block> blockCont, Player p) {
        ChangeDirRightLeft(blockCont, p);
        ChangeDirUpDown(blockCont, p);
        HandlePaddleCollision(p);
        HandleWallCollision();
    }

    public void Move(EntityContainer<Block> cont, Player p) {
        ChangeDir(cont, p);
        Shape.MoveX(Shape.AsDynamicShape().Direction.X);
        Shape.MoveY(Shape.AsDynamicShape().Direction.Y);
    }

    // Changes the balls horizontal direction
    private void ChangeDirRightLeft(EntityContainer<Block> cont, Player p) {
        cont.Iterate(ent => {    
            var entShape = ent.Shape;
            var dataEnt = CollisionDetection.Aabb(Shape.AsDynamicShape(), entShape);
            if (dataEnt.Collision){
                if (dataEnt.CollisionDir == CollisionDirection.CollisionDirRight
                    || dataEnt.CollisionDir == CollisionDirection.CollisionDirLeft) {
                        if (ent.CanBeDestroyed) {
                            ent.LoseHealth();
                        }
                        if (ent is InvisibleBlock) {
                            ((InvisibleBlock)ent).Visiblize();
                        }
                        if (ent.Health == 0) {
                            GameRunning.GetInstance().Score.IncrementScore(ent.Value);
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
                        if (ent.CanBeDestroyed) {
                            ent.LoseHealth();
                        }
                        if (ent is InvisibleBlock) {
                            ((InvisibleBlock)ent).Visiblize();
                        }
                        if (ent.Health == 0) {
                            GameRunning.GetInstance().Score.IncrementScore(ent.Value);
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

        var dataPLeftMore = CollisionDetection.Aabb(
            Shape.AsDynamicShape(), new DynamicShape(new Vec2F(
                pShape.Position.X, (pShape.Position.Y - pShape.Position.Y / 10f)), new Vec2F(
                pShape.Extent.X / 4f, pShape.Extent.Y)));

        var dataPLeft = CollisionDetection.Aabb(
            Shape.AsDynamicShape(), new DynamicShape(new Vec2F(
                (pShape.Position.X + p.Shape.Extent.X / 4f), 
                (pShape.Position.Y - pShape.Position.Y / 10f)), 
                new Vec2F(pShape.Extent.X / 4f, pShape.Extent.Y)));

        var dataPRight = CollisionDetection.Aabb(
            Shape.AsDynamicShape(), new DynamicShape(new Vec2F(
                (pShape.Position.X + p.Shape.Extent.X / 2f), 
                (pShape.Position.Y - pShape.Position.Y / 10f)), 
                new Vec2F((pShape.Extent.X / 4f), pShape.Extent.Y)));

        var dataPRightMore = CollisionDetection.Aabb(
            Shape.AsDynamicShape(), new DynamicShape(new Vec2F(
                (pShape.Position.X + (p.Shape.Extent.X / 4f) * 3), 
                (pShape.Position.Y - pShape.Position.Y / 10f)), 
                new Vec2F((pShape.Extent.X / 4f), pShape.Extent.Y)));

                
        if (dataPLeft.Collision) {
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
            if (Shape.AsDynamicShape().Direction.X < 0f) {
                Shape.AsDynamicShape().Direction = 
                    new Vec2F(Shape.AsDynamicShape().Direction.X*(-1), 
                        Shape.AsDynamicShape().Direction.Y*(-1f));
            } else {
                Shape.AsDynamicShape().Direction = 
                    new Vec2F(Shape.AsDynamicShape().Direction.X, 
                        Shape.AsDynamicShape().Direction.Y*(-1));
            }
        } else if (dataPRightMore.Collision) { 
            if (Shape.AsDynamicShape().Direction.X < 0f) {
                Shape.AsDynamicShape().Direction =
                    CalcDir(10f);
            } else {
                if (DirAngle() <= 20f) {
                    Shape.AsDynamicShape().Direction = 
                        CalcDir((float)(Math.PI/2));
                }
                Shape.AsDynamicShape().Direction =   
                    CalcDir((-10f));
            }
        } else if (dataPLeftMore.Collision) {
            // Send bolden mod venstre
            if (Shape.AsDynamicShape().Direction.X >= 0f) {
                Shape.AsDynamicShape().Direction = 
                    CalcDir(10f);
            } else {
                if (DirAngle() <= -20f) {
                    Shape.AsDynamicShape().Direction =
                        CalcDir((float)(Math.PI));
                }
                Shape.AsDynamicShape().Direction = 
                    CalcDir((10f));
            }
        }
    }

    public int LosingHealth(){
        if (Shape.Position.Y < 0.0){
            Shape.AsDynamicShape().Direction.X = 0f;
            Shape.AsDynamicShape().Direction.Y = 0f;
            return 1;
        }
        else {
            return 0;
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

    public void DoubleSize() {
        GameEvent doubleSize = new GameEvent();
            doubleSize.EventType = GameEventType.TimedEvent;
            doubleSize.Message = "doubleSize";
            BreakoutBus.GetBus().RegisterEvent(doubleSize);
        ProcessEvent(doubleSize);

        doubleSizeBack = new GameEvent();
            doubleSizeBack.EventType = GameEventType.TimedEvent;
            doubleSizeBack.Message = "doubleSizeBack";
            BreakoutBus.GetBus().RegisterEvent(doubleSizeBack);
        doubleSizeTimeStop = StaticTimer.GetElapsedSeconds() + 10;
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.Message == KeyboardKey.Space.ToString()) {
            if (Shape.AsDynamicShape().Direction.X == 0f 
                && Shape.AsDynamicShape().Direction.Y == 0f) {
                    StaticTimer.ResumeTimer();
                    SetDirection(MOVEMENT_SPEED * 0.1f, MOVEMENT_SPEED * 1f);
            }
        }
        else if (gameEvent.Message == "doubleSize") {
            if (!isDoubleSize) {
                this.Shape.Extent = new Vec2F(this.Shape.Extent.X*2, this.Shape.Extent.Y*2);
            } 
            isDoubleSize = true;
        }
        else if (gameEvent.Message == "doubleSizeBack") {
            this.Shape.Extent = new Vec2F(this.Shape.Extent.X*0.5f, this.Shape.Extent.Y*0.5f);
            isDoubleSize = false;
        }
    }

    public void ProcessTimedEvents() {
        if (doubleSizeTimeStop is not null) {
            if ((int)doubleSizeTimeStop == (int)StaticTimer.GetElapsedSeconds()) {
                if (isDoubleSize) {
                    ProcessEvent(doubleSizeBack);
                }
            }
        }
    }
}
