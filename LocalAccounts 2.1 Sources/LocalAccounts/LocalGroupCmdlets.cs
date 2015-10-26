using System.Management.Automation;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.DirectoryServices;
using System;

namespace LocalAccounts
{
    #region GroupCmdletBase
    public class GroupCmdletBase : Base
    {
        protected GroupPrincipal group = null;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }

        protected GroupPrincipal GetGroupByName(string name)
        {
            var group = GroupPrincipal.FindByIdentity(ctx, name);

            if (group == null)
            {
                throw new NoMatchingPrincipalException("The group could not be found");
            }

            return group;
        }
    }
    #endregion

    #region GetLocalGroup
    [Cmdlet(VerbsCommon.Get, "LocalGroup", DefaultParameterSetName = "ByName")]
    public class GetLocalGroup : GroupCmdletBase
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

                var filter = new GroupPrincipal(ctx);
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

    #region NewLocalGroup
    [Cmdlet(VerbsCommon.New, "LocalGroup")]
    public class NewLocalGroup : GroupCmdletBase
    {
        private SwitchParameter passThru;
        private string description;

        #region Parameters
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
        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                GroupPrincipal group = null;
                group = new GroupPrincipal(ctx);
                group.SamAccountName = name;
                group.Save();

                //Description property is not setable due to a bug, so the workaround
                if (!string.IsNullOrEmpty(description))
                {
                    var groupObject = (DirectoryEntry)group.GetUnderlyingObject();
                    groupObject.Properties["Description"].Value = description;
                    groupObject.CommitChanges();
                }

                group.Save();

                if (passThru)
                {
                    this.WriteObject(group);
                }
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupCreateError", ErrorCategory.WriteError, group);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region SetLocalGroup
    [Cmdlet(VerbsCommon.Set, "LocalGroup")]
    public class SetLocalGroup : GroupCmdletBase
    {
        private SwitchParameter passThru;
        private string description;

        #region Parameters
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
        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                group = this.GetGroupByName(this.name);

                if (this.MyInvocation.BoundParameters.ContainsKey("Description"))
                {
                    //Description property is not setable due to a bug, so the workaround

                    var groupObject = (DirectoryEntry)group.GetUnderlyingObject();

                    groupObject.Properties["Description"].Value = description;

                    groupObject.CommitChanges();
                }

                group.Save();
                this.WriteVerbose(string.Format("Group '{0}' on computer '{1}' has been set", name, ctx.ConnectedServer));

                if (passThru)
                {
                    this.WriteObject(group);
                }
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupSetError", ErrorCategory.WriteError, group);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region RemoveLocalGroup
    [Cmdlet(VerbsCommon.Remove, "LocalGroup")]
    public class RemoveLocalGroup : GroupCmdletBase
    {
        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                group = this.GetGroupByName(this.name);
                group.Delete();
                this.WriteWarning(string.Format("Group '{0}' has been removed from machine '{1}'", name, ctx.ConnectedServer));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupRemoveError", ErrorCategory.WriteError, group);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region RenameLocalGroup
    [Cmdlet(VerbsCommon.Rename, "LocalGroup")]
    public class RenameLocalGroup : GroupCmdletBase
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

                group = this.GetGroupByName(this.name);
                var groupDirectoryEntry = (DirectoryEntry)group.GetUnderlyingObject();
                groupDirectoryEntry.Rename(newName);
                this.WriteWarning(string.Format("Group '{0}' on computer '{1}' has been renamed to '{2}'", name, ctx.ConnectedServer, newName));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupRenameError", ErrorCategory.WriteError, group);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region GetLocalGroupMember
    [Cmdlet(VerbsCommon.Get, "LocalGroupMember")]
    public class GetLocalGroupMember : GroupCmdletBase
    {
        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                group = this.GetGroupByName(this.name);
                group.GetMembers().ForEach(m => this.WriteObject(m));
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "ReadMemberError", ErrorCategory.ReadError, group);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region AddLocalGroupMember
    [Cmdlet(VerbsCommon.Add, "LocalGroupMember")]
    public class AddLocalGroupMember : GroupCmdletBase
    {
        private Principal[] members;

        [Parameter(Position = 1, Mandatory = true)]
        public Principal[] Members
        {
            get { return members; }
            set { members = value; }
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                group = this.GetGroupByName(this.name);

                foreach (var member in members)
                {
                    try
                    {
                        group.Members.Add(member);
                        this.WriteVerbose(string.Format("Member '{0}' has been added to group '{1}' on computer '{2}'", member.Name, group.Name, ctx.ConnectedServer));
                    }
                    catch (Exception ex)
                    {
                        var errorRecord = new ErrorRecord(ex, "AddMemberError", ErrorCategory.WriteError, group);
                        this.WriteError(errorRecord);
                    }
                }

                group.Save();
            }
            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "SaveGroup", ErrorCategory.WriteError, group);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion

    #region RemoveLocalGroupMember
    [Cmdlet(VerbsCommon.Remove, "LocalGroupMember")]
    public class RemoveLocalGroupMember : GroupCmdletBase
    {
        private Principal[] members;

        [Parameter(Position = 1, Mandatory = true)]
        public Principal[] Members
        {
            get { return members; }
            set { members = value; }
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                group = this.GetGroupByName(this.name);

                foreach (var member in members)
                {
                    try
                    {
                        group.Members.Remove(member);
                        this.WriteVerbose(string.Format("Member '{0}' has been removed from group '{1}' on computer '{2}'", member.Name, group.Name, ctx.ConnectedServer));
                    }
                    catch (Exception ex)
                    {
                        var errorRecord = new ErrorRecord(ex, "RemoveMemberError", ErrorCategory.WriteError, group);
                        this.WriteError(errorRecord);
                    }
                }

                group.Save();
            }

            catch (NoMatchingPrincipalException ex)
            {
                var errorRecord = new ErrorRecord(ex, "GroupNotFoundError", ErrorCategory.ObjectNotFound, ctx);
                this.WriteError(errorRecord);
            }
            catch (Exception ex)
            {
                var errorRecord = new ErrorRecord(ex, "SaveGroup", ErrorCategory.WriteError, group);
                this.WriteError(errorRecord);
            }
        }
    }
    #endregion
}