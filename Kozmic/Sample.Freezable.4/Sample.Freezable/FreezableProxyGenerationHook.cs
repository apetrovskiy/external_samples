namespace Sample.Freezable
{
    using System;
    using System.Reflection;
    using Castle.DynamicProxy;

    public class FreezableProxyGenerationHook : IProxyGenerationHook
    {
        public bool ShouldInterceptMethod(Type type, MethodInfo memberInfo)
        {
            return memberInfo.Name.StartsWith("set_", StringComparison.Ordinal);
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        public void MethodsInspected()
        {
        }
    }
}