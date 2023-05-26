using NUnit.Framework;
using Breakout;
using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Timers;

public class HealthTest {
    Health HP;
    [SetUp]
    public void SetUp(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        HP = new Health();

    }

    [Test]
    public void LoseHealthTest(){
        int exp = 2; // health = 3 - 1 = 2
        HP.LoseHealth();
        int res = HP.Value;
        Assert.AreEqual(res, exp);
    }

    [Test]
    public void GainHealthTest(){
        int exp = 3; // health = 3 - 1 + 1 + 1 = 3
        HP.LoseHealth();
        // Make sure it cannot go above 3 health
        HP.GainHealth();
        HP.GainHealth();
        int res = HP.Value;
        Assert.AreEqual(res, exp);
    }
    
}