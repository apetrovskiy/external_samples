using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecflowPlayground.Tags
{
    [Scope(Tag = "scope1")]
    [Binding]
    public class ScopedBindingSteps1
    {
        private string _tag;

        [Given(@"a binding scoped by tag")]
        public void GivenScope1()
        {
            _tag = "scope1";
        }

        [Then(@"(.*) should be saved")]
        public void ThenScope1(string expectedScope)
        {
            Assert.AreEqual(expectedScope, _tag);
        }
    }

    [Scope(Tag = "scope2")]
    [Binding]
    public class ScoopedBindingSteps2
    {
        private string _tag;

        [Given(@"a binding scoped by tag")]
        public void GivenScope2()
        {
            _tag = "scope2";
        }

        [Then(@"(.*) should be saved")]
        public void ThenScope2(string expectedScope)
        {
            Assert.AreEqual(expectedScope, _tag);
        }
    }
}
