using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using GameEngineCore;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic;
using SpaceShooterLogic.Components;
using SpaceShooterLogic.Systems;
using SpaceShooterUtilities;

namespace UnitTests
{
    [TestClass]
    public class SystemsLoadTests
    {
        private const int NUMBER_OF_ENTITIES = 100000;

        [TestMethod]
        public void TestAnimationSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.AnimationData[i] = new AnimationData(AnimationSpecCreator.Create("Test", 64, 16, 16, 16, 160, true), 0, 0.0f);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new AnimationSystem("Animation", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestClampToViewportSystem()
        {
            DeviceManager.Instance.Viewport = new Rectangle(0, 0, 480, 640);

            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(2.5f), new Vector2(16.0f, 16.0f));
                state.Players[i] = new Player(0, 3);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new ClampToViewportSystem("ClampToViewport", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestCollisionResolutionSystem()
        {
            Texture2D tex = null;
            SoundEffect snd = null;
            AssetsManager.Instance.AddTexture("Explosion10", tex);
            AssetsManager.Instance.AddAnimation("Explosion10", null);
            AssetsManager.Instance.AddSound("sndExplode0", snd);
            AssetsManager.Instance.AddSound("sndExplode1", snd);
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(2.5f), new Vector2(16.0f, 16.0f));
                state.Players[i] = new Player(0, 3);
                state.Enemies[i] = new Enemy(EnemyType.Carrier, 5);
                state.Tags[i] = 1 + 2;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new CollisionResolutionSystem("CollisionResolution", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestDestroyIfOutsideViewportSystem()
        {
            DeviceManager.Instance.Viewport = new Rectangle(0, 0, 480, 640);

            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(1.0f), new Vector2(16.0f, 16.0f));
                state.Enemies[i] = new Enemy(EnemyType.Carrier, 5);
                state.Tags[i] = 1 + 4;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new DestroyIfOutsideViewportSystem("DestroyIfOutsideViewport", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestEnemyChaseSystem()
        {
            var state = new GameState();

            state.Players[0] = new Player(0, 3);
            state.Transforms[0] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(1.0f), new Vector2(16.0f, 16.0f));
            for (int i = 1; i < NUMBER_OF_ENTITIES + 1; ++i)
            {
                state.Transforms[i] = new Transform(new Vector2(50.0f, 300.0f), 0.0f, Vector2.One, new Vector2(16.0f, 16.0f));
                state.Enemies[i] = new Enemy(EnemyType.Chaser, 10);
                state.Transforms[i] = new Transform(new Vector2(50.0f, 300.0f), 0.0f, new Vector2(1.0f), new Vector2(16.0f, 16.0f));
                state.Velocities[i] = new Vector2(1.0f, 1.0f);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new EnemyChaseSystem("EnemyChase", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestEnemyFireProjectileSystem()
        {
            Texture2D tex = null;
            SoundEffect snd = null;
            AssetsManager.Instance.AddTexture("sprLaserEnemy0", tex);
            AssetsManager.Instance.AddSound("sndLaser", snd);
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(1.0f), new Vector2(16.0f, 16.0f));
                state.Enemies[i] = new Enemy(EnemyType.Gunship, 20);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new EnemyFireProjectileSystem("EnemyFireProjectile", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestEnemySpawnSystem()
        {
            Texture2D tex = null;
            AssetsManager.Instance.AddTexture("sprEnemy0", tex);
            AssetsManager.Instance.AddTexture("sprEnemy1", tex);
            AssetsManager.Instance.AddTexture("sprEnemy2", tex);
            AssetsManager.Instance.AddAnimation("sprEnemy0", null);
            AssetsManager.Instance.AddAnimation("sprEnemy1", null);
            AssetsManager.Instance.AddAnimation("sprEnemy2", null);
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.EnemySpawner[i] = new EnemySpawner();
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new EnemySpawnSystem("EnemySpawn", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestIsGameOverSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Players[i] = new Player(0, 3);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new IsGameOverSystem("IsGameOver", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestMovementSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(1.0f), new Vector2(16.0f, 16.0f));
                state.Velocities[i] = new Vector2(1.0f, 1.0f);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new MovementSystem("Movement", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestPlayerFireProjectileSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Players[i] = new Player(0, 3);
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(1.0f), new Vector2(16.0f, 16.0f));
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new PlayerFireProjectileSystem("FireProjectile", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestPlayerInputSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Players[i] = new Player(0, 3);
                state.Velocities[i] = new Vector2(0.0f, 0.0f);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new PlayerInputSystem("PlayerInput", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestRestorePlayerSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(2.5f), new Vector2(16.0f, 16.0f));
                state.Players[i] = new Player(0, 3);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new RestorePlayerSystem("RestorePlayer", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        public void TestSetBoundingBoxSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(1.0f), new Vector2(16.0f, 16.0f));
                state.Volumes[i] = new Rectangle(0, 0, 16, 16);
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new SetBoundingBoxSystem("SetBoundingBox", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        [TestMethod]
        [Ignore("Test incomplete - no textures added.")]
        public void TestRenderingSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                //state.Textures[i] = 
                state.Transforms[i] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(1.0f), new Vector2(16.0f, 16.0f));

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new RenderingSystem("Rendering", state);
                RunSystem(system, i);
            }
            state.ClearState();
        }

        private void RunSystem(SpaceShooterLogic.Systems.System system, int numberOfThreads)
        {
            for (int i = 0; i < 60; ++i)
            {
                system.Process(16.0f, numberOfThreads);
            }

            foreach (var metric in system.GameState.Metrics)
            {
                double avg = metric.Value.ElapsedTime / metric.Value.Frames;
                Console.WriteLine($"Threads: {numberOfThreads} - {metric.Key}: {metric.Value.ElapsedTime} ms, Frames: {metric.Value.Frames}, Average: {avg:N} ms/frame.");
            }
            system.GameState.Metrics.Clear();
        }
    }
}