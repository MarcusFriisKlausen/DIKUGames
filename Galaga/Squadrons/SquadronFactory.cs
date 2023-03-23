using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga.Squadron {
    public class SquadronFactory {
        public ISquadron squadron;

        /// <summary>
        /// Creates a random squadron of a random formation.
        /// </summary>
        public void CreateSquadron() {
            Random rnd = new Random();
            int r = rnd.Next(0,3);

            if (r == 0) {
                squadron = new SquadronFormI();
            } 
            else if (r == 1) {
                squadron =  new SquadronFormBracket();
            } 
            else if ( r == 2) {
                squadron =  new SquadronFormV();     
            }
        }

        /// <summary>
        /// Spawns a new squadron of enemies if there are no enemies left.
        /// </summary>
        public EntityContainer<Enemy> SpawnNew(EntityContainer<Enemy> enemies, List<Image> images, 
            List<Image> enrageStride, int level) {
                CreateSquadron();
                squadron.CreateEnemies(images, enrageStride);
            
                enemies = squadron.Enemies;
                
                foreach (Enemy enm in enemies){
                    enm.IncreaseMS(level);
                }

                return enemies;
        }

        
    }    
}