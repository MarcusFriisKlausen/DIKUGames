using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;

namespace Breakout.PowerUps;
/// <summary>
/// This class is responsible for applying the power up effect to the player.
/// </summary>
public class Wide : BlockEffect {
    public Wide(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Image = image;
        this.Shape = shape;
    }

    public override void Effect(Player p) {
        p.Widen() ;
    }
}