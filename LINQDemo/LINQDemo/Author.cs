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

    [Table( Name = "Authors" )]
    public class Author : IBookCollection
    {
        [Column( IsPrimaryKey = true, IsDbGenerated = true )] public int Id { get; set; }
        [Column] public string Name { get; set; }

        private EntitySet<BookAuthor> _bookAuthors = new EntitySet<BookAuthor>( );
        [Association( Name = "FK_BookAuthors_Authors", Storage = "_bookAuthors", OtherKey = "authorId", ThisKey = "Id" )]
        internal ICollection<BookAuthor> BookAuthors {
            get { return _bookAuthors; }
            set { _bookAuthors.Assign( value ); }
        }
        public ICollection<Book> Books { get { return ( from ba in BookAuthors select ba.Book ).ToList( ); } }
    }
}