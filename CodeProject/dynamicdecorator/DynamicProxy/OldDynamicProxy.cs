using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Collections;

namespace ImageCenter.Common.Core.Logging
{
    //This delegate is used to present an aspect
    public delegate object InvocationDelegate(object target, object[] parameters);

    //This interface will be implemented by a custom RealProxy
    public interface IDynamicProxy
    {
        object ProxyTarget
        {
            get;
            set;
        }
    }

    //This class is used to create an aspect
    public class Aspect
    {
        public Aspect(InvocationDelegate n, object[] p)
        {
            name = n;
            paras = p;
        }

        private InvocationDelegate name;
        private object[] paras;

        public InvocationDelegate Name
        {
            get { return name; }
        }

        public object[] Parameters
        {
            get { return paras; }
        }
    }

    //The DynamicProxy contains aspects which are invoked when an object is accessed 
    //from its DynamicProxy.
    public class DynamicProxy : RealProxy, IDynamicProxy, IRemotingTypeInfo
    {
        private object proxyTarget;
        private ArrayList preAspects;
        private ArrayList postAspects;

        protected internal DynamicProxy(object proxyTarget, ArrayList preAspects, ArrayList postAspects)
            : base(typeof(IDynamicProxy))
        {
            this.proxyTarget = proxyTarget;
            this.preAspects = preAspects;
            this.postAspects = postAspects;
        }

        public override ObjRef CreateObjRef(System.Type type)
        {
            throw new NotSupportedException("ObjRef for DynamicProxy isn't supported");
        }

        public bool CanCastTo(System.Type toType, object obj)
        {
            // Assume we can (which is the default unless strict is true)
            bool canCast = true;
            return canCast;
        }

        public string TypeName
        {
            get { throw new System.NotSupportedException("TypeName for DynamicProxy isn't supported"); }
            set { throw new System.NotSupportedException("TypeName for DynamicProxy isn't supported"); }
        }

        public override IMessage Invoke(IMessage message)
        {
            // Convert to a MethodCallMessage
            IMethodCallMessage methodMessage = new MethodCallMessageWrapper((IMethodCallMessage)message);

            // Extract the method being called
            MethodBase method = methodMessage.MethodBase;

            // Perform the call
            object returnValue = null;
            if (method.DeclaringType == typeof(IDynamicProxy))
            {
                // Handle IDynamicProxy interface calls on this instance instead of on the proxy target instance
                returnValue = method.Invoke(this, methodMessage.Args);
            }
            else
            {
                int i = 0;
                if (preAspects != null)
                {
                    for (i = 0; i < preAspects.Count; i++)
                    {
                        if (preAspects[i] != null)
                        {
                            Aspect hd = preAspects[i] as Aspect;
                            hd.Name(proxyTarget, hd.Parameters);
                        }
                    }
                }

                returnValue = method.Invoke(proxyTarget, methodMessage.Args);

                if (postAspects != null)
                {
                    for (i = 0; i < postAspects.Count; i++)
                    {
                        if (postAspects[i] != null)
                        {
                            Aspect hd = postAspects[i] as Aspect;
                            hd.Name(proxyTarget, hd.Parameters);
                        }
                    }
                }
            }

            // Create the return message (ReturnMessage)
            ReturnMessage returnMessage = new ReturnMessage(returnValue, methodMessage.Args, methodMessage.ArgCount, methodMessage.LogicalCallContext, methodMessage);
            return returnMessage;
        }

        public object ProxyTarget
        {
            get { return proxyTarget; }
            set { proxyTarget = value; }
        }
        /*
                public void AddPreAspect(InvocationDelegate name, object[] paras)
                {
                    preAspects.Add(new Aspect(name, paras));
                }

                public void AddPostAspect(InvocationDelegate name, object[] paras)
                {
                    postAspects.Add(new Aspect(name, paras));
                }
        */
    }

    //This DynamicProxyFactory manufactures a DynamicProxy or just returns the original object.
    public class DynamicProxyFactory
    {
        //The DynamicProxyFactory validates aspects to ensure a client doesn't pass invalid arraylist and
        //manufactures a trasparent proxy of the target object or returns the object.
        public static object CreateProxy(object target, ArrayList preAspects, ArrayList postAspects, bool useDynamicProxy)
        {
            if (useDynamicProxy == true)
            {
                int i = 0;
                if (preAspects != null)
                {
                    for (i = 0; i < preAspects.Count; i++)
                    {
                        if (preAspects[i] is Aspect != true)
                        {
                            throw new System.ArgumentException("A pre-aspect is not a type Aspect.");
                        }
                    }
                }

                if (postAspects != null)
                {
                    for (i = 0; i < postAspects.Count; i++)
                    {
                        if (postAspects[i] is Aspect != true)
                        {
                            throw new System.ArgumentException("A post-aspect is not a type Aspect.");
                        }
                    }
                }

                return new DynamicProxy(target, preAspects, postAspects).GetTransparentProxy();
            }

            return target;
        }
    }
}
