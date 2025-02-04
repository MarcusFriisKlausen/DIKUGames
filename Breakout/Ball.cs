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
/// <summary>
/// This class is responsible for initializing a ball and calculating and changing its moving 
/// direction. Additionally it handles collisions with the player and the walls of the game window.
/// It also has a function for decrementing player health, if there is no balls left in the window,
/// and one that makes the ball double sized as a result of a power up.
/// </summary>
public class Ball : Entity, IGameEventProcessor {
    private GameEvent doubleSizeBack;
    private double? doubleSizeTimeStop;
    private bool isDoubleSize = false;
    private float MOVEMENT_SPEED = 0.02f;
    private GameEventBus eventBus;
    public Ball(DynamicShape shape, IBaseImage image) 
    : base(shape, image) {
        Image = base.Image;
        shape.Direction = new Vec2F(MOVEMENT_SPEED * 0f, MOVEMENT_SPEED * 0f);

        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.TimedEvent, this);
    }

    /// <summary>
    /// Sets the initial direction of the ball
    /// <summary>
    private void SetDirection(float x, float y) {
        Shape.AsDynamicShape().Direction = new Vec2F(x, y);
    }

    /// <summary>
    /// Changes the balls direction left, right, up and down when collisions happen
    /// <summary>
    private void ChangeDir(EntityContainer<Block> blockCont, Player p) {
        CollisionHandler.ChangeDirRightLeft(this.Shape.AsDynamicShape(), blockCont);
        CollisionHandler.ChangeDirUpDown(this.Shape.AsDynamicShape(), blockCont);
        CollisionHandler.HandlePaddleCollision(this.Shape.AsDynamicShape(), p.Shape);
        CollisionHandler.HandleWallCollision(this.Shape);
    }

    /// <summary>
    /// Moves the ball
    /// <summary>
    public void Move(EntityContainer<Block> cont, Player p) {
        ChangeDir(cont, p);
        Shape.MoveX(Shape.AsDynamicShape().Direction.X);
        Shape.MoveY(Shape.AsDynamicShape().Direction.Y);
    }

    /// <summary>
    /// Makes the player lose health when a ball leaves the window
    /// <summary>
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

    /// <summary>
    /// Makes the size of the ball double
    /// <summary>
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

    /// <summary>
    /// Processes events related to the ball
    /// <summary>
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

    /// <summary>
    /// Processes timed events related to the ball
    /// <summary>
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
