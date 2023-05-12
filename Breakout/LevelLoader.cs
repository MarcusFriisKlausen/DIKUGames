using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Utilities;
using Breakout.BreakoutStates;
using DIKUArcade.Events;
using System.Runtime;

namespace Breakout;
public class LevelLoader {
    private string[] fileEntries = Directory.GetFiles(Path.Combine("Assets", "Levels"));
    private int mapLevel = 0;
    public StreamReader nextMap = new StreamReader(Path.Combine(
        Constants.MAIN_PATH, "Assets", "Levels", "level2.txt"));
    public StreamReader currentMap = new StreamReader(Path.Combine(
        Constants.MAIN_PATH, "Assets", "Levels", "level1.txt"));
    private List<string> LevelListMaker(StreamReader map) {
        List<string> lineList = new List<string>();
        try {
            using (map) {
                string? line;
                while ((line = map.ReadLine()) != "Legend/") {
                    if (line != null) {
                        lineList.Add(line);
                    }
                }
                if ((line = map.ReadLine()) == "Legend/") {
                    lineList.Add(line);
                }
            }
        } catch (Exception e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return lineList;
    }

    private List<string> LevelMapListMaker(List<string> lst) {
        List<string> lineList = new List<string>();
        
        for (int i = 1; i < lst.Count; i++) {
            if (i == 0) {
                i++;
            }
            if (lst[i-1] == "Map:") {
                while (lst[i] != "Map/") {
                    lineList.Add(lst[i]);
                    i++;
                }
            }
        }
        return lineList;
    }

    private Dictionary<char, string> LegendMaker(List<string> lst) {
        Dictionary<char, string> legend = new Dictionary<char, string>{};
        
        int j = 0;
        for (int i = 1; i < lst.Count; i++) {
            if (lst[j] == "Legend:") {
                while (lst[i] != "Legend/") {
                    if (!(legend.ContainsKey(lst[i][0]))) {    
                        legend.Add(lst[i][0], lst[i].Substring(3));
                    } else if (legend.ContainsKey(lst[i][0])) {
                        break;
                    }
                    if (i < lst.Count - 1) {
                        i++;
                        j++;
                    }
                }
            }
            j++;
        }
        return legend;
    }

    private Dictionary<string, Block> BlockDic(int j, int i) {

        DynamicShape blockShape = new DynamicShape(new Vec2F(((1f/12f)*(float)i), 
            (1f - (float)j/25f)), new Vec2F(1f/12f, 1f/25f)); 
        Dictionary<string, Block> blocks = new Dictionary<string, Block>{
            {"blue-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "blue-block.png")), 
                new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png")))},
            {"brown-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "brown-block.png")), 
                new Image(Path.Combine("Assets", "Images", "brown-block-damaged.png")))},
            {"darkgreen-block.png", 
            new Block(blockShape, 
                new Image(Path.Combine("Assets", "Images", "darkgreen-block.png")), 
                new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png")))},
            {"green-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "green-block.png")), 
                new Image(Path.Combine("Assets", "Images", "green-block-damaged.png")))},
            {"grey-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "grey-block.png")), 
                new Image(Path.Combine("Assets", "Images", "grey-block-damaged.png")))},
            {"orange-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "orange-block.png")), 
                new Image(Path.Combine("Assets", "Images", "orange-block-damaged.png")))},
            {"purple-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "purple-block.png")), 
                new Image(Path.Combine("Assets", "Images", "purple-block-damaged.png")))},
            {"red-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "red-block.png")), 
                new Image(Path.Combine("Assets", "Images", "red-block-damaged.png")))},
            {"teal-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "teal-block.png")), 
                new Image(Path.Combine("Assets", "Images", "teal-block-damaged.png")))},
            {"yellow-block.png", 
            new Block(blockShape, new Image(Path.Combine("Assets", "Images", "yellow-block.png")), 
                new Image(Path.Combine("Assets", "Images", "yellow-block-damaged.png")))},
            {"unbreakable-grey-block.png", 
            new BlockUnbreakable(blockShape, 
                new Image(Path.Combine("Assets", "Images", "grey-block.png")), 
                new Image(Path.Combine("Assets", "Images", "grey-block-damaged.png")))},
            {"hardened-brown-block.png", 
            new BlockHardened(blockShape, 
                new Image(Path.Combine("Assets", "Images", "brown-block.png")), 
                new Image(Path.Combine("Assets", "Images", "brown-block-damaged.png")))}
        };
        return blocks;
    }

    public EntityContainer<Block> LevelMaker() {
        if (mapLevel == fileEntries.Length) {
            GameEvent returnToMenu = new GameEvent();
            returnToMenu.EventType = GameEventType.GameStateEvent;
            returnToMenu.Message = "MAIN_MENU";
            BreakoutBus.GetBus().RegisterEvent(returnToMenu);
        }

        StreamReader map = new StreamReader(fileEntries[mapLevel]);
        EntityContainer<Block> blockMap = new EntityContainer<Block>();
        List<string> lst = LevelListMaker(map);
        List<string> level = LevelMapListMaker(lst);
        Dictionary<char, string> legend = LegendMaker(lst);
        for (int j = 0; j < level.Count; j++) {
            for (int i = 0; i < level[j].Length; i++) { 
                char sign = (level[j])[i];
                if (sign.ToString() != "-") {
                    try {
                        Dictionary<string, Block> blocks = BlockDic(j, i);
                        blockMap.AddEntity(blocks[legend[sign]]);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message); 
                    }
                }
            }
        }
        mapLevel++;
        return blockMap;
    }
}