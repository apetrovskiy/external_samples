  using System;
  using System.Collections.Generic;

   // State Pattern               Judith Bishop  Oct 2007
   // Simple game where the context changes the state based on user input
   // Has four states, each with 6 operations 
   
  interface IState {
    int MoveUp(Context context);
    int MoveDown(Context context);
  }

  // State 1
  class NormalState : IState {
    public  int MoveUp(Context context) {
      context.Counter+=2;
      return context.Counter;
    }

    public int MoveDown(Context context) {
        if (context.Counter < Context.limit) {
          context.State = new FastState();
          Console.Write("|| ");
        }
        context.Counter-=2;
        return context.Counter;
    }
  }

  // State 2
  class FastState : IState {
    public int MoveUp(Context context) {
      context.Counter+=5;
      return context.Counter;
    }

    public int MoveDown(Context context) {
       if (context.Counter < Context.limit) {
        context.State = new NormalState();
        Console.Write("||");
      }
      context.Counter-=5;
      return context.Counter;
    }
  }

  // Context
  class Context {
    public const int limit = 10;
    public IState State {get; set; }
    public int Counter = limit;
      
    public int Request(int n) {
      if (n==2)
        return State.MoveUp(this);
      else
        return State.MoveDown(this);
    }
  }
   
  static class Program {
     // The user interface
    static void Main () {
      Context context = new Context();
      context.State = new NormalState();
      Random r = new Random(37);
      for (int i = 5; i<=25; i++) {
        int command = r.Next(3);
        Console.Write(context.Request(command)+" ");
      }
      Console.WriteLine();
    }
  }
   /* Output
   8 10 8 || 6 11 16 11 6 ||1 3 || 1 ||-4 || -6 -1 4 ||-1 || -3 2 7 ||2 4 
   */
 


