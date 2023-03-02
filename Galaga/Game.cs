using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
namespace Galaga;
public class Game : DIKUGame {
    private Player player;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
        new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
        new Image(Path.Combine("Assets", "Images", "Player.png")));
    }

    public override void Render() {
        player.Render();
    }

    public override void Update() {
        throw new System.NotImplementedException("Galaga game has no entities to update yet.");
    }
}