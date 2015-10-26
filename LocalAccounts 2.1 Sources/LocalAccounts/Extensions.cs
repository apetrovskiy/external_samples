using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAccounts
{
    public static class Extensions
    {

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) { throw new ArgumentException(); }
            if (action == null) { throw new ArgumentException(); }

            foreach (T element in source)
            {
                action(element);
            }
        }
    }
}