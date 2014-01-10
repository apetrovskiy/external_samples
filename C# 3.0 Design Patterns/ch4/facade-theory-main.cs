  using System;
  using FacadeLib;
  // Compile with csc /r:FacadeLib.dll Facade2Main.cs
  
  class FacadePattern1 {
    class Client {
    public void ClientMain () {

        Facade.Operation1();
        Facade.Operation2();

    }
  }
  
  static void Main() {
     new Client().ClientMain();
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