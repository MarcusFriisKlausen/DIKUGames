using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;

namespace Galaga;
public class Health {
    private int health;
    public int HEALTH {
        get { return health; }
    }
    public bool gameOver = false;
    public Text display;
    public Health (Vec2F position, Vec2F extent) {
        health = 3;
        display = new Text ("HEALTH: " + (health.ToString()), position, extent);
        display.SetColor(255, 255, 255, 255);
    }
    // Remember to explaination your choice as to what happens
    //when losing health.
    public void LoseHealth () {
        health--;
        
        if (health <= 0) {
            gameOver = true;
        } 
    }

    public void RenderHealth () {
        if (health >= 0){
            display.RenderText();
        }
    }

    public override string ToString() {
        return health.ToString();
    }
}