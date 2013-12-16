using TestStack.White.UIItems.Finders;
using Xunit;

namespace TestStack.White.UnitTests.UIItems.Finders
{
    public class IndexConditionTest
    {
        [Fact]
        public void TestEquals()
        {
            Assert.Equal(new IndexCondition(1), new IndexCondition(1));
            Assert.NotEqual(new IndexCondition(1), new IndexCondition(2));
            Assert.Equal(new IndexCondition(-1), new IndexCondition(-1));
        }
    }
}