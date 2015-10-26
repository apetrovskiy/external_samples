/*********************************************************************
 * A LINQ Tutorial: Mapping Tables to Objects
 * By: Abby Fichtner, http://www.TheHackerChickBlog.com
 * Article URL: http://www.codeproject.com/KB/linq/linqtutorial.aspx
 * Licensed under The Code Project Open License (CPOL)
 *********************************************************************/

using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace LINQDemo
{
    #pragma warning disable 0169        // disable never used warnings for fields that are being used by LINQ

    [Table( Name = "BookCategories" )] 
    public class Category : IBookCollection
    {
        [Column( IsPrimaryKey = true, IsDbGenerated = true )] public int Id { get; set; }
        [Column] public string Name { get; set; }

        private EntitySet<Book> _books = new EntitySet<Book>();
        [Association( Name = "FK_Books_BookCategories", Storage = "_books", OtherKey = "categoryId", ThisKey = "Id" )]
        public ICollection<Book> Books {
            get { return _books; }
            set { _books.Assign( value ); }
        }
    }
}
