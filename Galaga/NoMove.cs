using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {

    public class NoMove : IMovementStrategy {
        public void MoveEnemy (Enemy enemy){
            enemy.Shape = enemy.Shape;
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach (Enemy enm in enemies) {
                MoveEnemy(enm);
            }
        }
    }
}  