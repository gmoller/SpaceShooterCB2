using System;
using AnimationLibrary;
using GameEngineCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SpaceShooterLogic;
using SpaceShooterLogic.Systems;
using SpaceShooterUtilities;

namespace UnitTests
{
    [TestClass]
    public class SystemsLoadTests
    {
        [TestMethod]
        public void TestMovementSystem()
        {
            var state = new GameState();

            for (int i = 0; i < 100000; ++i)
            {
                state.Positions[i] = new Vector2(50.0f, 600.0f);
                state.Velocities[i] = new Vector2(0.0f, 0.0f);
                state.Volumes[i] = new Rectangle(0, 0, 16, 16);

                Registrar.Instance.EntityCount++;
            }

            for (int i = 1; i < 33; ++i)
            {
                var system = new MovementSystem("Movement", state);
                RunSystem(system, i);
            }
        }

        [TestMethod]
        public void TestAnimationSystem()
        {
            var state = new GameState();

            for (int i = 0; i < 100000; ++i)
            {
                state.AnimationData[i] = new AnimationData(AnimationSpecCreator.Create("Test", 64, 16, 16, 16, 160, true), 0, 0.0f);

                Registrar.Instance.EntityCount++;
            }

            var system = new AnimationSystem("Animation", state);
            RunSystem(system, 1);
        }

        private void RunSystem(SpaceShooterLogic.Systems.System system, int numberOfThreads)
        {
            for (int i = 0; i < 60; ++i)
            {
                system.Process(16.0f, numberOfThreads);
            }

            foreach (var metric in BenchmarkMetrics.Instance.Metrics)
            {
                double avg = metric.Value._elapsedTime / metric.Value._frames;
                Console.WriteLine($"Threads: {numberOfThreads} - {metric.Key}: {metric.Value._elapsedTime} ms, Frames: {metric.Value._frames}, Average: {avg:N} ms/frame.");
            }
        }
    }
}