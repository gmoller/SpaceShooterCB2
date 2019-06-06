using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using AnimationLibrary;
using SpaceShooterLogic;
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
                state.Positions[i] = new Vector2(50.0f, 600.0f);
                state.Velocities[i] = new Vector2(0.0f, 0.0f);
                state.Sizes[i] = new Vector2(16.0f, 16.0f);
                state.Tags[i] = 9;

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
        public void TestDestroyIfOutsideViewportSystem()
        {
            DeviceManager.Instance.Viewport = new Rectangle(0, 0, 480, 640);

            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Positions[i] = new Vector2(50.0f, 600.0f);
                state.Sizes[i] = new Vector2(16.0f, 16.0f);
                state.Tags[i] = 17;

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
        public void TestFireProjectileSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Positions[i] = new Vector2(50.0f, 600.0f);
                state.TimesSinceLastShot[i] = 1000.0f;
                state.Tags[i] = 1;

                state.EntityCount++;
            }

            Console.WriteLine($"Number of entities: {state.EntityCount}");
            for (int i = 1; i < 33; ++i)
            {
                var system = new FireProjectileSystem("FireProjectile", state);
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
                state.Positions[i] = new Vector2(50.0f, 600.0f);
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
        public void TestPlayerInputSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Velocities[i] = new Vector2(0.0f, 0.0f);
                state.Tags[i] = 3;

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
        public void TestSetBoundingBoxSystem()
        {
            var state = new GameState();

            for (int i = 0; i < NUMBER_OF_ENTITIES; ++i)
            {
                state.Positions[i] = new Vector2(50.0f, 600.0f);
                state.Velocities[i] = new Vector2(1.0f, 1.0f);
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
                state.Positions[i] = new Vector2(50.0f, 600.0f);
                state.Sizes[i] = new Vector2(16.0f, 16.0f);

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