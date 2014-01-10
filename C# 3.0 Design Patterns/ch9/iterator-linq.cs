  using System;
  using System.Collections.Generic;
  using System.Linq;

  class IteratorPattern {
  
    // Simple iterator usage    Judith Bishop  September 2007
    // Has an iterator with a Where filter implemented by a delegate

    static void Main() {
  
      Dictionary <string, int> daysInMonths = new Dictionary <string, int> {
          {"January", 31},    {"February", 28},
          {"March", 31},      {"April", 30},
          {"May", 31},         {"June", 30},
          {"July", 31},          {"August", 31},
          {"September", 30}, {"October", 31},
          {"November", 30},  {"December", 31}};

      var selection = from n in daysInMonths
              where n.Key.Length> 5
              select n;
      
      selection = from n in selection
              where n.Value ==31
              orderby n.Key
              select n;
      
      foreach (KeyValuePair<string,int> n in selection) 
            Console.Write(n+"   ");
      Console.WriteLine("\n");
    }
}
/*Output
Long named Months at 31
[January, 31]   [August, 31]   [October, 31]   [December, 31]   

[August, 31]   [December, 31]   [January, 31]   [October, 31]   
*/