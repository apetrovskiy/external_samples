  using System;
  using System.Xml;
  using System.Reflection;
  using System.Collections;
  using System.Collections.Generic;
  using System.Windows.Forms;

  public class Mirror {
  
    // Mirrors    by Hans Lombard June 2006, revised Sept 2007
    //                based on Views and Views-2 by Nigel Horspool, Judith Bishop and D-J Miller
    // A general purpose interpreter for any .NET API
    // Reads XML and executes the methods it represents
    // This example assumes the Windows Form API only in the final line where 
    // Application.Run is called.
    
    Stack objectStack;
    List<Command> commands;
    public object CurrentObject { get { return objectStack.Peek(); } }
    public XmlTextReader Reader { get; set; }
    public object LastObject { get; set; }

    public Mirror(string spec) {
      objectStack = new Stack();
      objectStack.Push(null);

      //Register the commands
      commands = new List<Command>();
      commands.Add(new ElementCommand());
      commands.Add(new EndElementCommand());
      commands.Add(new AttributeCommand());

      Reader = new XmlTextReader(spec);
      while (Reader.Read()) {
        InterpretCommands();

        bool b = Reader.IsEmptyElement;
        if (Reader.HasAttributes) {
          for (int i = 0; i < Reader.AttributeCount; i++) {
            Reader.MoveToAttribute(i);
            InterpretCommands();
          }
        }
        if (b) Pop();
      }
    }
    
    public void InterpretCommands() {
      //Run through the commands and interpret
      foreach (Command c in commands) 
        c.Interpret(this);
    }

    public void Push(object o) {
      objectStack.Push(o);
    }

    public void Pop() {
      LastObject = objectStack.Pop();
    }

    public object Peek() {
      return objectStack.Peek();
    }
  }

  public abstract class Command {
    public abstract void Interpret (Mirror context);
  }

  //Handles an XML element. Creates a new object which reflects the XML
  //element name
  public class ElementCommand : Command {
    public override void Interpret (Mirror context) {
      if (context.Reader.NodeType != XmlNodeType.Element) return;
      Type type = GetTypeOf(context.Reader.Name);
      if (type == null) return;
      object o = Activator.CreateInstance(type);

      if (context.Peek() != null)
        ((Control)context.Peek()).Controls.Add((Control)o);
      context.Push(o);
    }

    public Type GetTypeOf(string s) {
      string ns = "System.Windows.Forms";
      Assembly asm = Assembly.Load("System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
      Type type = asm.GetType(ns + "." + s);
      return type;
    }
  }

  //Handles an XML end element. Removes the element from the object stack.
  public class EndElementCommand : Command {
    public override void Interpret (Mirror context) {
      if (context.Reader.NodeType != XmlNodeType.EndElement) return;
      context.Pop();
    }
  }

  //Applies attributes to the current object. The attributes reflects to the properties
  //of the object
  public class AttributeCommand : Command {
    public override void Interpret (Mirror context) {
      if (context.Reader.NodeType != XmlNodeType.Attribute) return;
      SetProperty(context.Peek(), context.Reader.Name, context.Reader.Value);
    }

    public void SetProperty(object o, string name, string val) {
      Type type = o.GetType();
      PropertyInfo property = type.GetProperty(name);

          //Find an appropriate property to match the attribute name
      if (property.PropertyType.IsAssignableFrom(typeof(string))) {
        property.SetValue(o, val, null);
      } else if (property.PropertyType.IsSubclassOf(typeof(Enum))) {
        object ev = Enum.Parse(property.PropertyType, val, true);
        property.SetValue(o, ev, null);
      } else {
        MethodInfo m = property.PropertyType.GetMethod("Parse", new Type[] { typeof(string) });
        object newval = m.Invoke(null /*static */, new object[] { val });
        property.SetValue(o, newval, null);
      }
    }
  }

  public class MainClass {
    public static void Main() {
      Mirror m = new Mirror("calc_winforms.xml");
      Application.Run((Form)m.LastObject);
    }
  }
