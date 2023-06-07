using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Effects;
public abstract class BlockEffect : Entity {
    private const float MOVEMENT_SPEED = 0.005f;
    public BlockEffect(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Image = image;
        this.Shape = shape;
        Shape.AsDynamicShape().Direction.Y = -MOVEMENT_SPEED;
    }

    public void Move() {
        Shape.MoveY(Shape.AsDynamicShape().Direction.Y); 
    }

    public abstract void Effect(Player p);
}