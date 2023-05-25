using Breakout.Effects;

namespace Breakout.Hazards;

public class HazardDrop {
    public int? rand;
    public List<BlockEffect> hazards;

    public HazardDrop() {
        hazards = new List<BlockEffect>{
        };
    }

    public void AddHazards(List<BlockEffect> hazardLst) {
        foreach (BlockEffect hazard in hazardLst) {
            hazards.Add(hazard);
        }
    }
    
    public void SetRandomInt() {
        rand = new Random().Next(5);
    }

    public BlockEffect Drop(Block block) {
        int hazardRand = new Random().Next(hazards.Count());
        BlockEffect hazard = hazards[hazardRand];
        hazard.Shape.Position = block.Shape.Position;
        return hazard;
    }
}