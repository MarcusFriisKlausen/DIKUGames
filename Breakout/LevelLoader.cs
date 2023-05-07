using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Utilities;
using Breakout.BreakoutStates;
using DIKUArcade.Events;

using Breakout.Blocks;
namespace Breakout;
public class LevelLoader {
    // nullImage is used as a placeholder for the block images
    private Image nullImage = new Image(
        Path.Combine("Assets", "Images", "deep-bronze-square.png"));
    public StreamReader nextMap = new StreamReader(Path.Combine(
        Constants.MAIN_PATH, "Assets","Levels","level2.txt"));
    public StreamReader currentMap = new StreamReader(Path.Combine(
        Constants.MAIN_PATH, "Assets","Levels","level1.txt"));
    private List<string> LevelListMaker(StreamReader map) {
        List<string> lineList = new List<string>();
        try {
            using (map) {
                string? line;
                while ((line = map.ReadLine()) != "Map/") {
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
    public EntityContainer<Entity> LevelMaker(StreamReader map) {
        EntityContainer<Entity> blockMap = new EntityContainer<Entity>();
        List<string> level = LevelListMaker(map);
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
        if (currentMap == new StreamReader(@"Assets/Levels/level2.txt")) {
            nextMap = new StreamReader(@"Assets/Levels/level3.txt");
        }
        else if (currentMap == new StreamReader(@"Assets/Levels/level3.txt")) {
            nextMap = new StreamReader(@"Assets/Levels/columns.txt");
        }
        else if (currentMap == new StreamReader(@"Assets/Levels/columns.txt")) {
            nextMap = new StreamReader(@"Assets/Levels/wall.txt");
        }
        else if (currentMap == new StreamReader(@"Assets/Levels/wall.txt")) {
            nextMap = new StreamReader(@"Assets/Levels/central-mass.txt");
        }
        else {
            GameEvent returnToMenu = new GameEvent();
            returnToMenu.EventType = GameEventType.GameStateEvent;
            returnToMenu.Message = "MAIN_MENU";
            BreakoutBus.GetBus().RegisterEvent(returnToMenu);
        }
    return blockMap;
    }
}