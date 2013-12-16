using TestStack.White.AutomationElementSearch;
using TestStack.White.UIItems.Finders;
using Xunit;

namespace TestStack.White.UnitTests.AutomationElementSearch
{
    public class SearchConditionTest
    {
        [Fact]
        public void TestToString()
        {
            const string name = "blah";
            string @string = AutomationSearchCondition.ByName(name).ToString();
            Assert.True(@string.Contains(name), @string);
        }

        [Fact]
        public void EqualsTests()
        {
            Assert.Equal(SearchConditionFactory.CreateForAutomationId("foo"), SearchConditionFactory.CreateForAutomationId("foo"));
            Assert.NotEqual(SearchConditionFactory.CreateForAutomationId("foo"), SearchConditionFactory.CreateForAutomationId("foo1"));
            Assert.NotEqual(SearchConditionFactory.CreateForAutomationId("foo"), SearchConditionFactory.CreateForName(("foo")));
        }
    }
}