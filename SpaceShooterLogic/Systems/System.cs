using System.Diagnostics;
using System.Threading.Tasks;
using GameEngineCore;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public abstract class System
    {
        private readonly Stopwatch _stopwatchProcess = new Stopwatch();
        private int _frames;

        private readonly string _name;

        protected GameState GameState;

        protected System(string name, GameState gameState)
        {
            _name = name;
            GameState = gameState;
        }

        public void Process(float deltaTime, int numberOfThreads = 1)
        {
            _frames++;

            var ranges = RangeCreator.GetRanges(Registrar.Instance.EntityCount, numberOfThreads);

            _stopwatchProcess.Start();
            var tasks = new Task[numberOfThreads];
            for (int i = 0; i < numberOfThreads; ++i)
            {
                var range = ranges[i];
                var task = Task.Run(() => ProcessBatch(range.from, range.to, deltaTime));
                tasks[i] = task;
            }

            Task.WaitAll(tasks);
            _stopwatchProcess.Stop();

            BenchmarkMetrics.Instance.Metrics[$"System:{_name}.Process"] = new Metric(_stopwatchProcess.Elapsed.TotalMilliseconds, _frames);
        }

        private void ProcessBatch(int fromInclusive, int toExclusive, float deltaTime)
        {
            for (int entityId = fromInclusive; entityId < toExclusive; ++entityId)
            {
                ProcessOneEntity(entityId, deltaTime);
            }
        }

        protected abstract void ProcessOneEntity(int entityId, float deltaTime);
    }
}