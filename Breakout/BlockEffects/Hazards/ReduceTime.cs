using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;

namespace Breakout.Hazards;

public class ReduceTime : BlockEffect {
    public ReduceTime(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Image = image;
        this.Shape = shape;
    }

    public override void Effect(Player p) {
        p.ReduceTime();
    }
}