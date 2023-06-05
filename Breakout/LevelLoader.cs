using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.BreakoutStates;

using Breakout.Blocks;
using Breakout.Blocks.Factory;

namespace Breakout;
public class LevelLoader {
    private string[] fileEntries = Directory.GetFiles(Path.Combine("Assets", "Levels"));
    private int mapLevel;
    public Timer timer;
    private NormalBlockFactory normalBlockFactory;
    private InvisibleBlockFactory invisibleBlockFactory;
    private UnbreakableBlockFactory unbreakableBlockFactory;
    private PowerUpBlockFactory powerUpBlockFactory;

    public LevelLoader() {
        mapLevel = 0;
        timer = new Timer(new Vec2F(0.0f, -0.1f), new Vec2F(0.3f, 0.32f));
        normalBlockFactory = new NormalBlockFactory();
        invisibleBlockFactory = new InvisibleBlockFactory();
        unbreakableBlockFactory = new UnbreakableBlockFactory();
        powerUpBlockFactory = new PowerUpBlockFactory();
    }

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

    private Dictionary<string, string> MetaMaker(List<string> lst) {
        Dictionary<string, string> meta = new Dictionary<string, string>{};

        int j = 0;
        for (int i = 1; i < lst.Count; i++) {
            if (lst[j] == "Meta:") {
                while (lst[i] != "Meta/") {
                    string key = "";
                    int k = 0;
                        while (lst[i][k] != ":".ToCharArray()[0]){
                            key = key + lst[i][k].ToString();
                            k++;
                    }
                    if (!(meta.ContainsKey(key))) {    
                        meta.Add(key, lst[i].Substring(key.Length+2));
                    } else if (meta.ContainsKey(key)) {
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
        return meta;
    }

    private Dictionary<string, Block> BlockDic(int j, int i) {

        DynamicShape blockShape = new DynamicShape(new Vec2F(((1f/12f)*(float)i), 
            ((1f - 2f/25f) - (float)j/25f)), new Vec2F(1f/12f, 1f/25f)); 
        Dictionary<string, Block> blocks = new Dictionary<string, Block>{
            {"blue-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "blue-block.png")), 
                new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png")))},
            {"brown-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "brown-block.png")), 
                new Image(Path.Combine("Assets", "Images", "brown-block-damaged.png")))},
            {"darkgreen-block.png", 
            normalBlockFactory.CreateBlock(blockShape, 
                new Image(Path.Combine("Assets", "Images", "darkgreen-block.png")), 
                new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png")))},
            {"green-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "green-block.png")), 
                new Image(Path.Combine("Assets", "Images", "green-block-damaged.png")))},
            {"grey-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "grey-block.png")), 
                new Image(Path.Combine("Assets", "Images", "grey-block-damaged.png")))},
            {"orange-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "orange-block.png")), 
                new Image(Path.Combine("Assets", "Images", "orange-block-damaged.png")))},
            {"purple-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "purple-block.png")), 
                new Image(Path.Combine("Assets", "Images", "purple-block-damaged.png")))},
            {"red-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "red-block.png")), 
                new Image(Path.Combine("Assets", "Images", "red-block-damaged.png")))},
            {"teal-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "teal-block.png")), 
                new Image(Path.Combine("Assets", "Images", "teal-block-damaged.png")))},
            {"yellow-block.png", 
            normalBlockFactory.CreateBlock(blockShape, new Image
                (Path.Combine("Assets", "Images", "yellow-block.png")), 
                new Image(Path.Combine("Assets", "Images", "yellow-block-damaged.png")))}
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
                        NormalBlock newBlock = (NormalBlock)blocks[legend[sign]];

                        if (MetaMaker(lst).ContainsKey("Unbreakable") 
                            || MetaMaker(lst).ContainsKey("Invisible")) {
                                if (MetaMaker(lst).ContainsKey("Unbreakable") 
                                    && MetaMaker(lst)["Unbreakable"] == sign.ToString()) {
                                        UnbreakableBlock unbreakaBlock = 
                                            (UnbreakableBlock)unbreakableBlockFactory.CreateSpecialBlock(newBlock);
                                        blockMap.AddEntity(unbreakaBlock);
                                } else if (MetaMaker(lst).ContainsKey("Invisible") 
                                    && MetaMaker(lst)["Invisible"] == sign.ToString()) {
                                        InvisibleBlock invisiBlock = 
                                            (InvisibleBlock)invisibleBlockFactory.CreateSpecialBlock(newBlock);
                                        blockMap.AddEntity(invisiBlock);
                                } else if (MetaMaker(lst).ContainsKey("PowerUp") 
                                    && MetaMaker(lst)["PowerUp"] == sign.ToString()) {
                                        PowerUpBlock PUBlock = 
                                            (PowerUpBlock)powerUpBlockFactory.CreateSpecialBlock(newBlock);
                                        blockMap.AddEntity(PUBlock);
                                } else {
                                    blockMap.AddEntity(newBlock);
                                }
                        } 
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message); 
                    }
                }
            }
        }

        timer.TurnOffReset();

        if (MetaMaker(lst).ContainsKey("Time")) {
            timer.SetTime(Int32.Parse(MetaMaker(lst)["Time"]));
        }


        mapLevel++;
        return blockMap;
    }
}