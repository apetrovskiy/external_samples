
  using System;

  class SingletonPattern {

  //  Singleton Pattern      Judith Bishop Dec 2006
  //  The public property protects the private constructor
    
    public sealed class Singleton {
      // Private Constructor 
      Singleton() { }
      
      // Private object instantiated with private constructor
      static readonly Singleton instance = new Singleton();

      // Public static property to get the object
      public static Singleton UniqueInstance {
        get { return instance;}
      }
    }

    static void Main() {
      Singleton s1 = Singleton.UniqueInstance;
      Singleton s2 = Singleton.UniqueInstance;

      if (s1 == s2) {
        Console.WriteLine("Objects are the same instance");
      }
    }
  }

