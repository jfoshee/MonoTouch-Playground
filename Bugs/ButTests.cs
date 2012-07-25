
using System;
using NUnit.Framework;

namespace Bugs
{
    [TestFixture]
    public class ButTests
    {
        [Test]
        public void EmptyRectangularArray()
        {
            var bytes = new byte[,]{};
        }
    }
}
