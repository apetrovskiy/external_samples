using System;

namespace DataMigration.Business
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ConfigurationAttribute : Attribute { }
}