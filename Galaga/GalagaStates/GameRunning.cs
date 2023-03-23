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
using DIKUArcade.State;

namespace Galaga.GalagaStates;

public class GameRunning : IGameState {

    private Player player;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;
    private Health health;
    private Text levelDisplay;
    public Text LevelDisplay {
        get {return levelDisplay;}
    }
    private List<Image> images;
    private List<Image> enrageStride;
    private int level = 0;
    private SquadronFactory squadFac;
    private MovementFactory moveFac;
    private static GameRunning instance = null;
    private GameEventBus eventBus;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;
    }
    
    private void InitGame() {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));

        // GalagaBus Subscriptions
        eventBus = GalagaBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.InputEvent, player);

        // Strides for monsters
        images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png")); 
        enrageStride = ImageStride.CreateStrides
            (2, Path.Combine("Assets","Images", "RedMonster.png"));
        
        // Squad and move factories.
        squadFac = new SquadronFactory();
        moveFac = new MovementFactory();
        squadFac.CreateSquadron();

        // Creating enemies
        squadFac.squadron.CreateEnemies(images, enrageStride);
        enemies = squadFac.squadron.Enemies;

        // Shots and explosions
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        enemyExplosions = new AnimationContainer(squadFac.squadron.MaxEnemies);
        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));
        
        // Health and level
        health = new Health(new Vec2F(0.0f, -0.2f), new Vec2F(0.3f, 0.32f));
        levelDisplay = new Text ("LEVEL: " + (level.ToString()), new Vec2F(0.0f, 0.0f), 
            new Vec2F(0.3f, 0.32f));
        levelDisplay.SetColor(255, 255, 255, 255);
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
            if (enemies.CountEntities() == 0) {
                enemies = squadFac.SpawnNew(enemies, images, enrageStride, level);
                level++;
            }
        }

        public void EnemyReach() {
            foreach (Enemy enemy in enemies) {
                if (enemy.Shape.Position.Y <= 0.0f + enemy.Shape.Extent.Y){
                                health.LoseHealth();
                                enemy.DeleteEntity();  
                                enemies = RemoveEntities(enemies);
                                if (health.ToString() == "0") {
                                    GameEvent gameOver = new GameEvent();
                                    gameOver.EventType = GameEventType.GameStateEvent;
                                    gameOver.Message = "GAME_LOST";
                                    GalagaBus.GetBus().RegisterEvent(gameOver);
                                } 
                            }
            }
        }
    public void ResetState() {
        InitGame();
    }

    public void UpdateState() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
        moveFac.CreateMovement(level).MoveEnemies(enemies);
        EnemyReach();
        HealthUpdate();
        LevelUpdate();
    }

    public void RenderState() {
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
        health.RenderHealth();
        levelDisplay.RenderText();
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        Console.WriteLine($"Handling key event: action={action}, key={key}");
        if (action == KeyboardAction.KeyRelease){
                KeyRelease(key);
            }
            else if (action == KeyboardAction.KeyPress){
                KeyPress(key);
            }
    }
    private void KeyRelease(KeyboardKey key) {
            switch (key){
                case KeyboardKey.Left:
                    GameEvent stopLeft = new GameEvent();
                    stopLeft.EventType = GameEventType.PlayerEvent;
                    stopLeft.Message = key.ToString() + " Stop";
                    GalagaBus.GetBus().RegisterEvent(stopLeft);
                    player.ProcessEvent(stopLeft);
                    break;
                case KeyboardKey.Right:
                    GameEvent stopRight = new GameEvent();
                    stopRight.EventType = GameEventType.PlayerEvent;
                    stopRight.Message = key.ToString() + " Stop";
                    GalagaBus.GetBus().RegisterEvent(stopRight);
                    player.ProcessEvent(stopRight);
                    break;
            }
        }
        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    GameEvent moveLeft = new GameEvent();
                    moveLeft.EventType = GameEventType.PlayerEvent;
                    moveLeft.Message = key.ToString();
                    GalagaBus.GetBus().RegisterEvent(moveLeft);
                    player.ProcessEvent(moveLeft);
                    break;
                case KeyboardKey.Right:
                    GameEvent moveRight = new GameEvent();
                    moveRight.EventType = GameEventType.PlayerEvent;
                    moveRight.Message = key.ToString();
                    GalagaBus.GetBus().RegisterEvent(moveRight);
                    player.ProcessEvent(moveRight);
                    break;
                case KeyboardKey.Space:
                    GameEvent shoot = new GameEvent();
                    shoot.EventType = GameEventType.InputEvent;
                    shoot.Message = key.ToString();
                    GalagaBus.GetBus().RegisterEvent(shoot);

                    Vec2F pVec = player.GetPosition() + (player.Shape.Extent / 2);
                    playerShots.AddEntity(new PlayerShot(playerShotImage, pVec));
                    break;
                case KeyboardKey.Escape:
                    GameEvent pause = new GameEvent();
                    pause.EventType = GameEventType.GameStateEvent;
                    pause.Message = "GAME_PAUSED";
                    GalagaBus.GetBus().RegisterEvent(pause);
                    break;
            }
        }

}
