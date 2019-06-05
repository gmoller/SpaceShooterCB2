using GameEngineCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class RangeCreatorTests
    {
        [TestMethod]
        public void Test_100000_5()
        {
            var ranges = RangeCreator.GetRanges(100000, 5);

            Assert.AreEqual(5, ranges.Count);

            Assert.AreEqual(0, ranges[0].from);
            Assert.AreEqual(20000, ranges[0].to);

            Assert.AreEqual(20000, ranges[1].from);
            Assert.AreEqual(40000, ranges[1].to);

            Assert.AreEqual(40000, ranges[2].from);
            Assert.AreEqual(60000, ranges[2].to);

            Assert.AreEqual(60000, ranges[3].from);
            Assert.AreEqual(80000, ranges[3].to);

            Assert.AreEqual(80000, ranges[4].from);
            Assert.AreEqual(100000, ranges[4].to);
        }

        [TestMethod]
        public void Test_100000_2()
        {
            var ranges = RangeCreator.GetRanges(100000, 2);

            Assert.AreEqual(2, ranges.Count);

            Assert.AreEqual(0, ranges[0].from);
            Assert.AreEqual(50000, ranges[0].to);

            Assert.AreEqual(50000, ranges[1].from);
            Assert.AreEqual(100000, ranges[1].to);
        }

        [TestMethod]
        public void Test_100001_2()
        {
            var ranges = RangeCreator.GetRanges(100001, 2);

            Assert.AreEqual(2, ranges.Count);

            Assert.AreEqual(0, ranges[0].from);
            Assert.AreEqual(50000, ranges[0].to);

            Assert.AreEqual(50000, ranges[1].from);
            Assert.AreEqual(100001, ranges[1].to);
        }

        [TestMethod]
        public void Test_99999_2()
        {
            var ranges = RangeCreator.GetRanges(99999, 2);

            Assert.AreEqual(2, ranges.Count);

            Assert.AreEqual(0, ranges[0].from);
            Assert.AreEqual(49999, ranges[0].to);

            Assert.AreEqual(49999, ranges[1].from);
            Assert.AreEqual(99999, ranges[1].to);
        }

        [TestMethod]
        public void Test_999_1()
        {
            var ranges = RangeCreator.GetRanges(999, 1);

            Assert.AreEqual(1, ranges.Count);

            Assert.AreEqual(0, ranges[0].from);
            Assert.AreEqual(999, ranges[0].to);
        }

        [TestMethod]
        public void Test_16_5()
        {
            var ranges = RangeCreator.GetRanges(16, 5);

            Assert.AreEqual(5, ranges.Count);

            Assert.AreEqual(0, ranges[0].from);
            Assert.AreEqual(3, ranges[0].to);

            Assert.AreEqual(3, ranges[1].from);
            Assert.AreEqual(6, ranges[1].to);

            Assert.AreEqual(6, ranges[2].from);
            Assert.AreEqual(9, ranges[2].to);

            Assert.AreEqual(9, ranges[3].from);
            Assert.AreEqual(12, ranges[3].to);

            Assert.AreEqual(12, ranges[4].from);
            Assert.AreEqual(16, ranges[4].to);
        }

    }
}