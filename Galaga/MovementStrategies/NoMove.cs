using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {

    public class NoMove : IMovementStrategy {
        public void MoveEnemy (Enemy enemy) {
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies) {
        }
    }
}  