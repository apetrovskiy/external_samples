  using System;
  using System.Collections.Generic;
  using System.Windows.Forms;
  using System.Drawing;
  using System.Threading;

  // Mediator Pattern        Judith Bishop Sept 07
  // This version of the Pattern does not need a list of Colleagues in
  // the Mediator. The delegate keeps a record of the methods on its chain.
  // All other information stays in the colleague itself.
  
  namespace MediatorSpace {
    public delegate void Callback(string message, string from);
    
    // The Mediator is in two parts: Interact handles the GUI 
    // Mediator itself sets up the threads and does the 
    // broadcast from the delegate (Send method)
    
    class Mediator {
      event Callback Respond;
      
      public void SignOn (string name, Callback Receive, Interact visuals) {
        // Add the Collegue to the delegate chain
        Respond += Receive;
        new Thread((ParameterizedThreadStart) delegate(object o) {
           visuals.InputEvent += Send;
           Application.Run(visuals);
        }).Start(this);

         // Wait to load the GUI
         while (visuals == null || !visuals.IsHandleCreated) {
          Application.DoEvents();
          Thread.Sleep(100);
      }
    }
    
    // Send implemented as a broadcast
    public void Send(string message, string from) {
      // Message not sent if it contains "work"
      if (message.IndexOf("work")==-1)
        if (Respond != null)
          Respond(message, from);
    }
  }

  class Interact : Form {
    TextBox wall ;
    Button sendButton ;
    TextBox messageBox;
    string name;
    
    public Interact(string name) {

      Control.CheckForIllegalCrossThreadCalls = true;
      // wall must be first!
      this.name = name;
      wall = new TextBox();
      wall.Multiline = true;
      wall.Location = new Point(0, 30);
      wall.Width = 300;
      wall.Height = 200;
      wall.AcceptsReturn = true;
      wall.Dock = DockStyle.Fill;
      this.Text = name;
      this.Controls.Add(wall);

      // Panel must be second
      Panel p = new Panel();
      messageBox = new TextBox();
      messageBox.Width = 150;
      p.Controls.Add(messageBox);
      sendButton = new Button();
      sendButton.Left = messageBox.Width;
      sendButton.Text = "Send";
      sendButton.Click += new EventHandler(Input);
      p.Controls.Add(sendButton);

      p.Height = sendButton.Height;
      p.Dock = DockStyle.Top;
      this.Controls.Add(p);
    }
    
    protected override void OnFormClosed(FormClosedEventArgs e) {
      base.OnFormClosed(e);
    }

    public event Callback InputEvent;

    public void Input(object source, EventArgs e) {
      if (InputEvent != null)
        InputEvent(messageBox.Text, name);
    }

    public void Output(string message) {
      if (this.InvokeRequired)
        this.Invoke((MethodInvoker)delegate() { Output(message); });
      else {
        wall.AppendText(message + "\r\n");
        messageBox.Clear();
        this.Show();
      }
    }
  }

  class Colleague {
    Interact visuals;
    string name;
      
    public Colleague(Mediator mediator, string name) {
      this.name = name;
      visuals = new Interact(name);
      mediator.SignOn(name, Receive, visuals);
    }

    public void Receive(string message, string from) {
      visuals.Output(from + ": " + message);
    }
  }

  class MediatorPattern {
    static void Main () {
        Mediator m = new Mediator();

        Colleague chat1 = new Colleague(m, "John");
        Colleague chat2 = new Colleague(m, "David");
        Colleague chat3 = new Colleague(m, "Lucy");
      }
    }
  }
  

