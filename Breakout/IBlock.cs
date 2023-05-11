using DIKUArcade.Graphics;

namespace Breakout{
    public interface IBlock {
        // bool CanBeDestoyed {get;}
        int MaxHealth {get;}
        int Health {get; set;}
        IBaseImage Image {get; set;}
        IBaseImage BrokenImage {get;}
    }
}