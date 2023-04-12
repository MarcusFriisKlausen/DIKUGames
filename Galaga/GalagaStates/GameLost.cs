using DIKUArcade;
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

namespace Galaga.GalagaStates;
public class GameLost : IGameState{
    private static GameLost instance = null;
    private Text levelDisplay;
    private Text gameOver;
    private Text[] menuButtons;
    private int maxMenuButtons;
    private int minMenuButtons;
    private int activeMenuButton = 1;
    public static GameLost GetInstance() {
        if (GameLost.instance == null) {
            GameLost.instance = new GameLost();
            GameLost.instance.ResetState();
        }
        return GameLost.instance;
    }
    public GameLost(){
        maxMenuButtons = 1;
        minMenuButtons = 0;
        menuButtons = new Text[]{
                new Text("Quit", new Vec2F(0.42f, 0.0f), new Vec2F(0.5f, 0.5f)),
                new Text("Main Menu", new Vec2F(0.35f, -0.1f), new Vec2F(0.5f, 0.5f))
        };

        gameOver = new Text ("GAME OVER", new Vec2F(0.275f, 0.2f), 
        new Vec2F(0.5f, 0.5f));
        gameOver.SetColor(255, 255, 1, 1);
        levelDisplay = GameRunning.GetInstance().LevelDisplay;
        levelDisplay.SetColor(255, 255, 255, 255);
    }
    public void ResetState() {
        
    }
    public void RenderState() {
        for (int i = 0; i < menuButtons.Length; i++) {
            menuButtons[i].RenderText();
        }
        gameOver.RenderText();
        levelDisplay.RenderText();
    }
    public void UpdateState() {
        GalagaBus.GetBus().ProcessEvents();
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
        if (key == KeyboardKey.Enter) {
            if (menuButtons[activeMenuButton].Equals(menuButtons[0])) {
                GameEvent quit = new GameEvent();
                quit.EventType = GameEventType.WindowEvent;
                quit.Message = "QUIT";
                GalagaBus.GetBus().RegisterEvent(quit);
            }
            if (menuButtons[activeMenuButton].Equals(menuButtons[1])) {
                GameEvent returnToMenu = new GameEvent();
                returnToMenu.EventType = GameEventType.GameStateEvent;
                returnToMenu.Message = "MAIN_MENU";
                GalagaBus.GetBus().RegisterEvent(returnToMenu);
                activeMenuButton = 1;
                GameRunning.GetInstance().ResetState();
            }
        }
        if (key == KeyboardKey.Down) {
                activeMenuButton = Math.Min(activeMenuButton + 1, maxMenuButtons);
        }
        if (key == KeyboardKey.Up) {
                activeMenuButton = Math.Max(activeMenuButton - 1, minMenuButtons);
        }
    }
}