  using System;

  class CommandPattern {

    // Command Pattern           Judith Bishop June 2007
    //
    // Has two different delegates for two types of commands
    // The second receiver uses both of them

    delegate void Invoker ();
    delegate void InvokerSet (string s);

    static Invoker Execute, Redo, Undo;
    static InvokerSet Set;
  
    class Command  {
      public Command(Receiver receiver) {  
        Set = delegate {Console.WriteLine("Not implemented - default of XXX used");
        receiver.S = "XXX";};
        Execute= receiver.Action;
        Redo = receiver.Action;
        Undo = receiver.Reverse;
      }
    }
  
    class Command2  {
      public Command2(Receiver2 receiver) {  
        Set = receiver.SetData;
        Execute= receiver.DoIt;
        Redo = receiver.DoIt;
        Undo = delegate {Console.WriteLine("Not Implemented");};
      }
    }
  
    public class Receiver {
      string build, oldbuild;
      string s = "some string ";
      public string S { get; set; }
      
      public void Action() {
          oldbuild = build;
          build +=s;
          Console.WriteLine("Receiver is adding "+build);
      }
     
      public void Reverse() {
        build = oldbuild;
        Console.WriteLine("Receiver is reverting to "+build);
      }
    }
  
  
    public class Receiver2 {
      string build, oldbuild;
      string s;
      
      public void SetData(string s) {
        this.s = s;
      }
    
      public void DoIt() {
        oldbuild = build;
        build +=s;
        Console.WriteLine("Receiver is building "+build);
      }
    }
    
    class Client {
      static void Main() {
      
        new Command (new Receiver());
        Execute();
        Redo();
        Undo();
        Set("III");
        Execute();
        
        Console.WriteLine();
        new Command2 (new Receiver2());
        Set("houses ");
        Execute();
        Set("castles ");
        Undo();
        Execute();
        Redo();
      }
    }
  }
/*
Receiver is adding some string 
Receiver is adding some string some string 
Receiver is reverting to some string 
Not implemented - default of XXX used
Receiver is adding some string XXX

Receiver is building houses 
Not Implemented
Receiver is building houses castles 
Receiver is building houses castles castles  
*/


