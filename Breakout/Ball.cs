using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;


namespace Breakout {
    public class Ball : Entity {
        public Ball(DynamicShape shape, IBaseImage image) 
        : base(shape, image) {
            Image = base.Image;
        }

    }
}