using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic;
using SpaceShooterLogic.Creators;
using SpaceShooterLogic.Systems;
using SpaceShooterUtilities;

namespace UnitTests
{
    [TestClass]
    public class SystemsTests
    {
        [TestMethod]
        public void TestRenderingSystem()
        {
            Texture2D tex = null;
            AssetsManager.Instance.AddTexture("sprEnemy2", tex);
            AssetsManager.Instance.AddAnimation("sprEnemy2", null);

            var state = new GameState();
            const int number = 100000;
            for (int i = 0; i < number; ++i)
            {
                EnemyCreator.Create2(new Vector2(100.0f, 100.0f), new Vector2(0.0f, 10.0f), state);
                EnemySpawnerCreator.Create2(state);
                TransformOnlyCreator.Create2(state);
                TextureOnlyCreator.Create2(state);
            }
            var system = new RenderingSystem2();

            Console.WriteLine($"Number of entities: {state.GameData2.EntityCount/4:N0}");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            RenderingSystem2.Process(state, state.GameData2.Transform, state.GameData2.Texture, state.GameData2.Volume);
            stopwatch.Stop();

            Assert.AreEqual(number, state.SpriteBatchList.Count());

            Console.WriteLine($"RenderingSystem2 time taken: {stopwatch.Elapsed.TotalMilliseconds}");
        }
    }
}