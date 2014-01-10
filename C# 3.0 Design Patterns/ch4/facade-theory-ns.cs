using System;

  // Facade Pattern                       Judith Bishop Dec 2006
  // Sets up a library of three systems, accessed through a
  // Facade of two operations
  // Compile with csc /t:library /out:FacadeLib.dll Facade2.cs

  namespace FacadeLib {
    
  internal class SubsystemA {
    internal string A1() {
      return "Subsystem A, Method A1\n";
    }
      
    internal string A2() {
        return "Subsystem A, Method A2\n";
      }
  }

  internal class SubsystemB{
    internal string B1() {
      return "Subsystem B, Method B1\n";
    }
  }
  
  internal class SubsystemC{
    internal string C1() {
      return "Subsystem C, Method C1\n";
    }
  }
  
  public static class Facade {
    static SubsystemA a = new SubsystemA();
    static SubsystemB b = new SubsystemB();
    static SubsystemC c = new SubsystemC();

    public static void Operation1() {
      Console.WriteLine("Operation 1\n" +
      a.A1() +
      a.A2() +
      b.B1());
    }

    public static void Operation2() {
      Console.WriteLine("Operation 2\n" +
      b.B1() +
      c.C1());
    }
  }
}

/* Output:

Operation 1
Subsystem A, Method A1
Subsystem A, Method A2
Subsystem B, Method B1

Operation 2
Subsystem B, Method B1
Subsystem C, Method C1

*/

/*
SubsystemC x = new SubsystemC();
x.C1();
Facade2Main.cs(12,3): error CS0122: 'FacadeLib.SubsystemC' is inaccessible due to its protection level
Facade2Main.cs(12,22): error CS0122: 'FacadeLib.SubsystemC' is inaccessible due to its protection level
Facade2Main.cs(12,18): error CS0143: The type 'FacadeLib.SubsystemC' has no constructors defined
Facade2Main.cs(13,5): error CS0117: 'FacadeLib.SubsystemC' does not contain a definition for 'C1'
*/
 
