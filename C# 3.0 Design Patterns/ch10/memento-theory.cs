  using System;
  using System.IO;
  using System.Runtime.Serialization;
  using System.Runtime.Serialization.Formatters.Binary;
  using System.Collections.Generic;
  using System.Collections;

  // Memento Pattern                Judith Bishop     September 2007
  //  Simple illustration with undo
  
  class MementoPattern {

    // Client
    static void Main() {
      
      //References to the mementos
      Caretaker [] c = new Caretaker [10];
      Originator originator = new Originator();
      
      int move = 0;
      // Iterator for the moves
      Simulator simulator = new Simulator();
      
      foreach (string command in simulator) {
        // Check for undo
        if (command[0]=='*' && move > 0) 
          originator.Restore(c[move-1].Memento);
        else  
          originator.Operation(command);
        move++;
        c[move] = new Caretaker();
        c[move].Memento = originator.Save();
      } 
    }

    // Originator 
    [Serializable()]
    class Originator {
      List <string> state = new List <string> ();
      
      public void Operation (string s) {
        state.Add(s);
        foreach (string line in state)
          Console.WriteLine(line);
        Console.WriteLine("=======================");
      }
      
      // The reference to the memento is passed back to the client
      public Memento SetMemento() { 
        Memento memento = new Memento();
        return memento.Save(state);
      }

      public void GetMemento(Memento memento) {
        state = (List <string>) memento.Restore();
      }
    }

    [Serializable()]
    // Serializes by deep copy to memory and back
    class Memento {
      MemoryStream stream = new MemoryStream();
      BinaryFormatter formatter = new BinaryFormatter();

      public Memento Save (object o) {
        formatter.Serialize(stream, o);
        return this;
      }

      public object Restore() {
        stream.Seek(0, SeekOrigin.Begin);
        object o = formatter.Deserialize(stream);
        stream.Close();
        return o;
      }
    }

    class Caretaker {
      public Memento Memento {get; set;}
    }
  
    class Simulator : IEnumerable {
  
      string [] lines = {
         "The curfew tolls the knell of parting day",
          "The lowing herd winds slowly o'er the lea",
          "Uh hum uh hum",
         "*UNDO",
         "The plowman homeward plods his weary way",
         "And leaves the world to darkness and to me."};
         
      public IEnumerator GetEnumerator () {
        foreach( string element in lines )
          yield return element;
      }
    }
  }
/* Output
The curfew tolls the knell of parting day
=======================
The curfew tolls the knell of parting day
The lowing herd winds slowly o'er the lea
=======================
The curfew tolls the knell of parting day
The lowing herd winds slowly o'er the lea
Uh hum uh hum
=======================
The curfew tolls the knell of parting day
The lowing herd winds slowly o'er the lea
The plowman homeward plods his weary way
=======================
The curfew tolls the knell of parting day
The lowing herd winds slowly o'er the lea
The plowman homeward plods his weary way
And leaves the world to darkness and to me.
=======================
*/