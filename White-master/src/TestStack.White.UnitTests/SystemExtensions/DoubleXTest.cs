using TestStack.White.SystemExtensions;
using Xunit;

namespace TestStack.White.UnitTests.SystemExtensions
{
    public class DoubleXTest
    {
        [Fact]
        public void IsInvalid()
        {
            Assert.Equal(true, double.NegativeInfinity.IsInvalid());
            Assert.Equal(true, double.PositiveInfinity.IsInvalid());
            Assert.Equal(true, double.NaN.IsInvalid());
            Assert.Equal(false, 0d.IsInvalid());
        }
    }
}