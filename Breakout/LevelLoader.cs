using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

using Breakout.Blocks;
namespace Breakout{
    public class LevelLoader {
        // nullImage is used as a placeholder for the block images
        private Image nullImage = new Image(
            Path.Combine("Assets", "Images", "deep-bronze-square.png"));
        private StreamReader nextMap = new StreamReader(@"Assets/Levels/level2.txt");
        private StreamReader currentMap = new StreamReader(@"Assets/Levels/level1.txt");

        private List<string> LevelListMaker() {
            List<string> lineList = new List<string>();
            try {
                using (currentMap) {
                    string? line;
                    while ((line = currentMap.ReadLine()) != "Map/") {
                        if (line != null) {
                            lineList.Add(line);
                        }
                    }
                }
            } catch (Exception e) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return lineList;
        }
        // - 0.04f*(float)i
        public EntityContainer<Entity> LevelMaker() {
            EntityContainer<Entity> blockMap = new EntityContainer<Entity>();
            List<string> level = LevelListMaker();
            for (int j = 0; j < level.Count; j++) {
                for (int i = 0; i < level[j].Length; i++) { 
                    switch((level[j])[i]) {
                        case 'a':
                            blockMap.AddEntity(new PurpleBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)),
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                        case 'b':
                            blockMap.AddEntity(new YellowBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)),
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                        case 'h':
                        case '#':
                            blockMap.AddEntity(new GreenBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)),
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                        case 'i':
                            blockMap.AddEntity(new TealBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)),
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                        case 'k':
                        case 'Y':
                            blockMap.AddEntity(new BrownBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)),
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                        case 'w':
                            blockMap.AddEntity(new DarkGreenBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)),
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                        case '0':
                            blockMap.AddEntity(new GreyBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)), 
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                        case '1':
                            blockMap.AddEntity(new OrangeBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)), 
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                        case '%': 
                        case 'j':
                            blockMap.AddEntity(new BlueBlock(
                                new DynamicShape(
                                    new Vec2F(((1f/12f)*(float)i), (1f - (float)j/25f)), 
                                    new Vec2F(1f/12f, 1f/25f)), nullImage));
                            break;
                    }
                }
            }

            currentMap = nextMap;
            nextMap = new StreamReader(@"Assets/Levels/level3.txt");

            return blockMap;
        }
    }
}