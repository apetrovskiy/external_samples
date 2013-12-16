namespace Sample.Freezable
{
    internal interface IFreezable
    {
        bool IsFrozen { get; }
        void Freeze();
    }
}