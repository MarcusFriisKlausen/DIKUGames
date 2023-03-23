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

public class GamePaused : IGameState {
    private static GamePaused instance = null;
    private Text[] menuButtons;
    private int activeMenuButton = 1;
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.ResetState();
        }
        return GamePaused.instance;
    }

    public GamePaused() {
        menuButtons = new Text[] {
                new Text("PAUSED", new Vec2F(0.35f, 0.2f), new Vec2F(0.5f, 0.5f)),
                new Text("Continue", new Vec2F(0.35f, 0.1f), new Vec2F(0.5f, 0.5f)),
            };
        menuButtons[0].SetColor(255, 255, 255, 255);
        menuButtons[activeMenuButton].SetColor(255, 1, 1, 255);
    }
    public void ResetState() {

    }

    public void RenderState() {
        for (int i = 0; i < menuButtons.Length; i++){
            menuButtons[i].RenderText();
        }
    }

    public void UpdateState() {
        GalagaBus.GetBus().ProcessEvents();
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (key == KeyboardKey.Enter) {
            GameEvent returnToGame = new GameEvent();
            returnToGame.EventType = GameEventType.GameStateEvent;
            returnToGame.Message = "GAME_RUNNING";
            GalagaBus.GetBus().RegisterEvent(returnToGame);
        }
    }
}