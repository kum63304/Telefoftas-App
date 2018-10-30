using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;



namespace System.Linq
{
    public delegate int SkipSize();
    public class Partitioner<T>
    {
        public int currentIndex;
        public Func<T, int, bool> partitioner;
    }
    public static class Extensions
    {
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, int index, T value)
        {
            return enumerable.Select((x, i) => index == i ? value : x);
        }
       
      
    }

}
