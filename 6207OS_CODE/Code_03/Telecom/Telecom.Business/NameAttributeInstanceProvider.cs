using System.Linq;
using Ninject.Extensions.Factory;

namespace Telecom.Business
{
    public class NameAttributeInstanceProvider : StandardInstanceProvider
    {
        protected override string GetName(System.Reflection.MethodInfo methodInfo, object[] arguments)
        {
            var nameAttribute = methodInfo
                                    .GetCustomAttributes(typeof(BindingNameAttribute), true)
                                    .FirstOrDefault() as BindingNameAttribute;
            if (nameAttribute != null)
            {
                return nameAttribute.Name;
            }
            return base.GetName(methodInfo, arguments);
        }
    }
}