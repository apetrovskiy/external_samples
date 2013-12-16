using System;
using System.Collections.Generic;
using Microsoft.SharePoint;

namespace Typemock.Examples.Sharepoint
{
    public class SharePointLogic
    {
        internal const string PriorityFieldName = "Priority";
        internal const string TASKS_LIST_NAME = "Tasks";

        public enum Priority
        {
            Low,
            Medium,
            High,
            Urgent,
        }

        /// <summary>
        /// Gets all lists that were modified since <see cref="lastUpdateDate"/>
        /// </summary>
        /// <param name="site">The site to test.</param>
        /// <param name="lastUpdateDate">The last update date.</param>
        /// <returns></returns>
        public List<SPList> GetAllListsThatWereModifiedSince(SPSite site, DateTime lastUpdateDate)
        {
            var result = new List<SPList>();
            foreach (SPList list in site.OpenWeb().Lists)
            {
                if (list.LastItemModifiedDate.CompareTo(lastUpdateDate) >= 0)
                {
                    result.Add(list);
                }
            }

            return result;
        }

        public List<SPListItem> GetAllTasks()
        {
            var site = new SPSite("http://sharepoint.typemock.com");

            var taskList = site.OpenWeb().Lists[TASKS_LIST_NAME];
            var urgentTasks = new List<SPListItem>();
            foreach (SPListItem item in taskList.Items)
            {
                urgentTasks.Add(item);
            }

            return urgentTasks;
        }

        /// <summary>
        /// Retrieves all of the tasks at the "tasks" list and returns the names of the tasks that are marked <see cref="Priority.Urgent"/>.
        /// </summary>
        /// <returns>Names of the Urgent tasks</returns>
        public List<string> GetUrgentTasks()
        {
            var site = new SPSite("http://sharepoint.typemock.com");

            var taskList = site.OpenWeb().Lists[TASKS_LIST_NAME];
            var urgentTasks = new List<string>();

            foreach (SPListItem item in taskList.Items)
            {
                if (item[PriorityFieldName] != null &&
                    item[PriorityFieldName].ToString() == Priority.Urgent.ToString())
                {
                    urgentTasks.Add(item.Name);
                }
            }
            return urgentTasks;
        }

        /// <summary>
        /// This function returns a SPUser object. Create the user if the user login does not exist.
        /// </summary>
        public SPUser CreateUserIfDoesNotExist(string siteUrl, string userLoginName, string userEmail, string userName, string notes)
        {
            SPUser spUser;

            //Open the ShrePoint site
            using (var spSite = new SPSite(siteUrl))
            {
                using (var spWeb = spSite.OpenWeb())
                {
                    //Check to see if user exists
                    spUser = spWeb.AllUsers[userLoginName];
                    if (spUser == null)
                    {
                        //Assign role and add user to site
                        var spRoleAssignment = new SPRoleAssignment(userLoginName, userEmail, userName, notes);

                        //Using Contribute, might need high access
                        var spSPRoleDefinition = spWeb.RoleDefinitions["Contribute"];

                        spRoleAssignment.RoleDefinitionBindings.Add(spSPRoleDefinition);
                        spWeb.RoleAssignments.Add(spRoleAssignment);

                        //Update site
                        spWeb.Update();
                        spUser = spWeb.AllUsers[userLoginName];
                    }
                }
            }

            return spUser;
        }
    }
}
