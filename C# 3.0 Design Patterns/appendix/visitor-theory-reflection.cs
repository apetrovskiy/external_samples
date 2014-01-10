  using System;
  using System.Reflection;
  using ObjectStructure;

  // Visitor Pattern - Example    Judith Bishop October 2007
  // Sets up an object structure and visits it 
  // in two ways - for counting using reflection
   
  namespace ObjectStructure {

    class Element {
      public Element Next {get; set;}
      public Element Part {get; set;}
      public Element () {}
      public Element (Element next) {
        Next = next;
      }
    }
  
    class ElementWithLink : Element {
      public ElementWithLink (Element part, Element next) {
        Next = next; 
        Part = part;
      }
    }
  }
 
  abstract class IVisitor {
    public void ReflectiveVisit(Element element) {
       // Use reflection to find and invoke the correct Visit method
       Type[] types = new Type[] {element.GetType()};
       MethodInfo methodInfo = this.GetType().GetMethod("Visit", types);
       if (methodInfo != null) 
                methodInfo.Invoke(this, new object[] {element});
      else Console.WriteLine("Unexpected Visit");
      }
  }

   // Visitor
  class CountVisitor : IVisitor {
      public int Count {get; set;}
      public void CountElements(Element element) {
        ReflectiveVisit(element);
        if (element.Part!=null) CountElements(element.Part);
        if (element.Next!=null) CountElements(element.Next);
      } 
     
     public void Visit(ElementWithLink element) {
       Console.WriteLine("Not counting");
     }
     // Only Elements are counted
     public void Visit(Element element) {
           Count++;
     }
  }

  // Client
  class Client {
    
  static void Main() {
    // Set up the object structure
    Element objectStructure = 
      new Element(
          new Element(
          new ElementWithLink(
           new Element(           
                 new Element(  
                   new ElementWithLink(
               new Element(null),        
                 new Element(
                 null)))),
      new Element(
          new Element(
          new Element(null))))));

    Console.WriteLine ("Count the elements");
    CountVisitor visitor = new CountVisitor();
    visitor.CountElements(objectStructure);
    Console.WriteLine("Number of Elements is: "+visitor.Count);
  }
}
/*
Count the elements
Not counting
Not counting
Number of Elements is: 9
*/


