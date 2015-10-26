using System.Management.Automation;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System;
using System.Runtime.InteropServices;

namespace LocalAccounts
{
    public abstract class Base : PSCmdlet
    {
        protected string name;
        protected string source;
        protected PSCredential credential;
        protected PrincipalContext ctx;

        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("SamAccountName")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
        [Alias("Server, ComputerName, DomainName")]
        public string Source
        {
            get { return source; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    source = System.Net.Dns.GetHostByName("localhost").HostName;
                }
                else
                {
                    source = value;
                }
            }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public PSCredential Credential
        {
            get { return credential; }
            set { credential = value; }
        }

        protected override void ProcessRecord()
        {
            this.GetContext();
        }

        private void GetContext()
        {
            if (credential == null)
            {
                try
                {
                    ctx = new PrincipalContext(ContextType.Machine, source);
                    var temp = ctx.ConnectedServer; //just access the ConnectedServer property to see if the connection could be made
                }
                catch (COMException ex)
                {
                    if (ex.ErrorCode == -2147024891)
                    {
                        throw new UnauthorizedAccessException(ex.Message, ex);
                    }
                    else if (ex.ErrorCode == -2147024843)
                    {
                        try
                        {
                            ctx = new PrincipalContext(ContextType.Domain, source);
                            var temp = ctx.ConnectedServer; //just access the ConnectedServer property to see if the connection could be made
                        }
                        catch (COMException ex2)
                        {
                            if (ex2.ErrorCode == -2147024891)
                            {
                                throw new AccessDeniedException(ex2.Message, ex2);
                            }
                            else if (ex2.ErrorCode == -2147023570)
                            {
                                throw new LogonFailureException(ex2.Message, ex2);
                            }
                            else if (ex2.ErrorCode == -2147024843)
                            {
                                throw new NetworkPathNotFoundException(ex2.Message, ex2);
                            }
                            else
                            {
                                throw new Exception(ex2.Message, ex2);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(ex.Message, ex);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
            else
            {
                var networkCredential = credential.GetNetworkCredential();
                var userName = string.IsNullOrEmpty(networkCredential.Domain) ? networkCredential.UserName : string.Format("{0}\\{1}", networkCredential.Domain, networkCredential.UserName);

                try
                {
                    ctx = new PrincipalContext(ContextType.Machine, source, userName, credential.GetNetworkCredential().Password);
                    var temp = ctx.ConnectedServer; //just access the ConnectedServer property to see if the connection could be made
                }
                catch (COMException ex)
                {
                    if (ex.ErrorCode == -2147024891)
                    {
                        throw new AccessDeniedException(ex.Message, ex);
                    }
                    else if (ex.ErrorCode == -2147023570)
                    {
                        throw new LogonFailureException(ex.Message, ex);
                    }
                    else if (ex.ErrorCode == -2147024843)
                    {
                        try
                        {
                            ctx = new PrincipalContext(ContextType.Domain, source, userName, credential.GetNetworkCredential().Password);
                            var temp = ctx.ConnectedServer; //just access the ConnectedServer property to see if the connection could be made
                        }
                        catch (COMException ex2)
                        {
                            if (ex2.ErrorCode == -2147024891)
                            {
                                throw new AccessDeniedException(ex2.Message, ex2);
                            }
                            else if (ex2.ErrorCode == -2147023570)
                            {
                                throw new LogonFailureException(ex2.Message, ex2);
                            }
                            else if (ex2.ErrorCode == -2147024843)
                            {
                                throw new NetworkPathNotFoundException(ex2.Message, ex2);
                            }
                            else
                            {
                                throw new Exception(ex2.Message, ex2);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(ex.Message, ex);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}