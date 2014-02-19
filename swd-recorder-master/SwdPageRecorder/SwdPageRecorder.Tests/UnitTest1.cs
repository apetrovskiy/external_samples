using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;


namespace SwdPageRecorder.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Application app = Application.Launch(@"D:\projects_current\swd-recorder\Bin\SwdPageRecorder.UI.exe");
            var mainWin = app.GetWindows().First( w => w.Title.StartsWith("SWD"));

            var button = mainWin.Get<Button>(SearchCriteria.ByText("Start"));

            button.RaiseClickEvent();

            var waitingIndicator = mainWin.Get<Label>("lblLoadingInProgress");

            UglySleep(1000);

            while (waitingIndicator.Visible)
            {
                System.Threading.Thread.Sleep(100);
            }

            app.Close();

            

        }

        private static void UglySleep(int miliseconds)
        {
            System.Threading.Thread.Sleep(miliseconds);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

    }
}
