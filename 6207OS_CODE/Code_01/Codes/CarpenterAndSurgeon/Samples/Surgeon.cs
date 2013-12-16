namespace Samples
{
    class Surgeon
    {
        private Forceps forceps;

        public Surgeon(Forceps forceps)
        {
            this.forceps = forceps;
        }

        public void Operate()
        {
            forceps.Grab();
            //...
        }
    }
}