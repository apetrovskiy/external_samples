/*********************************************************************
 * A LINQ Tutorial: Mapping Tables to Objects
 * By: Abby Fichtner, http://www.TheHackerChickBlog.com
 * Article URL: http://www.codeproject.com/KB/linq/linqtutorial.aspx
 * Licensed under The Code Project Open License (CPOL)
 *********************************************************************/

using System.Data.Linq;
using System.Data.Linq.Mapping;
using System;

namespace LINQDemo
{
#pragma warning disable 0169, 0649        // disable never used/assigned warnings for fields that are being used by LINQ

    [Database]
    public class BookCatalog : DataContext
    {
        public Table<Author> Authors;
        public Table<Book> Books;
        public Table<Category> Categories;

        public BookCatalog( ) : base( "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\BookCatalog.mdf;Integrated Security=True;User Instance=True" ) { }
    }
}
