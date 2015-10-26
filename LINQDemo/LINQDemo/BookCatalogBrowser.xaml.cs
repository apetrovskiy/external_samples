/*********************************************************************
 * A LINQ Tutorial: Mapping Tables to Objects
 * By: Abby Fichtner, http://www.TheHackerChickBlog.com
 * Article URL: http://www.codeproject.com/KB/linq/linqtutorial.aspx
 * Licensed under The Code Project Open License (CPOL)
 *********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace LINQDemo
{
    // This file primarily just keeps state and switches the display between books, authors, and catagories
    // See the DataTemplates in the xaml for the data bindings which are used to access the objects and their properties
    // 
    // See Also: LinqTutorialSampleQueries.cs (not invoked by default) 
    //           which will simply perform the queries from the tutorial and write their results to the console
    public partial class BookCatalogBrowser : Window
    {
        private BookCatalog Catalog = new BookCatalog( );
        private string StatusMessage = null;
        private IBookCollection ViewingBooksForAttribute = null;

        public BookCatalogBrowser( ) {
            InitializeComponent( );
            DisplayAllBooks( );
        }

        private void DisplayAuthors( object sender, RoutedEventArgs e ) {
            DisplayAuthors( Catalog.Authors );
        }
        private void DisplayAuthors( IEnumerable<Author> authors ) {
            StatusMessage = "Displaying authors";
            DisplayList( from author in authors
                         orderby author.Name
                         select author );
            AuthorButton.IsEnabled = false;
        }

        private void DisplayAllBooks( ){
            StatusMessage = "Displaying all books";
            DisplayBooks( Catalog.Books );
            BookButton.IsEnabled = false;
        }
        private void DisplayBooks( object sender, RoutedEventArgs e ) {
            DisplayAllBooks( );
        }
        private void DisplayBooks( IEnumerable<Book> books ) {
            DisplayList( from book in books 
                         orderby book.Title 
                         select book );
        }

        private void DisplayCategories( object sender, RoutedEventArgs e ) {
            DisplayCategories( Catalog.Categories );
        }
        private void DisplayCategories( IEnumerable<Category> categories ) {
            StatusMessage = "Displaying categories";
            DisplayList( from category in categories
                         orderby category.Name
                         select category );
            CategoryButton.IsEnabled = false;
        }

        private void DisplayList( IEnumerable dataToList){
            ResetDisplay( );
            Listing.DataContext = dataToList;
        }

        private void LoadBooksForAttribute( object sender, RoutedEventArgs e ) {
            if( sender == null || !( sender is Hyperlink ) ) { return; }
            IBookCollection bookHolder = ( ( sender as Hyperlink ).CommandParameter ) as IBookCollection;
            if( bookHolder == null ) { return; }

            bool alreadyDisplaying = false;
            if( bookHolder.Equals( ViewingBooksForAttribute ) ) {
                System.Media.SystemSounds.Exclamation.Play( );
                alreadyDisplaying = true;
            }

            StatusMessage = ( alreadyDisplaying ? "Already displaying" : "Displaying" ) + " books for " + bookHolder.Name;
            DisplayBooks( bookHolder.Books );
            ViewingBooksForAttribute = bookHolder;
            e.Handled = true;
        }

        private void LoadIndividualBook( object sender, RoutedEventArgs e ){
            if( sender == null || !( sender is Hyperlink ) ){ return; }
            Book book = ( ( sender as Hyperlink ).CommandParameter ) as Book;
            if( book == null ){ return; }

            StatusMessage = "Displaying book details";
            DisplayBooks( new List<Book>( ){ book } );
            e.Handled = true;
        }

        private void ResetDisplay( ) {
            ViewingBooksForAttribute = null;

            AuthorButton.IsEnabled = true;
            BookButton.IsEnabled = true;
            CategoryButton.IsEnabled = true;

            StatusText.Content = StatusMessage;
        }
    }
}

