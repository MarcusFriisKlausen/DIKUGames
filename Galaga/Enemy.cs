using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Galaga;
    public class Enemy : Entity {
        private IBaseImage enemyImage;
        private int hitpoints = 5; 
        public int Hitpoints {
            get {
                return hitpoints;
            }

            set {
                hitpoints = value;
            }
        }
        private bool isEnraged = false;
        public bool IsEnraged {
            get {
                return isEnraged;
            }
        }

        public Enemy(DynamicShape shape, IBaseImage image) 
            : base(shape, image) {
                enemyImage = base.Image;
            }

        public void Enrage() {
            if (hitpoints >= 3) {
                isEnraged = true;
                this.Image =  ;    
            }
        }
}