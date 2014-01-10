  using System;
  using System.Reflection;

   // Visitor Pattern - Example    Judith Bishop October 2007
   // Sets up course rules and visits in two ways
 
  class Program {
  
    abstract class IVisitor {
      public void ReflectiveVisit(Element element) {
        Type[] types = new Type[] {element.GetType()};
        MethodInfo methodInfo = this.GetType().GetMethod("Visit", types);

        if (methodInfo != null) {
            methodInfo.Invoke(this, new object[] {element});
        }
      }
    }

    class PrintVisitor : IVisitor {
      public void Print(Element element) {
        ReflectiveVisit(element);
        if (element.Part!=null) {
          Console.Write(" [");
          Print(element.Part);
        }
        if (element.Next!=null) Print (element.Next);
          Console.Write("] ");
      }
      public void Visit(Element element) {
        Console.Write(" "+element.Weight);
      }
    }

    class StructureVisitor : IVisitor {
      public int Lab {get; set;}
      public int Test {get; set;}
    
      public void VisitAllLabTest(Element element) {
         ReflectiveVisit(element);
         if (element.Part!=null) VisitAllLabTest(element.Part.Next);
         if (element.Next!=null) VisitAllLabTest(element.Next);
      }
      public void Visit(Lab element) {
           Lab += element.Weight;
      }
      public void Visit(Test element) {
         Test += element.Weight;
      }
      public void Visit (Element element) {
         if ((element is Midterm || element is Exam)
            && element.Part==null) Test += element.Weight;
      }
    }

   class Element {
      public int Weight {get; set;}
      public Element Next {get; set;}
      public Element Part {get; set;}

      public void Parse (Context context) {
          string starters = "LTME";
          if (context.Input.Length>0 && starters.IndexOf(context.Input[0])>=0) {
            switch(context.Input[0]) {
              case 'L':                   
                  Next=new Lab();
                   break;
              case 'T':
                  Next=new Test();
                   break;
              case 'M': 
                  Next=new Midterm();
                   break;
              case 'E': 
                  Next = new Exam();
                  break;
            }
            Next.Weight = GetNumber(context);
            if (context.Input.Length>0 && context.Input[0]=='(') {
              context.Input = context.Input.Substring(1);
              Next.Part = new Element();
              Next.Part.Parse(context);
              Element e = Next.Part;
              while (e!=null) {
                e.Weight = e.Weight * Next.Weight / 100;
                e = e.Next;
              }
              context.Input = context.Input.Substring(2);
            }
            Next.Parse(context);
         } 
      }
    }

   class Course : Element {
      public string Name {get; set;}
      public Course (Context context) {
         Name = context.Input.Substring(0,6);
         context.Input = context.Input.Substring(7);
      }
   }

   class Lab : Element {
   }

   class Test : Element {
   }

   class Midterm : Element {
   }

   class Exam : Element {
   }

  class Context {
    public string Input {get; set;}
    public double Output {get; set;}

    public Context(string c) {
      Input = c;
      Output = 0;
    }
  }

  static int GetNumber (Context context) {
    int atSpace = context.Input.IndexOf(' ');
    int number = Int32.Parse(context.Input.Substring(1,atSpace));
    context.Input = context.Input.Substring(atSpace+1);
    return number;
  }

  static void Main() {
     string rules = "COS333 L2 L2 L2 L2 L2 M25 (L40 T60 ) L10 E55 (L28 T73 ) ";

     Context context;
     Console.WriteLine (rules+"\n");
     context = new Context (rules);
     Element course = new Course(context);
     course.Parse(context);

     PrintVisitor visitor = new PrintVisitor();
     Console.WriteLine("Visitor 1 - Course structure");
     visitor.Print(course);

     StructureVisitor visitor2 = new StructureVisitor();
     visitor2.VisitAllLabTest(course);
     Console.WriteLine ("\n\nVisitor 2 - Summing the weights\nLabs " 
                        +visitor2.Lab + "% and Tests " 
                        +visitor2.Test + "%");
  }
}
/*Output
COS333 L2 L2 L2 L2 L2 M25 (L40 T60 ) L10 E55 (L28 T73 ) 

Visitor 1 - Course structure
 0 2 2 2 2 2 25 [ 0 10 15] ] ]  10 55 [ 0 15 40] ] ] ] ] ] ] ] ] ] ] ] 

Visitor 2 - Summing the weights
Labs 45% and Tests 55%
*/
