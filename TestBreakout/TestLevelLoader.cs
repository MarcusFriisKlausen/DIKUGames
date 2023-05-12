// using NUnit.Framework;
// using Breakout;
// using Breakout.BreakoutStates;
// using DIKUArcade.Math;
// using DIKUArcade.Entities;
// using DIKUArcade.Graphics;
// using DIKUArcade.Events;
// using DIKUArcade.Input;
// using DIKUArcade.Utilities;
// using System.Collections.Generic;


// namespace TestLevelLoader;
//     [TestFixture]
//     public class Tests{
//         LevelLoader levelLoader;
//         StreamReader level;
        
//         [SetUp]
//         public void Setup(){
//             DIKUArcade.GUI.Window.CreateOpenGLContext();

//             levelLoader = new LevelLoader();

//             level = new StreamReader(
//                 Path.Combine(Constants.MAIN_PATH, "Assets","Levels","level1.txt"));
//         }

<<<<<<< Updated upstream:TestBreakout/TestLevelLoader.cs
        [Test]
        // Testing if LevelMaker can make a map correctly from a file, 
        // checking the first and last block
        public void testCanMakeosLosMapos() {
            List<Entity> blockLst = new List<Entity>();
=======
//         [Test]
//         // Testing if LevelMaker can make a map correctly from a file, 
//         // checking the first and last block
//         public void TestCanMakeosLosMapos() {
//             List<Entity> blockLst = new List<Entity>();
>>>>>>> Stashed changes:BreakoutTests/LevelLoaderTests.cs

//             foreach (Entity block in levelLoader.LevelMaker(level)) {
//                 blockLst.Add(block);
//             }
//             //Checking the first block
//             Assert.That
//                 (blockLst[0].GetType(), Is.EqualTo((new PurpleBlock(
//                     new DynamicShape(
//                         new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
//                         new Vec2F(1f/12f, 1f/25f)), new Image(
//                             Path.Combine(
//                                 "Assets", "Images", "deep-bronze-square.png")))).GetType()));
//             //Checking the last block
//             Assert.That
//                 (blockLst[blockLst.Count - 1].GetType(), Is.EqualTo((new BlueBlock(
//                     new DynamicShape(
//                         new Vec2F(((1f/12f)*(float)0), (1f - (float)0/25f)),
//                         new Vec2F(1f/12f, 1f/25f)), new Image(
//                             Path.Combine(
//                                 "Assets", "Images", "deep-bronze-square.png")))).GetType()));
            
//             //Checking the first blocks position
//             Assert.That(blockLst[0].Shape.Position.X, Is.EqualTo((1f/12f)*(float)1));
//             Assert.That(blockLst[0].Shape.Position.Y, Is.EqualTo(1f - (float)0/25f));
//             //Checking the last blocks position
//             Assert.That(blockLst[blockLst.Count-1].Shape.Position.X, 
//                 Is.EqualTo((((1f/12f)*10f))));
//             Assert.That(blockLst[blockLst.Count-1].Shape.Position.Y, 
//                 Is.EqualTo(1f - (float)(11)/25f));
//         }
//     }
    