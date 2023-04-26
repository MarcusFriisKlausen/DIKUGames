using DIKUArcade.Entities;

using Breakout.Blocks;
namespace Breakout{
    public class LevelLoader {
        private EntityContainer<Entity>? blocks; //Får nok problemer med Entity vs IBlock
        private StreamReader nextMap = new StreamReader(@"../Assets/Levels/level2.txt");
        private StreamReader currentMap = new StreamReader(@"../Assets/Levels/level1.txt");

        public EntityContainer<Entity> LevelMaker() {
            throw new System.NotImplementedException();
        }
    }
}