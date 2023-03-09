using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga {
    public class Player {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft;
        private float moveRight;
        private const float MOVEMENT_SPEED = 0.01f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            this.moveLeft = 0.0f;
            this.moveRight = 0.0f;
        }

        public void Move() {
            float oldX = this.shape.Position.X;
            this.shape.Move();

            if (this.shape.Position.X < 0.0f) {
                this.shape.Position.X = 0.0f;
            } else if (this.shape.Position.X > 1.0f - this.shape.Extent.X) {
                this.shape.Position.X = 1.0f - this.shape.Extent.X;
            }

            if (this.shape.Position.X != oldX) {
                this.moveLeft = 0.0f;
                this.moveRight = 0.0f;
            }
        }
    

        public void SetMoveLeft(bool val) {
            if (val == true){
               this.moveLeft -= MOVEMENT_SPEED; 
            }
            else{
                this.moveLeft = 0.0f;
            }
            UpdateDirection();
        }

        public void SetMoveRight(bool val) {
            if (val == true){
                this.moveRight += MOVEMENT_SPEED; 
            }
            else{
                this.moveRight = 0.0f;
            }
            UpdateDirection();
        }

        private void UpdateDirection() {
            this.shape.Direction.X = this.moveLeft + this.moveRight;
        }

        public void Render() {
            entity.RenderEntity();
        }

        public Vec2F GetPosition() {
            return this.shape.Position;
        }
    }
}