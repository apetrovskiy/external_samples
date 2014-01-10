  using System;
  
  // Strategy Pattern    by  Judith Bishop         Oct 2007
  // Shows two strategies and a random switch between them

  // The Context
  class Context {
    // Context state
    public const int start = 5;
    public int Counter = 5;
    
    // Strategy aggregation
    IStrategy strategy = new Strategy1();
    
    // Algorithm invokes a strategy method
    public int Algorithm() {
      return strategy.Move(this);
    }
    
    // Changing strategies
    public void SwitchStrategy() {
      if (strategy is Strategy1)
        strategy = new Strategy2();
      else 
        strategy = new Strategy1();
    }
  }
  
  // Strategy interface
  interface IStrategy  {
    int Move (Context c);
  }

  // Strategy 1
  class Strategy1 : IStrategy {
    public int Move (Context c) {
      return ++c.Counter;
    }
  }
  
  // Strategy 2
  class Strategy2 : IStrategy {
    public int Move (Context c) {
      return --c.Counter ;
    }
  }
  
  // Client
  static class Program {
    static void Main () {
      Context context = new Context();
      context.SwitchStrategy();
      Random r = new Random(37);
      for (int i=Context.start; i<=Context.start+15; i++) {
        if (r.Next(3) == 2) {
          Console.Write("|| ");
          context.SwitchStrategy();
        }
        Console.Write(context.Algorithm() +"  ");
      }
      Console.WriteLine();
    }
  }
  /* Output
  4  || 5  6  7  || 6  || 7  8  9  10  || 9  8  7  6  || 7  || 6  5  
  */
