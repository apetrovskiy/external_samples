  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;

  //  Iterator Pattern Example        Judith Bishop   September 2007
  //  Illustrates the use of LINQ with iterators on a tree structure
  
  class Person {
    public Person() {}
    public string Name {get; set;}
    public int Birth {get; set;}

    public Person (string name, int birth) {
      Name = name;
      Birth = birth;
    }

    public override string ToString () {
      return ("["+Name+", "+Birth+"]");
    }
  }

  class Node <T> {
    public Node () {}
    public Node <T> Left{get; set;}
    public Node <T> Right {get; set;}
    public T Data {get; set;}

    public Node (T d, Node <T> left, Node <T> right) {
      Data = d;
      Left = left;
      Right = right;
    }
  }

  // T is the data type. The Node type is built-in
  class Tree <T>  {
    Node <T> root;
    public Tree() {}
    public Tree (Node <T> head) {
      root = head;
    }

    public IEnumerable <T> Preorder {
      get {return ScanPreorder (root);}
    }

    //  Enumerator with Filter
    public IEnumerable <T> Where (Func <T, bool> filter){
      foreach (T p in ScanPreorder(root))
        if (filter(p)==true)
          yield return p;
    }

    // Enumerator with T as Person
    private IEnumerable <T> ScanPreorder (Node <T> root) {
      yield return root.Data;
      if (root.Left !=null) 
        foreach (T p in ScanPreorder (root.Left)) 
          yield return p;
      if (root.Right !=null) 
        foreach (T p in ScanPreorder (root.Right)) 
          yield return p;
    }
  }

  class IteratorPattern {

  // Iterator Pattern for a Tree     Judith Bishop  September 2007
  // Shows two enumerators using links and recursion

    static void Main() {
      var family = new Tree <Person> ( new Node <Person> 
        (new Person ("Tom", 1950),
          new Node <Person> (new Person ("Peter", 1976), 
            new Node <Person> 
              (new Person ("Sarah", 2000), null, 
            new Node <Person> 
              (new Person ("James", 2002), null, 
            null)// no more siblings James
          ),
          new Node <Person> 
            (new Person ("Robert", 1978), null, 
          new Node <Person> 
            (new Person ("Mark", 1980), 
              new Node <Person> 
                (new Person ("Carrie", 2005), null, null),
              null)// no more siblings Mark
            )),
          null) // no siblings Tom
        );

        Console.WriteLine("Full family");
        foreach (Person p in family.Preorder) 
          Console.Write(p+"  ");
        Console.WriteLine("\n");

        // Older syntax
        var selection = family.
          Where(p=> p.Birth > 1980);

        // New syntax
        selection = from p in family
            where p.Birth > 1980
            orderby p.Name
            select p;
              
        Console.WriteLine("Born after 1980 in alpha order");
        foreach (Person p in selection) 
            Console.Write(p+"   ");
        Console.WriteLine("\n");
    }
  }


