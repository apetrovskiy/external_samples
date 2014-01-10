  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Windows.Forms;
  using System.Collections.Generic;
  using Given;

  // Decorator Pattern Example                        Judith Bishop  August 2007
  // Draws a single photograph in a window of fixed size
  // Has decorators that are BorderedPhotos and TaggedPhotos that can be composed and added
  // in different combinations

  namespace Given {
   
    // The original Photo class
    public class Photo : Form {
      Image image;
      public Photo () {
        image = new Bitmap("jug.jpg");
        this.Text = "Lemonade";
        this.Paint += new PaintEventHandler(Drawer);
      }

      public virtual void Drawer(Object source, PaintEventArgs e) {
        e.Graphics.DrawImage(image,30,20);
      }
    }
  }

  class DecoratorPatternExample {

    // This simple BorderedPhoto decorator adds a colored BorderedPhoto of fixed size
    class BorderedPhoto : Photo {
      Photo photo;
      Color color;

      public BorderedPhoto (Photo p, Color c) {
        photo = p;
        color=c;
      }

      public override void Drawer(Object source, PaintEventArgs e) {
        photo.Drawer(source, e);
        e.Graphics.DrawRectangle(new Pen(color, 10),25,15,215,225);
      }
    }

    // The TaggedPhoto decorator keeps track of the tag number which gives it 
    // a specific place to be written

    class TaggedPhoto : Photo {
       Photo photo;
       string tag;
       int number;
       static int count;
       List <string> tags = new List <string> ();
      
       public TaggedPhoto(Photo p, string t) {
          photo = p;
          tag = t;
          tags.Add(t);
          number = ++count;
       }

       public override void Drawer(Object source, PaintEventArgs e) {
          photo.Drawer(source,e);
          e.Graphics.DrawString(tag, 
          new Font("Arial", 16), 
          new SolidBrush(Color.Black), 
          new PointF(80,100+number*20));
       }

       public string ListTaggedPhotos() {
          string s = "Tags are: ";
          foreach (string t in tags) s +=t+" ";
          return s;
       }
    }



    static void Main () {
      // Application.Run acts as a simple client
      Photo photo;
      TaggedPhoto foodTaggedPhoto, colorTaggedPhoto, tag;
      BorderedPhoto composition;

      // Compose a photo with two TaggedPhotos and a blue BorderedPhoto
      photo = new Photo();
      Application.Run(photo);
      foodTaggedPhoto = new TaggedPhoto (photo,"Food");
      colorTaggedPhoto = new TaggedPhoto (foodTaggedPhoto,"Yellow");
      composition = new BorderedPhoto(colorTaggedPhoto, Color.Blue);
      Application.Run(composition);
      Console.WriteLine(colorTaggedPhoto.ListTaggedPhotos());

      // Compose a photo with one TaggedPhoto and a yellow BorderedPhoto
      photo = new Photo();
      tag = new TaggedPhoto (photo,"Jug");
      composition = new BorderedPhoto(tag, Color.Yellow);
      Application.Run(composition);
      Console.WriteLine(tag.ListTaggedPhotos());
    }
  }
/* Output

TaggedPhotos are: Food Yellow                                                                                                  
TaggedPhotos are: Food Yellow Jug   
*/
