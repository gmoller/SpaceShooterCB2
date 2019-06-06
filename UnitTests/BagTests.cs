using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngineCore;

namespace UnitTests
{
    [TestClass]
    public class BagTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var bag = new Bag<float>();
            bag[4] = 4.0f;

            Assert.AreEqual(bag[4], 4.0f);
        }
    }
}