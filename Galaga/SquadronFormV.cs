using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System.Collections.Generic;

namespace Galaga.Squadron;
public class SquadronFormV : ISquadron {
    const int maxEnemies = 5;
    public int MaxEnemies{
        get{return maxEnemies;}
    }

    private EntityContainer<Enemy> enemies = new EntityContainer<Enemy>(maxEnemies);
    public EntityContainer<Enemy> Enemies{
        get{return enemies;}
    }

    public SquadronFormV() : base() {}
    
    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        for (int i = 0; i < maxEnemies; i++) {
                if (i <= 1) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(
                            new Vec2F((0.45f + 0.2f - (float)i * 0.1f), 0.9f - (float)i * 0.1f), 
                            new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride)));
                } else if (i == 2) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(
                            new Vec2F(0.45f, 0.9f - (float)i * 0.1f), 
                            new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride)));
                } else {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(
                            new Vec2F((0.45f - ((float)i - 2) * 0.1f), 0.7f + ((float)i - 2) * 0.1f), 
                            new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride)));
                }
        }
    }
    
}