/*********************************************************************
 * A LINQ Tutorial: Mapping Tables to Objects
 * By: Abby Fichtner, http://www.TheHackerChickBlog.com
 * Article URL: http://www.codeproject.com/KB/linq/linqtutorial.aspx
 * Licensed under The Code Project Open License (CPOL)
 *********************************************************************/

using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace LINQDemo
{
    #pragma warning disable 0169        // disable never used warnings for fields that are being used by LINQ

    [Table( Name = "BookAuthors" )]
    internal class BookAuthor
    {
        [Column( IsPrimaryKey = true, Name = "Author" )] private int authorId;
        private EntityRef<Author> _author = new EntityRef<Author>( );
        [Association( Name = "FK_BookAuthors_Authors", IsForeignKey = true, Storage = "_author", ThisKey = "authorId" )]
        public Author Author {
            get { return _author.Entity; }
            set { _author.Entity = value; }
        }

        [Column( IsPrimaryKey = true, Name = "Book" )] private int bookId;
        private EntityRef<Book> _book = new EntityRef<Book>( );
        [Association( Name = "FK_BookAuthors_Books", IsForeignKey = true, Storage = "_book", ThisKey = "bookId" )]
        public Book Book {
            get { return _book.Entity; }
            set { _book.Entity = value; }
        }
    }
}
