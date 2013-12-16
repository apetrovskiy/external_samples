namespace Sample.Freezable
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var rex = Freezable.MakeFreezable<Pet>();
            rex.Name = "Rex";
            Console.WriteLine(Freezable.IsFreezable(rex)
                                  ? "Rex is freezable!"
                                  : "Rex is not freezable. Something is not working");
            Console.WriteLine(rex.ToString());
            Console.WriteLine("Add 50 years");
            rex.Age += 50;
            Console.WriteLine("Age: {0}", rex.Age);
            rex.Deceased = true;

            Console.WriteLine("Deceased: {0}", rex.Deceased);
            Freezable.Freeze(rex);
            try
            {
                rex.Age++;
            }
            catch(ObjectFrozenException ex)
            {
                Console.WriteLine("Oups. it's frozen. Can't change that anymore");
            }
            Console.WriteLine("--- press enter to close");
            Console.ReadLine();
        }
    }

    public class Pet
    {
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual bool Deceased { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Age: {1}, Deceased: {2}", Name, Age, Deceased);
        }
    }
}