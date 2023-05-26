// using NUnit.Framework;
// using DIKUArcade.Math;
// using Breakout.Blocks;
// using DIKUArcade.Events;
// using DIKUArcade.Entities;
// using DIKUArcade.Graphics;
// using DIKUArcade.Input;
// using DIKUArcade.Physics;
// using Breakout.BreakoutStates;

// namespace Breakout.Tests {
//     [TestFixture]
//     public class BallTests {
//         Ball ball;
//         NormalBlock block;
//         Player player;
//         [SetUp]
//         public void SetUp(){
//             DIKUArcade.GUI.Window.CreateOpenGLContext();
//             var shape = new DynamicShape(new Vec2F((1f/12f), 1f), new Vec2F(1f/12f, 1f/25f));
//             var image = new Image(Path.Combine("Assets", "Images", "darkgreen-block.png"));
//             var brokenImage = new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png"));
//             ball = new Ball(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)), null);
//             block = new NormalBlock(shape, image, brokenImage);
//             player = new Player(shape, image);

//             BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

//         }
        
//         [Test]
//         public void TestMove_DirectionChangesOnCollisionWithBlock() {
//             // Act
//             ball.Move()

//             // Assert
//             Assert.AreNotEqual(new Vec2F(0.01f, 0.01f), ball.Shape.AsDynamicShape().Direction);
//         }
        
//         [Test]
//         public void TestMove_DirectionChangesOnCollisionWithPlayer() {
//             // Act
//             ball.Move(new EntityContainer<Block>(), player);

//             // Assert
//             Assert.AreNotEqual(new Vec2F(0.01f, 0.01f), ball.Shape.AsDynamicShape().Direction);
//         }
        
//         [Test]
//         public void TestMove_DirectionDoesNotChangeWithoutCollision() {
//             // Act
//             ball.Move(new EntityContainer<Block>(), null);

//             // Assert
//             Assert.AreEqual(new Vec2F(0.01f, 0.01f), ball.Shape.AsDynamicShape().Direction);
//         }
        
//         [Test]
//         public void TestLosingHealth_ReturnsOneWhenBallGoesOutOfBound() {
//             ball.Shape.Position.Y = -0.1f;
//             // Act
//             int result = ball.LosingHealth();

//             // Assert
//             Assert.AreEqual(1, result);
//         }
        
//         [Test]
//         public void TestLosingHealth_ReturnsZeroWhenBallIsWithinBound() {
//             // Act
//             int result = ball.LosingHealth();

//             // Assert
//             Assert.AreEqual(0, result);
//         }
//     }
// }
