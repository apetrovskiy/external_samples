/*********************************************************************
 * A LINQ Tutorial: Mapping Tables to Objects
 * By: Abby Fichtner, http://www.TheHackerChickBlog.com
 * Article URL: http://www.codeproject.com/KB/linq/linqtutorial.aspx
 * Licensed under The Code Project Open License (CPOL)
 *********************************************************************/

using System.Collections.Generic;

namespace LINQDemo
{
    // For objects that can contain a set of books 
    // (just helps reduce some duplication in the UI code)
    interface IBookCollection
    {
        string Name { get; set; }
        ICollection<Book> Books { get; }
    }
}
