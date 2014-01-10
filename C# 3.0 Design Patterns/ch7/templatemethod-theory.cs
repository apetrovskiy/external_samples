using System;

// Template Method Pattern     Judith Bishop November 2007
//  Shows two versions of the same algorithm
       
  interface IPrimitives {
    string Operation1();
      string Operation2();
  }
      
  class Algorithm {
    public void TemplateMethod(IPrimitives a) {
        string s = 
          a.Operation1() +
          a.Operation2();
         Console.WriteLine(s);
    }
  }

  class ClassA : IPrimitives {
    public string Operation1() {
      return "ClassA:Op1 ";
    }
    public string Operation2() {
      return "ClassA:Op2 ";
    }
  }

  class ClassB : IPrimitives {
    public string Operation1() {
      return "ClassB:Op1 ";
    }
    public string Operation2() {
      return "ClassB.Op2 ";
    }
  }

  class TemplateMethodPattern {
    
    static void Main() {
      Algorithm m = new Algorithm();
           
      m.TemplateMethod(new ClassA());
      m.TemplateMethod(new ClassB());
    }
  }
/* Output
ClassA:Op1 ClassA:Op2 
ClassB:Op1 ClassB.Op2 
*/
