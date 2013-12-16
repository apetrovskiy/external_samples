using Microsoft.SharePoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Typemock.Examples.Sharepoint
{
    /// <summary>
    /// This class tests SPUser related functionality
    /// </summary>
    [TestClass, Isolated(DesignMode.Pragmatic)]
    public class SPUserTests
    {
        private SharePointLogic classUnderTest;

        /// <summary>
        /// Initialize the Tests.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            classUnderTest = new SharePointLogic();
        }

        [TestMethod]
        public void CreateUserIfDoesNotExist_CallWithUserThatDoesNotExist_NewUserCreated()
        {
             var fakeSite = Isolate.Swap.NextInstance<SPSite>().WithRecursiveFake();

            // Here we explicitly tell the collection to return null - user was not found
            Isolate.WhenCalled(() => fakeSite.OpenWeb().AllUsers["typemockUser"]).WillReturn(null);


            classUnderTest.CreateUserIfDoesNotExist("www.sharepoint.typemock.com", "typemockUser", "user@typemock.com", "user", string.Empty);

            Isolate.Verify.WasCalledWithAnyArguments(() => fakeSite.OpenWeb().Update());
        }
        
        [TestMethod]
        public void CreateUserIfDoesNotExist_CallWithUserThatAlreadyExist_UserNotCreated()
        {
            var fakeSite = Isolate.Swap.NextInstance<SPSite>().WithRecursiveFake();
            
            // Because we are using recursive fakes we do not need to specify return value for SPUser
            // It would be automatically created

            classUnderTest.CreateUserIfDoesNotExist("www.sharepoint.typemock.com", "typemockUser", "user@typemock.com", "user", string.Empty);

            Isolate.Verify.WasNotCalled(() => fakeSite.OpenWeb().Update());
        }

    }
}
