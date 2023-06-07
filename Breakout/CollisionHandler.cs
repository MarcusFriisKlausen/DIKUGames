using DIKUArcade.Physics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Blocks;
using Breakout.BreakoutStates;

namespace Breakout;
/// <summary>
/// This class is responsible for handling collisions between the ball and the player and walls,
/// and changing the angel and direction of the balls movement pattern.
/// </summary>
public static class CollisionHandler {
    /// </summary>
    /// 
    /// </summary>
    public static void HandlePaddleCollision(DynamicShape ball, Shape player) {
        var data = CollisionDetection.Aabb(ball, player);

        if (data.Collision) {
            double pMid = player.Position.X + (player.Extent.X / 2);
            double ballMid = ball.Position.X + (ball.Extent.X / 2);
            double midDiff = (pMid - ballMid) / (player.Extent.X / 2);

            double ballXSpeed;
            double ballYSpeed;
            
            ballXSpeed = ball.Direction.X;
            ballYSpeed = ball.Direction.Y;
            
            double ballSpeed = Math.Sqrt(ballXSpeed*ballXSpeed + ballYSpeed*ballYSpeed);

            const double X_INFLUENCE = 0.75;

            ballXSpeed = -(ballSpeed * midDiff * X_INFLUENCE);
            ball.Direction.X = (float)ballXSpeed;
            
            ballYSpeed = Math.Sqrt(ballSpeed * ballSpeed - ballXSpeed * ballXSpeed);
            ball.Direction.Y = (float)ballYSpeed;
        }
    }

    /// </summary>
    /// Changes the balls direction by multiplying with -1 if it collides with the walls or the 
    /// top of the window
    /// </summary>
    public static void HandleWallCollision(Shape ball) {
        if (ball.Position.X <= 0.0f || ball.Position.X >= 1.0f - ball.Extent.X) {
            ball.AsDynamicShape().Direction = 
            new Vec2F(ball.AsDynamicShape().Direction.X*(-1), ball.AsDynamicShape().Direction.Y);
        }
        if (ball.Position.Y >= 1.0f - ball.Extent.Y) {
            ball.AsDynamicShape().Direction = 
            new Vec2F(ball.AsDynamicShape().Direction.X, ball.AsDynamicShape().Direction.Y*(-1));
        }
    }

    /// </summary>
    /// Changes the balls horizontal direction. If collision happens with a block, the block loses
    /// health. If a invisible block is hit it turns visible. If the blocks health equals zero
    /// the score is incremented.
    /// </summary>
    public static void ChangeDirRightLeft(DynamicShape ball, EntityContainer<Block> cont) {
        cont.Iterate(ent => {    
            var entShape = ent.Shape;
            var dataEnt = CollisionDetection.Aabb(ball, entShape);
            if (dataEnt.Collision){
                if (dataEnt.CollisionDir == CollisionDirection.CollisionDirRight
                    || dataEnt.CollisionDir == CollisionDirection.CollisionDirLeft) {
                        if (ent.CanBeDestroyed) {
                            ent.LoseHealth();
                        }
                        if (ent is InvisibleBlock) {
                            ((InvisibleBlock)ent).Visiblize();
                        }
                        if (ent.Health == 0) {
                            GameRunning.GetInstance().Score.IncrementScore(ent.Value);
                        }
                        ball.Direction = new Vec2F(
                            ball.Direction.X*(-1), 
                            ball.Direction.Y);
                }
            }    
        });
    }

    /// </summary>
    /// Changes the balls vertical direction. If collision happens with a block, the block loses
    /// health. If a invisible block is hit it turns visible. If the blocks health equals zero
    /// the score is incremented.
    /// </summary>
    public static void ChangeDirUpDown(DynamicShape ball, EntityContainer<Block> cont) {
        cont.Iterate(ent => {    
            var entShape = ent.Shape;
            var dataEnt = CollisionDetection.Aabb(ball, entShape);
            if (dataEnt.Collision){
                if (dataEnt.CollisionDir == CollisionDirection.CollisionDirUp
                    || dataEnt.CollisionDir == CollisionDirection.CollisionDirDown) {
                        if (ent.CanBeDestroyed) {
                            ent.LoseHealth();
                        }
                        if (ent is InvisibleBlock) {
                            ((InvisibleBlock)ent).Visiblize();
                        }
                        if (ent.Health == 0) {
                            GameRunning.GetInstance().Score.IncrementScore(ent.Value);
                        }
                        ball.Direction = new Vec2F(
                            ball.Direction.X, 
                            ball.Direction.Y*(-1));
                }
            }    
        });
    }

}