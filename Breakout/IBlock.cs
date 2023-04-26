using DIKUArcade.Graphics;

namespace Breakout{
    public interface IBlock {
        IBaseImage image {get;}
        IBaseImage brokenImage {get;}
    }
}