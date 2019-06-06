using System.Collections;
using System.Collections.Generic;

namespace GameEngineCore
{
    public class BenchmarkMetrics : IEnumerable<KeyValuePair<string, Metric>>
    {
        private readonly Dictionary<string, Metric> _metrics;

        public BenchmarkMetrics()
        {
            _metrics = new Dictionary<string, Metric>();
        }

        public Metric this[string index]
        {
            get => _metrics[index];
            set => _metrics[index] = value;
        }

        public void Clear()
        {
            _metrics.Clear();
        }

        public IEnumerator<KeyValuePair<string, Metric>> GetEnumerator()
        {
            foreach (var item in _metrics)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct Metric
    {
        public double ElapsedTime { get; }
        public int Frames { get; }

        public Metric(double elapsedTime, int frames)
        {
            ElapsedTime = elapsedTime;
            Frames = frames;
        }
    }
}