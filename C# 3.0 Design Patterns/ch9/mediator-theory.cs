  using System;
  using System.Collections.Generic;

  class MediatorPattern {
  
     // Mediator Pattern        Judith Bishop Sept 07
     /* The Mediator maintains a list of colleagues and specifies the 
           communication methods that it can mediate, in this case, Send. 
           Receive is implemented at Colleague level and called via a delegate
          supplied by the colleagues to the mediator on signon.  
      */
  
  class Mediator {
    
    public delegate void Callback (string message, string from);
    Callback respond;
  
    public void SignOn (Callback method) {
      respond += method;
    }
    public void Block (Callback method) {
      respond -=method;
    }
    public void Unblock(Callback method) {
      respond +=method;
    }
    
    // Send is implemented as a broadcast
    public void Send(string message, string from) {
      respond(message, from);
      Console.WriteLine();
    }
  }

  class Colleague {
    Mediator mediator;
    protected string name;
      
    public Colleague(Mediator mediator,string name) {
      this.mediator = mediator;
      mediator.SignOn(Receive);
      this.name = name;
    }
    
    public virtual void Receive(string message, string from) {
      Console.WriteLine(name +" received from "+from+": " + message);
    } 
    
    public void Send(string message) {
      Console.WriteLine("Send (From "+name+ "): "+message);
      mediator.Send(message, name);
    }
  }
  
  class ColleagueB :  Colleague {
    public ColleagueB(Mediator mediator,string name) 
        : base (mediator, name) {
  }
 
  // Does not get copies of own messages
  public override void Receive(string message, string from) {
    if (!String.Equals(from,name))
      Console.WriteLine(name +" received from "+from+": " + message);
    } 
  }

  static void Main () {
    Mediator m = new Mediator();
    // Two from head office and one from a branch office
      Colleague head1 = new Colleague(m,"John");
      ColleagueB branch1 = new ColleagueB(m,"David");
      Colleague head2 = new Colleague(m,"Lucy");
      
      head1.Send("Meeting on Tuesday, please all ack");
      branch1.Send("Ack"); // by design does not get a copy
      m.Block(branch1.Receive); // temporarily gets no messages
      head1.Send("Still awaiting some Acks");
      head2.Send("Ack"); 
      m.Unblock(branch1.Receive); //open again
      head1.Send("Thanks all");
    }
  }
/* Output
Send (From John): Meeting on Tuesday, please all ack
John received from John: Meeting on Tuesday, please all ack
David received from John: Meeting on Tuesday, please all ack
Lucy received from John: Meeting on Tuesday, please all ack

Send (From David): Ack
John received from David: Ack
Lucy received from David: Ack

Send (From John): Still awaiting some Acks
John received from John: Still awaiting some Acks
Lucy received from John: Still awaiting some Acks

Send (From Lucy): Ack
John received from Lucy: Ack
Lucy received from Lucy: Ack

Send (From John): Thanks all
John received from John: Thanks all
Lucy received from John: Thanks all
David received from John: Thanks all
*/
