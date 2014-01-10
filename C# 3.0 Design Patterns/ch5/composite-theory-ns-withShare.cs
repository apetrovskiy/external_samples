  using System;
  using System.Collections.Generic;
  using System.IO;
  using PrototypePattern;

  namespace CompositePattern {
  
    // The Interface                                                                                                                 
    public interface IComponent <T> {
      void Add(IComponent <T> c);
      IComponent <T> Remove(T s);
      string Display(int depth);
      IComponent <T> Find(T s);
      IComponent <T> Share (T s,IComponent <T> home);
      string Name {get; set;}
    }

    // The Composite
    [Serializable()]
    public class Composite <T> : IPrototype <IComponent <T>>, IComponent  <T> {
      List  <IComponent <T>> list;

      public string Name {get; set;}
    
      public Composite (string name)  { 
        Name = name;
        list = new List <IComponent <T>> ();
      }

      public void Add(IComponent  <T> c) {
        list.Add(c);
      }

      // Finds the item from a particular point in the structure
      // and returns the composite from which it was removed
      // If not found, return the point as given
      public IComponent <T> Remove(T s) {
        holder = this;
        IComponent <T> p = holder.Find(s);
        if (holder!=null) {
          (holder as Composite<T>).list.Remove(p);
          return holder;
        } else 
           return this;
      }
    
      IComponent <T> holder=null;
    
      // Recursively looks for an item
      // Returns its reference or else null
      public IComponent <T>  Find (T s) {
        holder = this;
        if (Name.Equals(s)) return this;
        IComponent <T> found=null;
        foreach (IComponent <T> c in list)  {
          found = c.Find(s);
          if (found!=null) 
            break;
        }
        return found;
      }
      
      public IComponent <T> Share (T set, IComponent <T> toHere) {
        IPrototype <IComponent <T>> prototype = this.Find(set) as IPrototype <IComponent <T>>;
        IComponent <T> copy = prototype.DeepCopy() as IComponent<T>;
        toHere.Add(copy);
        return toHere;
      }
        
      // Displays items in a format indicating their level in the composite structure
      public string Display(int depth) {
        String s = new String('-', depth) + "Set "+ Name +  " length :" + list.Count + "\n";
        foreach (IComponent <T> component in list) {
          s += component.Display(depth + 2);
        }
        return s;
      }
    }

    // The Component
    [Serializable()]
    public class Component <T> : IPrototype <IComponent<T>>, IComponent <T> {
      public string Name {get; set;}
      
      public Component (string name)  { 
        Name = name;
      }

      public void Add(IComponent <T> c) {
        Console.WriteLine("Cannot add to an item");
      }

      public IComponent <T>  Remove(T s) {
        Console.WriteLine("Cannot remove directly");
        return this;
      }

      public string Display(int depth) {
        return new String('-', depth) + Name+"\n";
      }
    
      public IComponent <T>  Find (T s) {
        if (s.Equals(Name)) return this; 
        else 
          return null;
      }
      
      public IComponent <T> Share (T set, IComponent  <T>toHere) {
        IPrototype <IComponent <T>> prototype = this.Find(set) as IPrototype <IComponent <T>>;
        IComponent <T> copy = prototype.Clone() as IComponent<T>;
        toHere.Add(copy);
        return toHere;
      }
    }
  }