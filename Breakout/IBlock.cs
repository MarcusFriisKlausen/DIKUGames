using DIKUArcade.Graphics;

namespace Breakout;
public interface IBlock {
    // bool CanBeDestroyed {get;}
    int Health {get; set;}
    int Value {get;}
    IBaseImage Image {get; set;}
    IBaseImage BrokenImage {get;}
}
