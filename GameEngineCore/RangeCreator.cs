using System.Collections.Generic;

namespace GameEngineCore
{
    public static class RangeCreator
    {
        public static List<(int from, int to)> GetRanges(int numberOfEntities, int numberOfThreads)
        {
            int rangeSize = numberOfEntities / numberOfThreads;

            var ranges = new List<(int from, int to)>();
            int from = 0;
            for (int i = 0; i < numberOfThreads; ++i)
            {
                int to = from + rangeSize;
                ranges.Add((from, to));
                from = to;
            }

            if (ranges[numberOfThreads - 1].to < numberOfEntities)
            {
                var range = ranges[numberOfThreads - 1];
                range.to = numberOfEntities;
                ranges[numberOfThreads - 1] = range;
            }

            return ranges;
        }
    }
}