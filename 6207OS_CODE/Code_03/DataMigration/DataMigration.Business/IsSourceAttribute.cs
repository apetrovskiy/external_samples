using Ninject;

namespace DataMigration.Business
{
    public class IsSourceAttribute : ConstraintAttribute
    {
        private readonly bool isSource;

        public IsSourceAttribute(bool isSource)
        {
            this.isSource = isSource;
        }

        public override bool Matches(Ninject.Planning.Bindings.IBindingMetadata metadata)
        {
            return metadata.Has("IsSource") && metadata.Get<bool>("IsSource") == isSource;
        }
    }
}