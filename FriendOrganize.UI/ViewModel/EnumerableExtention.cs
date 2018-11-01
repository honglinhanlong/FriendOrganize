using System;
using System.Collections.Generic;

namespace FriendOrganize.UI.ViewModel
{
    public static class EnumerableExtention
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (T item in @this)
            {
                action(item);
            }
        }
    }
}
