using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;

namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            float a = 0.05f; 
            float p = 0.045f;

            float y_0 = enemy.StartPos.Y;
            float y_1 = enemy.Shape.Position.Y - enemy.MOVEMENT_SPEED;
            float x_0 = enemy.StartPos.X;
            float x_1 = (float)(a * Math.Sin((2.0 * Math.PI * (y_0 - y_1)) / p));

            enemy.Shape.Position.Y = y_1;

            if (!enemy.IsMovingLeft && enemy.Shape.Position.X + x_1 + enemy.Shape.Extent.X >= 1.0f) {
                x_1 = enemy.Shape.Position.X - (x_0 + x_1);
                enemy.swapDirection();
            } else if (enemy.IsMovingLeft && enemy.Shape.Position.X + x_1 <= 0.0f) {
                x_1 = -enemy.Shape.Position.X - x_0 - x_1;
                enemy.swapDirection();
            }

            if (!(enemy.IsMovingLeft)) {
                enemy.Shape.Position.X = x_0 + x_1;
            } else {
                enemy.Shape.Position.X = x_0 - x_1;
            }
        }




        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach (Enemy enm in enemies) {
                MoveEnemy(enm);
            }
        }
    }
}  