using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;


namespace Galaga;

public class PlayerShot : Entity {
    private static Vec2F extent = new Vec2F(0.008f, 0.021f);

    private static Vec2F direction = new Vec2F(0.0f, 0.1f);

    public PlayerShot(IBaseImage image, Vec2F pVec) 
        : base(new DynamicShape(pVec, extent), image) {}
}