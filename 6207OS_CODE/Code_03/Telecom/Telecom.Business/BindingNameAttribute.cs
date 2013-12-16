using System;

namespace Telecom.Business
{
    public class BindingNameAttribute : Attribute
    {
        public BindingNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}