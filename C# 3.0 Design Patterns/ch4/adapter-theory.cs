  using System;
  
  // Adapter Pattern -  Simple                       Judith Bishop Aug 2006
  // Simplest adapter using interfaces and inheritance
  
  // Existing way requests are implemented
  class Adaptee { 
    // Provide full precision
    public double SpecificRequest (double a, double b) {
      return a/b;
    }
  }
  
  // Required standard for requests
  interface ITarget {
    // Rough estimate required
    string Request (int i);
  }
  
  // Implementing the required standard via the Adaptee
  class Adapter : Adaptee, ITarget {
    public string Request (int i) {
      return "Rough estimate is " + (int) Math.Round(SpecificRequest (i,3));
    }
  }
  
  class Client {
    
    static void  Main () {
       // Showing the Adapteee in stand-alone mode
      Adaptee first = new Adaptee();
      Console.Write("Before the new standard\nPrecise reading: ");
      Console.WriteLine(first.SpecificRequest(5,3));
       
      // What the client really wants
      ITarget second = new Adapter();
      Console.WriteLine("\nMoving to the new standard");
      Console.WriteLine(second.Request(5));
    }
  }
