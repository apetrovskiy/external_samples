
namespace Samples.ServiceLocator
{
    class Carpenter
    {
        private Saw saw = Manual.Locate<Saw>();
        void MakeChair()
        {
            saw.Cut();
            // ...
        }
    }
}