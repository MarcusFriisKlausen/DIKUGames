using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;
using Breakout.BreakoutStates;

namespace Breakout.PowerUps;

public class DoubleSize : BlockEffect {
   public DoubleSize(DynamicShape shape, IBaseImage image) : base(shape, image) {
       this.Image = image;
       this.Shape = shape;
   }

   public override void Effect(Player p) {
       GameRunning.GetInstance().DoubleSize();
   }
}