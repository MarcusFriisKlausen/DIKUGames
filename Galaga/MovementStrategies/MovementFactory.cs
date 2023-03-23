using System;

namespace Galaga.MovementStrategy {
    public class MovementFactory {

        public IMovementStrategy movement;
        /// <summary>
        /// Creates a random movement strategy.
        /// </summary>
        public IMovementStrategy CreateMovement(int level) {
            Random rnd = new Random();
            int r = level;

            if (r % 3 == 0) {
                movement = new ZigZagDown();
            } 
            else {
                movement = new Down();
            }

            if (r == 0) {
                movement = new NoMove();
            }
            return movement;
        }
    }
}