using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Math;
using System;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Timers;

namespace Breakout.BreakoutStates;

public class GamePaused : IGameState {
    private static GamePaused? instance;
    private Entity backGroundImage;
    private Text[] menuButtons;
    private int maxMenuButtons;
    private int minMenuButtons;
    private int activeMenuButton = 1;
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.ResetState();
        }
        return GamePaused.instance;
    }

    public GamePaused() {
        instance = null;
        backGroundImage = new Entity(new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        menuButtons = new Text[] {
            new Text("PAUSED", new Vec2F(0.35f, 0.2f), new Vec2F(0.5f, 0.5f)),
            new Text("Continue", new Vec2F(0.35f, 0.1f), new Vec2F(0.5f, 0.5f)),
            new Text("Quit", new Vec2F(0.42f, 0.0f), new Vec2F(0.5f, 0.5f)),
            new Text("Main Menu", new Vec2F(0.35f, -0.1f), new Vec2F(0.5f, 0.5f))
        };
        maxMenuButtons = 3;
        minMenuButtons = 1;
    }
    public void ResetState() {

    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        for (int i = 0; i < menuButtons.Length; i++) {
            menuButtons[i].RenderText();
        }
    }

    public void UpdateState() {
        BreakoutBus.GetBus().ProcessEvents();
        if (activeMenuButton >= minMenuButtons && activeMenuButton <= maxMenuButtons) {
            menuButtons[activeMenuButton].SetColor(255, 1, 1, 255);
        }
        for (int i = 0; i < menuButtons.Length; i++) {
            if (menuButtons[i] != menuButtons[activeMenuButton]) {
                menuButtons[i].SetColor(255, 255, 255, 255);
            }
            menuButtons[i].RenderText();
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyRelease){
            return;
        }
        if (key == KeyboardKey.Enter) {
            if (menuButtons[activeMenuButton].Equals(menuButtons[1])) {
                GameEvent returnToGame = new GameEvent();
                returnToGame.EventType = GameEventType.GameStateEvent;
                returnToGame.Message = "GAME_RUNNING";
                StaticTimer.ResumeTimer();
                BreakoutBus.GetBus().RegisterEvent(returnToGame);
                }
            if (menuButtons[activeMenuButton].Equals(menuButtons[2])) {
                GameEvent quit = new GameEvent();
                quit.EventType = GameEventType.WindowEvent;
                quit.Message = "QUIT";
                BreakoutBus.GetBus().RegisterEvent(quit);
            }
            if (menuButtons[activeMenuButton].Equals(menuButtons[3])) {
                GameEvent returnToMenu = new GameEvent();
                returnToMenu.EventType = GameEventType.GameStateEvent;
                returnToMenu.Message = "MAIN_MENU";
                BreakoutBus.GetBus().RegisterEvent(returnToMenu);
                activeMenuButton = 1;
                GameRunning.GetInstance().ResetState();
            }
        }
        if (key == KeyboardKey.Down && activeMenuButton < maxMenuButtons) {
                activeMenuButton = activeMenuButton + 1;
                return;
        }
        if (key == KeyboardKey.Up && activeMenuButton > minMenuButtons) {
                activeMenuButton = activeMenuButton - 1;
                return;
        }
    }
}