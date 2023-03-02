using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga;
public class Player {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;


    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }
    public void Render() {
        entity.Render();
    }

    private void UpdateDirection(){
        float xDirection = moveLeft + moveRight;
        shape.Direction = new Vec2F(xDirection, 0.0f);

    }
    public void Move() {
    // TODO: move the shape and guard against the window borders
    }
    public void SetMoveLeft(bool val) {
        moveLeft = val ? - MOVEMENT_SPEED : 0.0f;
        UpdateDirection();
    }
    public void SetMoveRight(bool val) {
        moveRight = val ? MOVEMENT_SPEED : 0.0f;
        UpdateDirection();
    }

}
