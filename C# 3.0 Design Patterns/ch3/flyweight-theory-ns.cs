  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

  namespace FlyweightPattern {

    // Flyweight Pattern        Judith Bishop  Sept 07
    // Defined for state which is images
  
    // IFlyweight Interface
    public interface IFlyweight {
      void Load (string filename);
      void Display (PaintEventArgs e, int row, int col);
    }

    // Flyweight 
    public struct Flyweight : IFlyweight {
      // Intrinsicstate
      Image pThumbnail;
      public void Load (string filename) {
        pThumbnail = new Bitmap("images/"+filename).
                           GetThumbnailImage(100, 100, null, new IntPtr());
      }

      public void Display(PaintEventArgs e, int row, int col) {
        // Calculating extrinsic state
        e.Graphics.DrawImage(pThumbnail,col*100+10, row*130+40,
                                       pThumbnail.Width,pThumbnail.Height);
      }
    }

    public class FlyweightFactory {
      // Keeps an indexed list of IFlyweight objects in existance
      Dictionary <string,IFlyweight> flyweights =
          new Dictionary <string,IFlyweight> ();

      public FlyweightFactory () {
        flyweights.Clear();
      } 

      public IFlyweight this[string index] {   
        get {  
          if (!flyweights.ContainsKey(index)) 
              flyweights[index] = new Flyweight();
          return flyweights[index]; 
        }  
      }
    }
  }
