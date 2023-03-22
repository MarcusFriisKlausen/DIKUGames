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
using System;
using Galaga.Squadron;
using Galaga.MovementStrategy;

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
        private SquadronFormI squadronI = new SquadronFormI();
        private NoMove noMove = new NoMove();
        private Down down = new Down();
        private ZigZagDown zigZag = new ZigZagDown();
        private Health health;
        private Text levelDisplay;
        private List<Image> images;
        private List<Image> enrageStride;
        private int level = 0;
        private SquadronFormV squadronV = new SquadronFormV();
        private SquadronFormBracket squadronBracket = new SquadronFormBracket();

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { 
                GameEventType.InputEvent, GameEventType.PlayerEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            images = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            
            enrageStride = ImageStride.CreateStrides
                (2, Path.Combine("Assets","Images", "RedMonster.png"));
            
            squadronI.CreateEnemies(images, enrageStride);

            enemies = squadronI.Enemies;

            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            enemyExplosions = new AnimationContainer(squadronI.MaxEnemies);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            
            health = new Health(new Vec2F(0.0f, -0.2f), new Vec2F(0.3f, 0.32f));

            levelDisplay = new Text ("LEVEL: " + (level.ToString()), new Vec2F(0.0f, 0.0f), 
                new Vec2F(0.3f, 0.32f));
            levelDisplay.SetColor(255, 255, 255, 255);
        }   

        private void KeyPress(KeyboardKey key) {
            switch  (key) {
                case KeyboardKey.Escape:
                    GameEvent esc = new GameEvent();
                    esc.EventType = GameEventType.InputEvent;
                    esc.Message = key.ToString();
                    eventBus.RegisterEvent(esc);
                    break;
                case KeyboardKey.Left:
                    GameEvent moveLeft = new GameEvent();
                    moveLeft.EventType = GameEventType.PlayerEvent;
                    moveLeft.Message = key.ToString();
                    eventBus.RegisterEvent(moveLeft);
                    break;
                case KeyboardKey.Right:
                    GameEvent moveRight = new GameEvent();
                    moveRight.EventType = GameEventType.PlayerEvent;
                    moveRight.Message = key.ToString();
                    eventBus.RegisterEvent(moveRight);
                    break;
                case KeyboardKey.Space:
                    GameEvent shoot = new GameEvent();
                    shoot.EventType = GameEventType.InputEvent;
                    shoot.Message = key.ToString();
                    eventBus.RegisterEvent(shoot);
                    break;
            }
        }

        private void KeyRelease(KeyboardKey key) {
            switch (key){
                case KeyboardKey.Left:
                    GameEvent stopLeft = new GameEvent();
                    stopLeft.EventType = GameEventType.PlayerEvent;
                    stopLeft.Message = key.ToString() + " Stop";
                    eventBus.RegisterEvent(stopLeft);
                    break;
                case KeyboardKey.Right:
                    GameEvent stopRight = new GameEvent();
                    stopRight.EventType = GameEventType.PlayerEvent;
                    stopRight.Message = key.ToString() + " Stop";
                    eventBus.RegisterEvent(stopRight);
                    break;
                
            }
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
            if (gameEvent.Message == KeyboardKey.Escape.ToString()) {
                window.CloseWindow();
            }
            else if (gameEvent.Message == KeyboardKey.Space.ToString()) {
                Vec2F pVec = player.GetPosition();
                playerShots.AddEntity(new PlayerShot(playerShotImage, pVec));
            }
            player.ProcessEvent(gameEvent);
        }
        
    
        public override void Render() {
            if (health.gameOver == false) {
                player.Render();
                enemies.RenderEntities();
                playerShots.RenderEntities();
                enemyExplosions.RenderAnimations();
                health.RenderHealth();
                levelDisplay.RenderText();
            } 
            else {
                eventBus.ProcessEventsSequentially();
                levelDisplay.RenderText();
            }
        }
        
        public override void Update() {
            eventBus.ProcessEventsSequentially();
            player.Move();
            IterateShots();
            if (level > 5) {
                zigZag.MoveEnemies(enemies);
            }
            if (level > 1 && level <= 5) {
                down.MoveEnemies(enemies);
            }
            if (level <= 1) {
                noMove.MoveEnemies(enemies);
            }
            HealthUpdate();
            LevelUpdate();
            EnemyReach();
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
                        if (data.Collision == true){
                            enemy.Hitpoints = enemy.Hitpoints - shot.Damage;
                            shot.DeleteEntity();
                            if (enemy.Hitpoints <= Math.Floor((double)(enemy.Max_hitpoints/2))){
                                enemy.Enrage(enrageStride);
                            }
                            if (enemy.Hitpoints <= 0 ) {
                                AddExplosion(enemy.Shape.Position, new Vec2F(0.1f, 0.1f));
                                enemy.DeleteEntity();
                                shot.DeleteEntity();
                                enemies = RemoveEntities(enemies);
                                if (enemies.CountEntities() == 0){
                                    SpawnNew();
                                }
                            }
                        }
                    });
                }
            }); 
        }
        
        public EntityContainer<Enemy> RemoveEntities(EntityContainer<Enemy> container){
            var count = container.CountEntities();
            EntityContainer<Enemy> newCont = new EntityContainer<Enemy>(count);

            foreach (Enemy ent in container) {
                if (!ent.IsDeleted()) {
                    newCont.AddEntity(ent);
                }
            }

            return newCont;
        }

        public void AddExplosion(Vec2F position, Vec2F extent) {
            StationaryShape explosionShape = 
            new StationaryShape(position.X, position.Y, extent.X, extent.Y);
            ImageStride explosion = new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides);
            enemyExplosions.AddAnimation(explosionShape, EXPLOSION_LENGTH_MS, explosion);
        }

        public void HealthUpdate() {
            health.display.SetText("HEALTH: " + health.ToString());

    
        }

        public void LevelUpdate() {
            levelDisplay.SetText("LEVEL: " + (level.ToString()));
        }

        public void SpawnNew() {
            squadronI = new SquadronFormI();
            squadronV = new SquadronFormV();
            squadronBracket = new SquadronFormBracket();
            
            squadronI.CreateEnemies(images, enrageStride);
            squadronV.CreateEnemies(images, enrageStride);
            squadronBracket.CreateEnemies(images, enrageStride);

            level++;
            Random rnd = new Random();
            int r = rnd.Next(0, 2);
            
            if (r == 0) {
                foreach (Enemy enm in squadronV.Enemies) {
                    for (int i = 0; i < level; i++) {
                        enm.IncreaseMS();
                    }
                }
                enemies = squadronV.Enemies;
            } 
            if (r == 1) {
                foreach (Enemy enm in squadronBracket.Enemies) {
                    for (int i = 0; i < level; i++) {
                        enm.IncreaseMS();
                    }
                }
                enemies = squadronBracket.Enemies;
            } 
            if (r == 2) {
                foreach (Enemy enm in squadronI.Enemies) {
                    for (int i = 0; i < level; i++) {
                        enm.IncreaseMS();
                    }
                }
                enemies = squadronI.Enemies;
            }
        }

        public void EnemyReach() {
            foreach (Enemy enemy in enemies) {
                if (enemy.Shape.Position.Y <= 0.0f + enemy.Shape.Extent.Y){
                                health.LoseHealth();
                                enemy.DeleteEntity();  
                                enemies = RemoveEntities(enemies);
                                if (enemies.CountEntities() == 0){
                                    SpawnNew();
                                }
                            }
            }
        }

    }  
}