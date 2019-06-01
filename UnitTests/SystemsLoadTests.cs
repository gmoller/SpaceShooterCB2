using System;
using GameEngineCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
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
            Vector2 size = new Vector2(16.0f, 16.0f) * 2.5f;
            for (int i = 0; i < 100000; ++i)
            {
                Registrar.Instance.AddPlayerEntity(
                    null,
                    new Vector2(50.0f, 600.0f),
                    size,
                    new Rectangle(0, 0, 16, 16),
                    0.0f,
                    new Vector2(0.0f, 0.0f));
            }

            for (int i = 1; i < 33; ++i)
            {
                RunSystem(i);
            }
        }

        private void RunSystem(int numberOfThreads)
        {
            var system = new MovementSystem("Movement");
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