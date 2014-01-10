using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary; 
using CompositePattern;
using PrototypePattern;

//  Prototype Pattern Pattern		August 2007
//  Makes use of the Photo Librray examples
// Shares (i.e. deep copys) parts of the hierarchy, then makes changes
  
  // The Client
 class CompositePatternExample {
	 
    static void Main () {
	    IComponent <string> album = new Composite<string> ("Album");
        IComponent <string> point = album;
		IComponent <string> archive = new Composite<string> ("Archive");
		string [] s;
		string command, parameter;
      // Create and manipulate a structure 
		StreamReader instream = new StreamReader("prototype.dat");
	    do {
		  string t= instream.ReadLine();
		  Console.WriteLine("\t\t\t\t"+t);
		  s = t.Split();
		  command = s[0];
		  if (s.Length>1) parameter = s[1]; else parameter = null;
		  switch (command) {
			  case "AddSet"   :   IComponent <string> c = 
												new Composite <string> (parameter);
				                         point.Add(c);
								         point = c;
										break;
			  case "AddPhoto":  point.Add(new Component <string> (parameter)); 
				                       break;
			  case "Remove"   : point = point.Remove(parameter); 
									   break;
			  case "Find"        :  point = album.Find(parameter);  break;
			  case "Display"    :  if (parameter==null)
											Console.WriteLine(album.Display(0));
										else 
											Console.WriteLine(archive.Display(0));
										break;
			  case "Archive"	 :  archive = point.Share(parameter,archive); break;
			  case "Retrieve"  :  point = archive.Share(parameter,album); break;
			  case "Quit"        : break;
		}

	} while (!command.Equals("Quit"));
	}
}
/*Output
	AddSet Home
				AddPhoto Dinner.jpg
				AddSet Pets
				AddPhoto Dog.jpg
				AddPhoto Cat.jpg
				Find Album
				AddSet Garden
				AddPhoto Spring.jpg
				AddPhoto Summer.jpg
				AddPhoto Flowers.jpg
				AddPhoto Trees.jpg
				Display
Set Album length :2
--Set Home length :2
----Dinner.jpg
----Set Pets length :2
------Dog.jpg
------Cat.jpg
--Set Garden length :4
----Spring.jpg
----Summer.jpg
----Flowers.jpg
----Trees.jpg

				Find Pets
				Archive Pets
				Display Archive
Set Archive length :1
--Set Pets length :2
----Dog.jpg
----Cat.jpg

				Find Album
				Remove Home
				Find Album
				Remove Garden
				Display
Set Album length :0

				Retrieve Pets
				Display
Set Album length :1
--Set Pets length :2
----Dog.jpg
----Cat.jpg

				Quit
*/
  



