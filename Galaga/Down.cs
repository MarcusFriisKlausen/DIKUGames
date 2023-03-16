using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {

    public class Down : IMovementStrategy {
        public void MoveEnemy (Enemy enemy){
            enemy.Shape.Position.Y = enemy.Shape.Position.Y - enemy.MOVEMENT_SPEED;
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach (Enemy enm in enemies) {
                MoveEnemy(enm);
            }
        }
    }
}  