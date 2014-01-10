  using System;

  // Interpreter Pattern - Example    Judith Bishop October 2007
  // Sets up an object structure and interprets it with given data
        
  static class ElementExtensions {
    public static string gap;
      
    public static void Print(this Element element) {
      Console.WriteLine(gap+element + " " + element.Weight);
      if (element.Part!=null) {
        gap+="  "; 
        Print(element.Part.Next);
        gap = gap.Substring(2);
      }
      if (element.Next!=null) Print(element.Next);
    }
   
    public static int Lab {get; set;}
    public static int Test {get; set;}
     
    public static void Summarize(this Element element) {
      if (element is Lab) Lab += element.Weight;
      else if (element is Test) 
        Test += element.Weight;
      else if ((element is Midterm || element is Exam) && element.Part==null) 
        Test += element.Weight;
      if (element.Part!=null) Summarize(element.Part.Next);
      if (element.Next!=null) Summarize(element.Next);
    }
    
    public static int [] values;
    public static int  n;
    public static Context context;
       
    public static void SetUp (this Element element, Context c, int[] v) {
      context = c;
      context.Output=0;
      values = v;
      n = 0;
    }
    
    public static void Interpreter(this Element element) {
      if (element is Lab || element is Test) {
        context.Output += values[n]*element.Weight;
        n++;
      }
      else
      if ((element is Midterm || element is Exam) && element.Part==null) {
        context.Output += values[n]*element.Weight;
        n++;
      }
        
      if (element.Part!=null) Interpreter(element.Part.Next);
      if (element.Next!=null) Interpreter(element.Next);
    }
  }
  
  public class Element {
    public int Weight {get; set;}
    public Element Next {get; set;}
    public Element Part {get; set;}

    public virtual string Display() {
      return Weight+"%";
    }
    
    int GetNumber (Context context) {
      int atSpace = context.Input.IndexOf(' ');
      int number = Int32.Parse(context.Input.Substring(1,atSpace));
      context.Input = context.Input.Substring(atSpace+1);
      return number;
    }
      
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
    public override string Display() {
      return Name;
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
   
  public class Context {
    public string Input {get; set;}
     public double Output {get; set;}
    
    public Context(string c) {
      Input = c;
      Output = 0;
    }
  }

  static class IntArrayExtension {
    public static string Display (this int[] a) {
       string s = "[";
       foreach (int i in a) 
            s+=i+", ";
          return s.Substring(0,s.Length-2)+"]";
    }
  }
  
  class InterpreterPattern {
    
    static void Main() {
      string rules = "COS333 L2 L2 L2 L2 L2 M25 (L40 T60 ) L10 E55 (L28 T73 ) ";
      int [][] values = new [] { new [] {80,0,100,100,85,51,52,50,57,56},
                       new [] {87,95,100,100,77,70,99,100,75,94},
                       new [] {0,55,100,65,55,75,73,74,71,72}};
      
      Context context;
      Console.WriteLine (rules+"\n");
                       
      context = new Context (rules);
      Element course = new Course(context);
      course.Parse(context);
      
      Console.WriteLine("Visitor 1 - Course structure\n");
      course.Print();
                    
      course.Summarize();
      Console.WriteLine ("\n\nVisitor 2 - Summing the weights\nLabs " 
                  +ElementExtensions.Lab + "% and Tests " 
                  +ElementExtensions.Test + "%");
      
      Console.WriteLine("\n\nVisitor 3 (Interpreter) "); 
      foreach (int [] student in values) {
        Console.Write(student.Display());
        course.SetUp(context, student);
        course.Interpreter();
        Console.WriteLine(" = "+context.Output/100);
      }
    }
  }
  
/* Output
COS333 L2 L2 L2 L2 L2 M25 (L40 T60 ) L10 E55 (L28 T73 ) 

Visitor 1 - Course structure

Course 0
Lab 2
Lab 2
Lab 2
Lab 2
Lab 2
Midterm 25
  Lab 10
  Test 15
Lab 10
Exam 55
  Lab 15
  Test 40


Visitor 2 - Summing the weights
Labs 45% and Tests 55%


Visitor 3 (Interpreter) 
[80, 0, 100, 100, 85, 51, 52, 50, 57, 56] = 56.15
[87, 95, 100, 100, 77, 70, 99, 100, 75, 94] = 89.88
[0, 55, 100, 65, 55, 75, 73, 74, 71, 72] = 70.8
*/