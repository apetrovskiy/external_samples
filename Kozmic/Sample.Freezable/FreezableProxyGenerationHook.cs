namespace Sample.Freezable
{
    using System;
    using System.Reflection;
    using Castle.DynamicProxy;

    public class FreezableProxyGenerationHook : IProxyGenerationHook
    {
        public bool ShouldInterceptMethod(Type type, MethodInfo memberInfo)
        {
            return memberInfo.IsSpecialName &&
                   ( IsSetterName( memberInfo.Name ) ||
                     IsGetterName( memberInfo.Name ) );
        }

        private bool IsGetterName( string name )
        {
            return name.StartsWith( "get_", StringComparison.Ordinal );
        }

        private bool IsSetterName( string name )
        {
            return name.StartsWith("set_", StringComparison.Ordinal);
        }

        public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo)
        {
            var method = memberInfo as MethodInfo;
            if(method!=null)
            {
                this.ValidateNotSetter( method );
            }
        }

        private void ValidateNotSetter(MethodInfo method)
        {
            if( method.IsSpecialName && IsSetterName( method.Name ) )
                throw new InvalidOperationException(
                    string.Format(
                        "Property {0} is not virtual. Can't freeze classes with non-virtual properties.",
                        method.Name.Substring( "set_".Length )
                        )
                    );
        }

        public void MethodsInspected()
        {
        }
    }
}