using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.Extensions
{
    public static class List
    {
        public static void CopyListTo<T>(this List<T> source, List<T> dest)
        {
            //dest = new List<T>(new T[source.Count]);
            for (int i = dest.Count; i < source.Count; i++)
            {
                dest.Add(source[0]);
            }
            for (int i = dest.Count; i > source.Count; i--)
            {
                dest.RemoveAt(i - 1);
            }
            for (int i = 0; i < source.Count; i++)
            {
                dest[i] = source[i];
            }
        }
    }
}
