  using System;
  using System.Collections.Generic;
  using System.Collections;
  using System.Threading;
  using System.Windows.Forms;
  using System.Drawing;

  class ObserverPattern {
    
    //Observer Pattern            Judith Bishop Sept 2007
    // Demonstrates Blog updates. Observers can subscribe and unsubscribe
    // online through a GUI.
      
    // State type
    public class Blogs {
      public string Name {get; set;}
      public string Topic {get; set;}
    
      public Blogs (string name, string topic) {
        Name = name;
        Topic = topic;
      }
    }
  
    public delegate void Callback (Blogs blog);
          
    // The Subject runs in a thread and changes its state
    // independently by calling the Iterator. At each change, 
    // it notifies its Observers
    // The Callbacks are in a collection based on blogger name
    
    class Subject {
      Dictionary <string,Callback> Notify = new Dictionary <string,Callback> ();
      Simulator simulator = new Simulator();
      const int speed = 4000;
      
      public void Go() {
        new Thread(new ThreadStart(Run)).Start();
      }
    
      void Run () {
        foreach (Blogs blog in simulator) {
          Register(blog.Name); // if necessary
          Notify[blog.Name](blog); // publish changes
          Thread.Sleep(speed); // millisconds
        }
      }
      
      // Adds to the blogger list if unknown
      void Register (string blogger) {
        if (!Notify.ContainsKey(blogger)) {
          Notify[blogger] = delegate {};
        }
      }
    
      public void Attach(string blogger, Callback Update) {
        Register(blogger);
        Notify[blogger] += Update;
      }

      public void Detach(string blogger, Callback Update) {
        // Possible problem here
        Notify[blogger] -= Update;
      }
    }

    class Interact : Form {
      public TextBox wall ;
      public Button subscribeButton, unsubscribeButton ;
      public TextBox messageBox;
      string name;
      
      public Interact(string name, EventHandler Input) {

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
              wall.Font = new Font(wall.Font.Name, 12);
        this.Controls.Add(wall);

        // Panel must be second
        Panel p = new Panel();
        messageBox = new TextBox();
        messageBox.Width = 120;
        p.Controls.Add(messageBox);
        subscribeButton = new Button();
        subscribeButton.Left = messageBox.Width;
        subscribeButton.Text = "Subscribe";
        subscribeButton.Click += new EventHandler(Input);
        p.Controls.Add(subscribeButton);
        unsubscribeButton = new Button();
        unsubscribeButton.Left = messageBox.Width+subscribeButton.Width;
        unsubscribeButton.Text = "Unsubscribe";
        unsubscribeButton.Click += new EventHandler(Input);
        p.Controls.Add(unsubscribeButton);

        p.Height = subscribeButton.Height;
        p.Height = unsubscribeButton.Height;
        p.Dock = DockStyle.Top;
        this.Controls.Add(p);
      }

      public void Output(string message) {
        if (this.InvokeRequired)
          this.Invoke((MethodInvoker)delegate() { Output(message); });
        else {
          wall.AppendText(message + "\r\n");
          this.Show();
        }
      }
    }
    
    // Useful if more observer types
    interface IObserver {
      void Update(Blogs state);
    }

    class Observer : IObserver {
      string name;
      Subject blogs;
      Interact visuals;
        
      public Observer (Subject subject, string name) {
              this.blogs = subject;
              this.name = name;
        visuals = new Interact(name,Input);
        new Thread((ParameterizedThreadStart) delegate(object o) {
             Application.Run(visuals);
        }).Start(this);

        // Wait to load the GUI
        while (visuals == null || !visuals.IsHandleCreated) {
            Application.DoEvents();
            Thread.Sleep(100);
        }
        blogs.Attach("Jim",Update);
        blogs.Attach("Eric",Update);
        blogs.Attach("Judith",Update);
      }
    
      public void Input(object source, EventArgs e) {
        // Subscribe to the specified blogger
        if (source == visuals.subscribeButton) {
          blogs.Attach(visuals.messageBox.Text, Update);
          visuals.wall.AppendText("Subscribed to "+visuals.messageBox.Text+"\r\n");
        } else 
        //Unsubscribe to the blogger
        if (source == visuals.unsubscribeButton) {
          blogs.Detach(visuals.messageBox.Text, Update);
          visuals.wall.AppendText("Unsubscribed from "+visuals.messageBox.Text+"\r\n");
        }
      }

      public void Update(Blogs blog) {
              visuals.Output("Blog from "+blog.Name+" on "+blog.Topic);
      }
    }

    // Iterator to supply the data
    class Simulator : IEnumerable {
        
      Blogs [] bloggers = {new Blogs ("Jim","UML diagrams"),
      new Blogs("Eric","Iterators"),
      new Blogs("Eric","Extension Methods"),
      new Blogs("Judith","Delegates"),
      new Blogs("Eric","Type inference"),
      new Blogs("Jim","Threads"),
      new Blogs("Eric","Lamda expressions"),
      new Blogs("Judith","Anonymous properties"),
      new Blogs("Eric","Generic delegates"),
      new Blogs("Jim","Efficiency")};

      public IEnumerator GetEnumerator () {
        foreach( Blogs blog in bloggers )
          yield return blog;
        }
      }
    
    static void Main () {
      Subject subject = new Subject();
      Observer Observer =  new Observer(subject,"Thabo");
      Observer observer2 = new Observer(subject,"Ellen");
      subject.Go();
    }
  }
