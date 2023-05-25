using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;

namespace Breakout.PowerUps;

public class MoreTime : BlockEffect {
    public MoreTime(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Image = image;
        this.Shape = shape;
    }

    public override void Effect(Player p) {
        p.MoreTime();
    }
}