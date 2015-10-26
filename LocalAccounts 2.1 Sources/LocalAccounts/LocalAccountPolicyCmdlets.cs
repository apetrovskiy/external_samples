using System.Management.Automation;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.DirectoryServices;
using System;

namespace LocalAccounts
{
    #region GetLocalAccountPolicy
    [Cmdlet(VerbsCommon.Get, "LocalAccountPolicy")]
    public class GetLocalAccountPolicy : UserCmdletBase
    {
        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                UserPrincipal user = null;

                var filter = new UserPrincipal(ctx);
                var searcher = new PrincipalSearcher();
                PrincipalSearchResult<Principal> result;

                searcher.QueryFilter = filter;
                result = searcher.FindAll();

                user = (UserPrincipal)result.Where(p => p.SamAccountName.ToLower() == name.ToLower()).First();
                var policy = new LocalAccountPolicy(user);

                this.WriteObject(policy);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "ReadError", ErrorCategory.ReadError, ctx);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    public class LocalAccountPolicy
    {
        public int MinPasswordLength { get; set; }
        public int MaxPasswordAge { get; set; }
        public int MinPasswordAge { get; set; }
        public int PasswordHistoryLength { get; set; }
        public int LockoutObservationInterval { get; set; }
        public int MaxBadPasswordsAllowed { get; set; }
        public int AutoUnlockInterval { get; set; }

        public LocalAccountPolicy(UserPrincipal user)
        {
            var userObject = (DirectoryEntry)user.GetUnderlyingObject();

            this.AutoUnlockInterval = int.Parse(userObject.Properties["AutoUnlockInterval"].Value.ToString()) / 60;
            this.LockoutObservationInterval = int.Parse(userObject.Properties["LockoutObservationInterval"].Value.ToString()) / 60;
            this.MaxBadPasswordsAllowed = int.Parse(userObject.Properties["MaxBadPasswordsAllowed"].Value.ToString());
            this.MaxPasswordAge = int.Parse(userObject.Properties["MaxPasswordAge"].Value.ToString()) / 86400;
            this.MinPasswordAge = int.Parse(userObject.Properties["MinPasswordAge"].Value.ToString()) / 86400;
            this.MinPasswordLength = int.Parse(userObject.Properties["MinPasswordLength"].Value.ToString());
            this.PasswordHistoryLength = int.Parse(userObject.Properties["PasswordHistoryLength"].Value.ToString());
        }
    }
}