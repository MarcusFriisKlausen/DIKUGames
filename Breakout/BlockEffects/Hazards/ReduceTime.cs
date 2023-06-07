using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;

namespace Breakout.Hazards;
/// <summary>
/// This class is responsible for applying the hazard effect to the player.
/// </summary>
public class ReduceTime : BlockEffect {
    public ReduceTime(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Image = image;
        this.Shape = shape;
    }

    public override void Effect(Player p) {
        p.ReduceTime();
    }
}