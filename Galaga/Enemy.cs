using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private Vec2F startPos;
    private bool isMovingLeft = false;
    public bool IsMovingLeft {
        get {return isMovingLeft;}
    }
    public Vec2F StartPos {
        get {return startPos;}
    }
    private float movement_speed = 0.0003f;
    public float MOVEMENT_SPEED{
        get {return movement_speed;}
    }
    private const int max_hitpoints = 4;
    public int Max_hitpoints {
        get {
            return max_hitpoints;
        }
    }
    private int hitpoints = max_hitpoints; 
    public int Hitpoints {
        get {
            return hitpoints;
        }
        set {
            hitpoints = value;
        }
    }
    private bool isEnraged = false;
    public bool IsEnraged {
        get {
            return isEnraged;
        }
    }
    List<Image> enemyStridesRed;
    public Enemy(DynamicShape shape, IBaseImage image) 
        : base(shape, image) {
            Image = base.Image;
            startPos = this.Shape.Position;
        }
    public void Enrage(List<Image> EnrageStride) {
        isEnraged = true;
        enemyStridesRed = EnrageStride;
        Image = new ImageStride(80, enemyStridesRed);
        movement_speed = movement_speed * 3.0f; 
    }
    public void IncreaseMS() {
        movement_speed += 0.0001f;
    }

    /// <summary>
    /// Swaps the direction of the enemy. 
    /// Is used as a means of changing direction when the enemy hits a wall 
    /// in the ZigZagDown Movement Strategy.
    /// </summary>
    /// <returns>
    /// Void.
    /// </returns>
    public void SwapDirection() {
        if (!isMovingLeft) {
            isMovingLeft = true;
        } else {
            isMovingLeft = false;
        }
    }
}