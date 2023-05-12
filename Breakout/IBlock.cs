<<<<<<< Updated upstream
using DIKUArcade.Graphics;

namespace Breakout{
    public interface IBlock {
        IBaseImage image {get;}
        IBaseImage brokenImage {get;}
    }
}
=======
using DIKUArcade.Graphics;

namespace Breakout;
public interface IBlock {
    // bool CanBeDestroyed {get;}
    int Health {get; set;}
    int Value {get;}
    IBaseImage Image {get; set;}
    IBaseImage BrokenImage {get;}
}
>>>>>>> Stashed changes
