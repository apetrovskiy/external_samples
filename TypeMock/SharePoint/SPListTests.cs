using System;
using Microsoft.SharePoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using Typemock;

namespace Typemock.Examples.Sharepoint
{
    /// <summary>
    /// This class tests SPList related functionality
    /// </summary>
    [TestClass, Isolated(DesignMode.Pragmatic)]
    public class SPListTests
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

        /// <summary>
        /// Tests SharePointLogic.GetAllListsThatWereModifiedSince function. 
        /// This function returns all of the lists that were updated after a specific date.
        /// 
        /// In This test we expect to recieve one list from the function with title "News".
        /// </summary>
        [TestMethod]
        public void GetAllListsThatWereModifiedSince_OneListOnTheSiteHasBeenUpdated_TheUpdatedListIsFound()
        {
            var weekBeforeToday = DateTime.Now.AddDays(-7);

            // Recursive mocks - SPSite and all its nested classes are faked
            var fakeSite = Isolate.Fake.Instance<SPSite>();

            // Define one item in index 5 that has been modified a week before today.
            // Isolator will automatically create a fake list with 6 items in it 
            Isolate.WhenCalled(() => fakeSite.OpenWeb().Lists[5].LastItemModifiedDate).WillReturn(weekBeforeToday);
            Isolate.WhenCalled(() => fakeSite.OpenWeb().Lists[5].Title).WillReturn("News");

            // Call the function under test
            var listsThatWereModifiedLastWeek = classUnderTest.GetAllListsThatWereModifiedSince(fakeSite, weekBeforeToday);

            Assert.AreEqual(1, listsThatWereModifiedLastWeek.Count);
            Assert.AreEqual("News", listsThatWereModifiedLastWeek[0].Title);
        }


        /// <summary>
        /// Tests SharePointLogic.GetAllTasks function. 
        /// This function returns all items in the Task list.
        /// 
        /// In This test we expect to recieve three new items.
        /// </summary>
        [TestMethod]
        public void GetAllTasks_ThreeItemsInTasksList_ThreeTaskItemsFound()
        {
            
            // Swap future instance of SPSite
            var fakeSite = Isolate.Swap.NextInstance<SPSite>().WithRecursiveFake();

            var fakeTaskList = fakeSite.OpenWeb().Lists[SharePointLogic.TASKS_LIST_NAME];
            var fakeItem = Isolate.Fake.Instance<SPListItem>();
            
            // List collection shall return three fake items
            Isolate.WhenCalled(() => fakeTaskList.Items).WillReturnCollectionValuesOf(
                new[] {fakeItem, fakeItem, fakeItem});

            // Call the function under test
            var urgentTasks = classUnderTest.GetAllTasks();

            Assert.AreEqual(3, urgentTasks.Count);
        }


        /// <summary>
        /// Tests SharePointLogic.GetUrgentTasks() 
        /// that returns all of the items in the "Tasks" list that have urgent priority 
        /// 
        /// In this test we expect to recieve two items (tasks)
        /// </summary>
        [TestMethod]
        public void GetUrgentTasks_QuerySiteForAllTasksAndReturnTheNamesOfTheUrgentTasks_TwoUrgentTasksFound()
        {
            // Swap future instance of SPSite
            var fakeSite = Isolate.Swap.NextInstance<SPSite>().WithRecursiveFake();
            var fakeTaskList = fakeSite.OpenWeb().Lists[SharePointLogic.TASKS_LIST_NAME];

            // Isolator will automatically create a fake list with 10 items in it 
            // Two of those items (1 and 9) have "Urgent" Priority
            string urgentPriorityString = SharePointLogic.Priority.Urgent.ToString();
            var priorityFieldName = SharePointLogic.PriorityFieldName;

            Isolate.WhenCalled(() => fakeTaskList.Items[1][priorityFieldName]).WillReturn(urgentPriorityString);
            Isolate.WhenCalled(() => fakeTaskList.Items[1].Name).WillReturn("Do the laundry");
            Isolate.WhenCalled(() => fakeTaskList.Items[3][priorityFieldName]).WillReturn(SharePointLogic.Priority.Medium.ToString());
            Isolate.WhenCalled(() => fakeTaskList.Items[3].Name).WillReturn("Walk the dog"); 
            Isolate.WhenCalled(() => fakeTaskList.Items[9][priorityFieldName]).WillReturn(urgentPriorityString);
            Isolate.WhenCalled(() => fakeTaskList.Items[9].Name).WillReturn("Wash the dishes");

            // Call the function under test
            var urgentTasks = classUnderTest.GetUrgentTasks();

            Assert.AreEqual(2, urgentTasks.Count);
            Assert.AreEqual("Do the laundry", urgentTasks[0]);
            Assert.AreEqual("Wash the dishes", urgentTasks[1]);
        }
    }
}
