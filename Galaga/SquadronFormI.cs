using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.Squadron;
public class SquadronFormI : ISquadron {
    const int maxEnemies = 4;
    public int MaxEnemies{
        get{return maxEnemies;}
    }

    private EntityContainer<Enemy> enemies = new EntityContainer<Enemy>(maxEnemies);
    public EntityContainer<Enemy> Enemies{
        get{return enemies;}
    }

    public SquadronFormI() : base() {}
    
    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        for (int i = 0; i < maxEnemies; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.45f, 0.9f - (float)i * 0.1f), 
                    new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride)));
        }
    }
    
}