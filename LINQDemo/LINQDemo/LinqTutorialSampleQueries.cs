/*********************************************************************
 * A LINQ Tutorial: Mapping Tables to Objects
 * By: Abby Fichtner, http://www.TheHackerChickBlog.com
 * Article URL: http://www.codeproject.com/KB/linq/linqtutorial.aspx
 * Licensed under The Code Project Open License (CPOL)
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQDemo
{
    // This class contains the sample queries from the LINQ Tutorial article on The Code Project
    // It writes the results of each query to the console
    // It is not invoked anywhere by default in the application - but you can just put a call to Run() from anywhere in the app to invoke it
    class LinqTutorialSampleQueries
    {
        // This method calls each of the sample query methods and write the results to the console
        public static void Run( ){
            RetrieveYourDataWithLinqExamples( );
            AccessingDataFromaM1Relationship(  );
            AccessingDataFroma1MRelationship( );
            AccessingDataFromaMmRelationship( );
        }

        // Listed in the order they appear in the tutorial
        public static void RetrieveYourDataWithLinqExamples( ){
            BookCatalog bookCatalog = new BookCatalog( );

            Console.WriteLine( "----------------------------------------------------------------" );
            Console.WriteLine( "ALL BOOKS" );
            Console.WriteLine( "*********" );
            foreach( Book book in bookCatalog.Books ) {
                string title = book.Title;
                decimal price = book.Price;
                Console.WriteLine( title + ": $" + price );
            }


            IEnumerable<Book> cheapBooks = from book in bookCatalog.Books
                                           where book.Price.CompareTo( 30m ) < 0
                                           select book;

            IEnumerable<Book> sortedBooks = from book in cheapBooks
                                            orderby book.Title
                                            select book;

            Console.WriteLine( "----------------------------------------------------------------" );
            Console.WriteLine( "SORTED CHEAP BOOKS (Chain of LINQ Queries)" );
            Console.WriteLine( "******************************************" );
            foreach( Book book in sortedBooks ) {
                string title = book.Title;
                decimal price = book.Price;
                Console.WriteLine( title + ": $" + price );
            }
        }

        public static void AccessingDataFromaM1Relationship( ) {
            BookCatalog bookCatalog = new BookCatalog( );

            Console.WriteLine( "----------------------------------------------------------------" );
            Console.WriteLine( "BOOK CATEGORIES (Accessing Data from a M:1 Relationship)" );
            Console.WriteLine( "*******************************************************" );
            foreach( var book in bookCatalog.Books ) {
                string categoryName = book.Category.Name;
                Console.WriteLine( categoryName + " (" + book.Title + ")" );
            }
        }

        public static void AccessingDataFroma1MRelationship( ) {
            BookCatalog bookCatalog = new BookCatalog( );

            Console.WriteLine( "----------------------------------------------------------------" );
            Console.WriteLine( "BOOKS IN A CATEGORY (Accessing Data from a 1:M Relationship)" );
            Console.WriteLine( "************************************************************" );
            foreach( var category in bookCatalog.Categories ) {
                Console.WriteLine( category.Name + ":" );
                foreach( Book book in category.Books ) {
                    string bookTitle = book.Title;
                    Console.WriteLine( "    - " + bookTitle );
                }
            }
        }

        public static void AccessingDataFromaMmRelationship( ){
            BookCatalog bookCatalog = new BookCatalog( );

            Console.WriteLine( "----------------------------------------------------------------" );
            Console.WriteLine( "BOOKS (Accessing Data from a M:M Relationship)" );
            Console.WriteLine( "**********************************************" );
            foreach( var book in bookCatalog.Books ) {
                string title = book.Title;
                decimal price = book.Price;
                string category = book.Category.Name;

                ICollection<Author> authors = book.Authors;
                ICollection<Book> otherBooksInCategory = book.Category.Books;

                Console.WriteLine( title + " $" + price + " (" + category + "):" );
                Console.Write( "    - Author(s): " );
                foreach( var author in authors ) { Console.Write( author.Name + ", " ); }
                Console.Write( "\r\n    - Other Book(s) in Category: " );
                foreach( var otherBook in otherBooksInCategory ) { Console.Write( otherBook.Title + ", " ); }
                Console.WriteLine( "\r\n" );
            }

            Console.WriteLine( "AUTHORS (Accessing Data from a M:M Relationship)" );
            Console.WriteLine( "************************************************" );
            foreach( var author in bookCatalog.Authors ) {
                string name = author.Name;
                ICollection<Book> books = author.Books;

                Console.WriteLine( name + ":" );
                foreach( var book in books ) { Console.WriteLine( "    - " + book.Title ); }
            }

            Console.WriteLine( "CATEGORIES (Accessing Data from a M:M Relationship)" );
            Console.WriteLine( "**************************************************" );
            foreach( var category in bookCatalog.Categories ) {
                string name = category.Name;

                Console.WriteLine( name + ":" );
                foreach( var book in category.Books ) {
                    string bookTitle = book.Title;
                    ICollection<Author> bookAuthors = book.Authors;

                    Console.WriteLine( "    - " + bookTitle + ":" );
                    foreach( var author in bookAuthors ) { Console.WriteLine( "        - " + author.Name ); }
                }

                Console.WriteLine("");
            }
        }
    }
}
