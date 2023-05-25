using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;
using DIKUArcade.Math;

namespace Breakout.PowerUps;

public class Wide : BlockEffect {
    public Wide(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Image = image;
        this.Shape = shape;
    }

    public override void Effect(Player p) {
        p.Widen() ;
    }
}