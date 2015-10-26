/*********************************************************************
 * A LINQ Tutorial: Mapping Tables to Objects
 * By: Abby Fichtner, http://www.TheHackerChickBlog.com
 * Article URL: http://www.codeproject.com/KB/linq/linqtutorial.aspx
 * Licensed under The Code Project Open License (CPOL)
 *********************************************************************/

using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace LINQDemo
{
    #pragma warning disable 0169        // disable never used warnings for fields that are being used by LINQ

    [Table( Name = "Books" )]
    public class Book
    {

        [Column( IsPrimaryKey = true, IsDbGenerated = true )] public int Id { get; set; }
        [Column] public string Title { get; set; }
        [Column] public decimal Price { get; set; }

        [Column( Name = "Category" )] private int? categoryId;
        private EntityRef<Category> _category = new EntityRef<Category>( );
        [Association( Name = "FK_Books_BookCategories", IsForeignKey = true, Storage = "_category", ThisKey = "categoryId" )]
        public Category Category {
            get { return _category.Entity; }
            set { _category.Entity = value; }
        }

        private EntitySet<BookAuthor> _bookAuthors = new EntitySet<BookAuthor>( );
        [Association( Name = "FK_BookAuthors_Books", Storage = "_bookAuthors", OtherKey = "bookId", ThisKey = "Id" )]
        internal ICollection<BookAuthor> BookAuthors {
            get { return _bookAuthors; }
            set { _bookAuthors.Assign( value ); }
        }

        public ICollection<Author> Authors { get { return ( from ba in BookAuthors select ba.Author ).ToList( ); } }
    }
}

