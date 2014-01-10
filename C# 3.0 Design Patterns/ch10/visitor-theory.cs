  using System;
  using ObjectStructure;

    // Visitor Pattern - Example    Judith Bishop October 2007
    // Sets up an object structure and visits it 
    // in two ways - for printing and for collecting elements in groups
    // using dynamic-dispatch
   
  namespace ObjectStructure {
   
    abstract class IElement {
      // Added to make Elements Visitor-ready
      public abstract void Accept(IVisitor visitor);
    }

    class Element : IElement {
      public Element Next {get; set;}
      public Element Link {get; set;}
      public Element () {}
      public Element (Element next) {
        Next = next;
      }
      public override void Accept(IVisitor visitor) {
        visitor.Visit(this);
      }
    }
  
    class ElementWithLink : Element {
      public ElementWithLink (Element link, Element next) {
        Next = next; 
        Link = link;
      }
      public override void Accept(IVisitor visitor) {
        visitor.Visit(this);
      }
    }
  } // end ObjectStructure
  
    // Visitor interface 
    interface IVisitor {
      void Visit (Element element);
      void Visit (ElementWithLink element);
    }

    // Visitor
    class CountVisitor : IVisitor {
    
    public int Count {get; set;}
    
    public void CountElements(Element element) {
      element.Accept(this);
      if (element.Link!=null) CountElements(element.Link);
      if (element.Next!=null) CountElements(element.Next);
    } 
    
    // Elements with links are not counted
    public void Visit(ElementWithLink element) {
       Console.WriteLine("Not counting");
    }
    
    // Only Elements are counted
    public void Visit (Element element) {
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

      Console.WriteLine ("Count the Elements");
      CountVisitor visitor = new CountVisitor();
      visitor.CountElements(objectStructure);
      Console.WriteLine("Number of Elements is: "+visitor.Count);
    }
  }
/*
Count the As
Found Element
Found Element
Number of elements is: 9
*/


