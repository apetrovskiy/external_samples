using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;

namespace NCT
{
    public delegate void DecorationDelegate(object target, object[] parameters);

    public class Decoration
    {
        public Decoration(DecorationDelegate n, object[] p)
        {
            action = n;
            paras = p;
        }

        private DecorationDelegate action;
        private object[] paras;

        public DecorationDelegate Action
        {
            get { return action; }
        }

        public object[] Parameters
        {
            get { return paras; }
        }
    }

    public class ObjectProxy : RealProxy, IRemotingTypeInfo
    {
        private object target;
        private Decoration preAspect;
        private Decoration postAspect;
        private String[] arrMethods;

        protected internal ObjectProxy(object target, String[] arrMethods, 
            Decoration preAspect, Decoration postAspect)
            : base(typeof(MarshalByRefObject))
        {
            this.target = target;
            this.preAspect = preAspect;
            this.postAspect = postAspect;
            this.arrMethods = arrMethods;
        }

        public override ObjRef CreateObjRef(System.Type type)
        {
            throw new NotSupportedException("ObjRef for DynamicProxy isn't supported");
        }

        public bool CanCastTo(System.Type toType, object obj)
        {
            return true;
        }

        public string TypeName
        {
            get { throw new System.NotSupportedException("TypeName for DynamicProxy isn't supported"); }
            set { throw new System.NotSupportedException("TypeName for DynamicProxy isn't supported"); }
        }

        public override IMessage Invoke(IMessage message)
        {
            object returnValue = null;
            ReturnMessage returnMessage;

            IMethodCallMessage methodMessage = (IMethodCallMessage)message;
            MethodBase method = methodMessage.MethodBase;

            // Perform the preprocessing
            if (HasMethod(method.Name) && preAspect != null)
            {
                try
                {
                    preAspect.Action(target, preAspect.Parameters);
                }
                catch (Exception e)
                {
                    returnMessage = new ReturnMessage(e, methodMessage);
                    return returnMessage;
                }
            }

            // Perform the call
            try
            {
                returnValue = method.Invoke(target, methodMessage.Args);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
            }

            // Perform the postprocessing
            if (HasMethod(method.Name) && postAspect != null)
            {
                try
                {
                    postAspect.Action(target, postAspect.Parameters);
                }
                catch (Exception e)
                {
                    returnMessage = new ReturnMessage(e, methodMessage);
                    return returnMessage;
                }
            }

            // Create the return message (ReturnMessage)
            returnMessage = new ReturnMessage(returnValue, methodMessage.Args, 
                methodMessage.ArgCount, methodMessage.LogicalCallContext, methodMessage);
            return returnMessage;
        }

        private bool HasMethod(String mtd)
        {
            foreach (string s in arrMethods)
            {
                if (s.Equals(mtd))
                    return true;
            }

            return false;
        }
    }

    public class ObjectProxyFactory
    {
        public static object CreateProxy(object target, String[] arrMethods, 
            Decoration preAspect, Decoration postAspect)
        {
            ObjectProxy dp = new ObjectProxy(target, arrMethods, preAspect, postAspect);
            object o = dp.GetTransparentProxy();
            return o;
        }
    }
}
