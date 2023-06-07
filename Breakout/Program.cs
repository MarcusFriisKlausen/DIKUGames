using System;
using DIKUArcade.GUI;

namespace Breakout{
    /// <summary>
    /// Opens up a window in which the game is run and can be played
    /// </summary>
    class Program{
        static void Main(string[] args){
            var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
            var game = new Game(windowArgs);
            game.Run();
           
        }
    }
}