using System;

namespace AbstractFactoryPattern {
  //  Abstract Factory        D-J Miller and Judith Bishop Sept 2007
  //  Uses generics to simplify the creation of factories
  
  interface IFactory<Brand>
    where Brand : IBrand {
    IBag CreateBag();
    IShoes CreateShoes();
  }

  // Conctete Factories (both in the same one)
  class Factory<Brand> : IFactory<Brand>
    where Brand : IBrand, new() {
    public IBag CreateBag() {
      return new Bag<Brand>();
    }

    public IShoes CreateShoes() {
      return new Shoes<Brand>();
    }
  }

  // Product 1
  interface IBag {
    string Material { get; }
  }

  // Product 2
  interface IShoes {
    int Price { get; }
  }

  // Concrete Product 1
  class Bag<Brand> : IBag
    where Brand : IBrand, new() {
    private Brand myBrand;
    public Bag() {
      myBrand = new Brand();
    }

    public string Material { get { return myBrand.Material; } }
  }

  // Concrete Product 2
  class Shoes<Brand> : IShoes
    where Brand : IBrand, new() {
      
    private Brand myBrand;
      
    public Shoes() {
      myBrand = new Brand();
    }

    public int Price { get { return myBrand.Price; } }
  }

  interface IBrand {
    int Price { get; }
    string Material { get; }
  }

  class Gucci : IBrand {
    public int Price { get { return 1000; } }
    public string Material { get { return "Crocodile skin"; } }
  }

  class Poochy : IBrand {
    public int Price { get { return new Gucci().Price / 3; } }
    public string Material { get { return "Plastic"; } }
  }

  class Groundcover : IBrand {
    public int Price { get { return 2000; } }
    public string Material { get { return "South african leather"; } }
  }

  class Client<Brand>
    where Brand : IBrand, new() {
    public void ClientMain() { //IFactory<Brand> factory)
      IFactory<Brand> factory = new Factory<Brand>();

      IBag bag = factory.CreateBag();
      IShoes shoes = factory.CreateShoes();

      Console.WriteLine("I bought a Bag which is made from " + bag.Material);
      Console.WriteLine("I bought some shoes which cost " + shoes.Price);
    }
  }

  static class Program {
    static void Main() {
      // Call Client twice
      new Client<Poochy>().ClientMain();
      new Client<Gucci>().ClientMain();
      new Client<Groundcover>().ClientMain();
    }
  }
}
/* Output
I bought a Bag which is made from Plastic
I bought some shoes which cost 333
I bought a Bag which is made from Crocodile skin
I bought some shoes which cost 1000
I bought a Bag which is made from South african leather
I bought some shoes which cost 2000
*/
 