using System.Management.Automation;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.DirectoryServices;
using System;

namespace LocalAccounts
{
    #region UserCmdletBase
    public class UserCmdletBase : Base
    {
        protected UserPrincipal user = null;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }

        protected UserPrincipal GetUserByName(string name)
        {
            var user = UserPrincipal.FindByIdentity(ctx, name);

            if (user == null)
            {
                throw new NoMatchingPrincipalException("The user could not be found");
            }

            return user;
        }
    }
    #endregion

    #region GetLocalUser
    [Cmdlet(VerbsCommon.Get, "LocalUser", DefaultParameterSetName = "ByName")]
    public class GetLocalUser : UserCmdletBase
    {
        private new string name;
        private SwitchParameter all;

        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "ByName")]
        [Alias("SamAccountName")]
        public new string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Parameter(ParameterSetName = "All")]
        public SwitchParameter All
        {
            get { return all; }
            set { all = value; }
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                var filter = new UserPrincipal(ctx);
                var searcher = new PrincipalSearcher();
                PrincipalSearchResult<Principal> result;

                searcher.QueryFilter = filter;
                result = searcher.FindAll();

                if (this.ParameterSetName == "All")
                {
                    result.ForEach(r => this.WriteObject(r));
                }
                else
                {
                    this.WriteObject(result.Where(p => p.SamAccountName.ToLower() == name.ToLower()).First());
                }
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "ReadError", ErrorCategory.ReadError, ctx);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region NewLocalUser
    [Cmdlet(VerbsCommon.New, "LocalUser", DefaultParameterSetName = "ChangePassword")]
    public class NewLocalUser : UserCmdletBase
    {
        private string fullName;
        private SwitchParameter passThru;
        private string description;
        private string password;
        private SwitchParameter mustChangePasswordAtNextLogon;
        private SwitchParameter cannotChangePassword;
        private SwitchParameter isDisabled;
        private SwitchParameter passwordNeverExpires;
        private string profilePath;
        private string logonScript;
        private string homeDirectoryDrive;
        private string homeDirectory;

        #region Parameters
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        [Parameter]
        public SwitchParameter PassThru
        {
            get { return passThru; }
            set { passThru = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = "ChangePassword")]
        public SwitchParameter MustChangePasswordAtNextLogon
        {
            get { return mustChangePasswordAtNextLogon; }
            set { mustChangePasswordAtNextLogon = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = "NotChangePassword")]
        public SwitchParameter CannotChangePassword
        {
            get { return cannotChangePassword; }
            set { cannotChangePassword = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = "NotChangePassword")]
        public SwitchParameter PasswordNeverExpires
        {
            get { return passwordNeverExpires; }
            set { passwordNeverExpires = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public SwitchParameter IsDisabled
        {
            get { return isDisabled; }
            set { isDisabled = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string ProfilePath
        {
            get { return profilePath; }
            set { profilePath = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string LogonScript
        {
            get { return logonScript; }
            set { logonScript = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        [ValidatePattern("[d-z]:$")]
        public string HomeDirectoryDrive
        {
            get { return homeDirectoryDrive; }
            set { homeDirectoryDrive = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string HomeDirectory
        {
            get { return homeDirectory; }
            set { homeDirectory = value; }
        }
        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                UserPrincipal user = null;

                user = new UserPrincipal(ctx);
                user.SamAccountName = name;

                if (!string.IsNullOrEmpty(description))
                {
                    user.Description = description;
                }

                if (!string.IsNullOrEmpty(fullName))
                {
                    user.DisplayName = fullName;
                }

                if (cannotChangePassword)
                {
                    user.UserCannotChangePassword = true;
                }

                if (passwordNeverExpires)
                {
                    user.PasswordNeverExpires = true;
                }

                if (isDisabled)
                {
                    user.Enabled = false;
                }
                else
                {
                    user.Enabled = true;
                }

                if (!string.IsNullOrEmpty(logonScript))
                {
                    user.ScriptPath = logonScript;
                }

                if (!string.IsNullOrEmpty(homeDirectoryDrive))
                {
                    user.HomeDrive = homeDirectoryDrive;
                }

                if (!string.IsNullOrEmpty(homeDirectory))
                {
                    user.HomeDirectory = homeDirectory;
                }

                user.Save();
                user.SetPassword(password);

                if (mustChangePasswordAtNextLogon)
                {
                    user.ExpirePasswordNow();
                }

                if (!string.IsNullOrEmpty(profilePath))
                {
                    var userDirectoryEntry = (DirectoryEntry)user.GetUnderlyingObject();
                    userDirectoryEntry.Properties["Profile"].Value = profilePath;
                    userDirectoryEntry.CommitChanges();
                }

                if (passThru)
                {
                    this.WriteObject(user);
                }
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserCreateError", ErrorCategory.WriteError, user);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region SetLocalUser
    [Cmdlet(VerbsCommon.Set, "LocalUser", DefaultParameterSetName = "ChangePassword")]
    public class SetLocalUser : UserCmdletBase
    {
        private string fullName;
        private SwitchParameter passThru;
        private string description;
        private bool mustChangePasswordAtNextLogon;
        private bool cannotChangePassword;
        private bool passwordNeverExpires;
        private string profilePath;
        private string logonScript;
        private string homeDirectoryDrive;
        private string homeDirectory;

        #region Parameters
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("DisplayName")]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        [Parameter]
        public SwitchParameter PassThru
        {
            get { return passThru; }
            set { passThru = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = "ChangePassword")]
        public bool MustChangePasswordAtNextLogon
        {
            get { return mustChangePasswordAtNextLogon; }
            set { mustChangePasswordAtNextLogon = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = "NotChangePassword")]
        public bool CannotChangePassword
        {
            get { return cannotChangePassword; }
            set { cannotChangePassword = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = "NotChangePassword")]
        public bool PasswordNeverExpires
        {
            get { return passwordNeverExpires; }
            set { passwordNeverExpires = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string ProfilePath
        {
            get { return profilePath; }
            set { profilePath = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("ScriptPath")]
        public string LogonScript
        {
            get { return logonScript; }
            set { logonScript = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        [ValidatePattern("[d-z]:$|^$")]
        public string HomeDirectoryDrive
        {
            get { return homeDirectoryDrive; }
            set { homeDirectoryDrive = value; }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string HomeDirectory
        {
            get { return homeDirectory; }
            set { homeDirectory = value; }
        }
        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                user = this.GetUserByName(this.name);

                if (this.MyInvocation.BoundParameters.ContainsKey("Description"))
                {
                    user.Description = description;
                }

                if (this.MyInvocation.BoundParameters.ContainsKey("FullName"))
                {
                    user.DisplayName = fullName;
                }

                if (this.MyInvocation.BoundParameters.ContainsKey("CannotChangePassword"))
                {
                    user.UserCannotChangePassword = cannotChangePassword;
                }

                if (this.MyInvocation.BoundParameters.ContainsKey("PasswordNeverExpires"))
                {
                    user.PasswordNeverExpires = passwordNeverExpires;
                }

                if (this.MyInvocation.BoundParameters.ContainsKey("LogonScript"))
                {
                    user.ScriptPath = logonScript;
                }

                if (this.MyInvocation.BoundParameters.ContainsKey("HomeDirectoryDrive"))
                {
                    user.HomeDrive = homeDirectoryDrive;
                }

                if (this.MyInvocation.BoundParameters.ContainsKey("HomeDirectory"))
                {
                    user.HomeDirectory = homeDirectory;
                }

                user.Save();

                if (this.MyInvocation.BoundParameters.ContainsKey("MustChangePasswordAtNextLogon"))
                {
                    user.ExpirePasswordNow();
                }

                if (this.MyInvocation.BoundParameters.ContainsKey("ProfilePath"))
                {
                    var userDirectoryEntry = (DirectoryEntry)user.GetUnderlyingObject();
                    userDirectoryEntry.Properties["Profile"].Value = profilePath;
                    userDirectoryEntry.CommitChanges();
                }
                this.WriteVerbose(string.Format("User '{0}' on computer '{1}' has been set", name, ctx.ConnectedServer));

                if (passThru)
                {
                    this.WriteObject(user);
                }
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserSetError", ErrorCategory.WriteError, user);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region RemoveLocalUser
    [Cmdlet(VerbsCommon.Remove, "LocalUser")]
    public class RemoveLocalUser : UserCmdletBase
    {
        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                user = this.GetUserByName(this.name);
                user.Delete();
                this.WriteWarning(string.Format("User '{0}' has been removed from machine '{1}'", name, ctx.ConnectedServer));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserRemoveError", ErrorCategory.WriteError, user);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region UnlockLocalUser
    [Cmdlet(VerbsCommon.Unlock, "LocalUser")]
    public class UnlockLocalUser : UserCmdletBase
    {
        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                user = this.GetUserByName(this.name);
                user.UnlockAccount();
                this.WriteVerbose(string.Format("User '{0}' has been unlocked on machine '{1}'", name, ctx.ConnectedServer));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserUnlockError", ErrorCategory.WriteError, user);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region SetLocalUserPassword
    [Cmdlet(VerbsCommon.Set, "LocalUserPassword")]
    public class SetLocalUserPassword : UserCmdletBase
    {
        private string newPassword;

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; }
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                user = this.GetUserByName(this.name);
                user.SetPassword(newPassword);
                this.WriteVerbose(string.Format("Password for user '{0}' on computer '{1}' has been set", name, ctx.ConnectedServer));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserSetPasswordError", ErrorCategory.WriteError, user);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region DisableLocalUser
    [Cmdlet(VerbsLifecycle.Disable, "LocalUser")]
    public class DisableLocalUser : UserCmdletBase
    {
        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                user = this.GetUserByName(this.name);
                user.Enabled = false;
                user.Save();

                this.WriteWarning(string.Format("User '{0}' on computer '{1}' has been disabled", name, ctx.ConnectedServer));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserDisableError", ErrorCategory.WriteError, user);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region EnableLocalUser
    [Cmdlet(VerbsLifecycle.Enable, "LocalUser")]
    public class EnableLocalUser : UserCmdletBase
    {
        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                user = this.GetUserByName(this.name);
                user.Enabled = true;
                user.Save();

                this.WriteWarning(string.Format("User '{0}' on computer '{1}' has been disabled", name, ctx.ConnectedServer));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserEnableError", ErrorCategory.WriteError, user);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region RenameLocalUser
    [Cmdlet(VerbsCommon.Rename, "LocalUser")]
    public class RenameLocalUser : UserCmdletBase
    {
        private string newName;

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string NewName
        {
            get { return newName; }
            set { newName = value; }
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                user = this.GetUserByName(this.name);
                var userDirectoryEntry = (DirectoryEntry)user.GetUnderlyingObject();
                userDirectoryEntry.Rename(newName);

                this.WriteWarning(string.Format("User '{0}' on computer '{1}' has been renamed to '{2}'", name, ctx.ConnectedServer, newName));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "UserRenameError", ErrorCategory.WriteError, user);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion
}
