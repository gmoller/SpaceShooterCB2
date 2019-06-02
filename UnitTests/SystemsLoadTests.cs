using System;
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
                state.Positions[Registrar.Instance.EntityCount] = new Vector2(50.0f, 600.0f);
                state.Velocities[Registrar.Instance.EntityCount] = new Vector2(0.0f, 0.0f);
                state.Volumes[Registrar.Instance.EntityCount] = new Rectangle(0, 0, 16, 16);

                Registrar.Instance.EntityCount++;
            }

            for (int i = 1; i < 33; ++i)
            {
                RunSystem(i, state);
            }
        }

        private void RunSystem(int numberOfThreads, GameState state)
        {
            var system = new MovementSystem("Movement", state);
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