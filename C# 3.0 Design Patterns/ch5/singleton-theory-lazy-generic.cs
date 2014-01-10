  using System;

  //  Singleton PatternJudith Bishop Nov 2007
  //  Generic version
  
  public class Singleton <T>  where T : class, new(){
     Singleton() { }
  
    class SingletonCreator {
      static SingletonCreator () {}
      // Private object instantiated with private constructor
      internal static readonly T instance = new T();
    }

    public static T UniqueInstance {
      get {return SingletonCreator.instance;}
    }
  }

  class Test1 {}
  class Test2 {}

  class Client {

    static void Main () {
      Test1 t1a = Singleton<Test1>.UniqueInstance;
      Test1 t1b = Singleton<Test1>.UniqueInstance;
      Test2 t2 = Singleton <Test2>.UniqueInstance;

      if (t1a == t1b) {
        Console.WriteLine("Objects are the same instance");
      }
    }
  }

