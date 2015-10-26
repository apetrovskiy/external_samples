/*********************************************************************
 * A LINQ Tutorial: Mapping Tables to Objects
 * By: Abby Fichtner, http://www.TheHackerChickBlog.com
 * Article URL: http://www.codeproject.com/KB/linq/linqtutorial.aspx
 * Licensed under The Code Project Open License (CPOL)
 *********************************************************************/

using System;
using System.Globalization;
using System.Windows.Data;

namespace LINQDemo
{
    // Specifies how to display prices in the UI
    class PriceConverter: IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
            decimal amount = System.Convert.ToDecimal( value );
            string cost = Math.Round( amount, 2 ).ToString( );
            if( "0".Equals( cost ) ) {
                return "";
            }
            return ( "$" + cost );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
            throw new NotImplementedException( );
        }
    }
}
