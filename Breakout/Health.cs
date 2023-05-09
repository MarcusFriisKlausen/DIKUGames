/* using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;

namespace Breakout;
public class Health{
    private int health = 3;
    public int Health {
        get {return health;}
    }
    private IBaseImage EmptyImage;
    public IBaseImage image { get { return this.Image; } }
    //public IBaseImage EmptyImage { get { return this.EmptyImage; } }
    public HeartImage(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Image = new Image(Path.Combine("Assets", "Images", "heart_filled.png"));
        this.EmptyImage = new Image(Path.Combine("Assets", "Images", "heart_empty.png"));
    }
}  */