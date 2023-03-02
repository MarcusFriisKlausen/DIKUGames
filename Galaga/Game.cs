using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;

namespace Galaga
{
    public class Game : DIKUGame, IGameEventProcessor{
        private Player player;

        private GameEventBus eventBus;

        private EntityContainer<Enemy> enemies;

        private EntityContainer<PlayerShot> playerShots;

        private IBaseImage playerShotImage;

        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);

            List<Image> images = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), 
                    new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, images)));
            }

            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            enemyExplosions = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
        }   

        private void KeyPress(KeyboardKey key) {
            if (key == KeyboardKey.Escape) {
                GameEvent esc = new GameEvent();
                esc.EventType = GameEventType.WindowEvent;
                esc.Message = key.ToString();
                ProcessEvent(esc);
            }
            else if (key == KeyboardKey.Left) {
                GameEvent moveLeft = new GameEvent();
                moveLeft.EventType = GameEventType.MovementEvent;
                moveLeft.Message = key.ToString();
                ProcessEvent(moveLeft);
            }
            else if (key == KeyboardKey.Right) {
                GameEvent moveRight = new GameEvent();
                moveRight.EventType = GameEventType.MovementEvent;
                moveRight.Message = key.ToString();
                ProcessEvent(moveRight);
            }
            else if (key == KeyboardKey.Space) {
                GameEvent shoot = new GameEvent();
                shoot.EventType = GameEventType.PlayerEvent;
                shoot.Message = key.ToString();
                ProcessEvent(shoot);
            }
        }

        private void KeyRelease(KeyboardKey key) {
            GameEvent release = new GameEvent();
            release.Message = key.ToString();
            player.SetMoveLeft(false);
            player.SetMoveRight(false);
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyRelease){
                KeyRelease(key);
            }
            else if (action == KeyboardAction.KeyPress){
                KeyPress(key);
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                window.CloseWindow();
            }
            else if (gameEvent.EventType == GameEventType.MovementEvent) {
                if (gameEvent.Message == KeyboardKey.Left.ToString()) {
                    player.SetMoveLeft(true);
                }
                else if (gameEvent.Message == KeyboardKey.Right.ToString()) {
                    player.SetMoveRight(true);
                }
            }
            else if (gameEvent.EventType == GameEventType.PlayerEvent) {
                Vec2F pVec = player.GetPosition();
                playerShots.AddEntity(new PlayerShot(playerShotImage, pVec));
            }
        }

        public override void Render() {
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
        }
        public override void Update() {
            eventBus.ProcessEventsSequentially();
            player.Move();
            IterateShots();
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                shot.Shape.MoveY(0.1f);
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                } 
                else {
                    enemies.Iterate(enemy => {
                        var shotShape = shot.Shape.AsDynamicShape();
                        shotShape.ChangeDirection(new Vec2F(0.0f, 0.1f));
                        var enemyShape = enemy.Shape;
                        var data = CollisionDetection.Aabb(shotShape, enemyShape);
                        if (data.Collision == true) {
                            AddExplosion(enemy.Shape.Position, new Vec2F(0.1f, 0.1f));
                            enemy.DeleteEntity();
                            shot.DeleteEntity();
                        }
                    });
                }
            });
        }

        public void AddExplosion(Vec2F position, Vec2F extent) {
            StationaryShape explosionShape = 
            new StationaryShape(position.X, position.Y, extent.X, extent.Y);
            ImageStride explosion = new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides);
            enemyExplosions.AddAnimation(explosionShape, EXPLOSION_LENGTH_MS, explosion);
        }
    }  
}
