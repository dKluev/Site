using System;
using System.Collections.Generic;

namespace PrometricImport
{
    public static class StringExtension
    {
        public static string Between(this string str, string end)
        {
            return Between(str, null, end);
        }

        public static string Between(this string str, string start, string end)
        {
            var result = 0;
            return Between(str, start, end, 0, ref result);
        }

        public static List<string> BetweenAll(this string str, string start, string end)
        {
            var result = new List<string>();
            var currentBetween = string.Empty;
            var lastIndex = 0;
            while(lastIndex < str.Length) 
            {
                currentBetween = str.Between(start, end, lastIndex, ref lastIndex);
                if(currentBetween == null)
                    break;
                result.Add(currentBetween);
            }
            return result;
        }

        public static string Between(this string str, string start, string end, 
            int startIndex, ref int lastIndex)
        {
            var index = 0;
            if(start != null)
            {
                var tempIndex = str.IndexOf(start, startIndex);
                if(tempIndex < 0)
                    return null;
                index = tempIndex + start.Length;
            }
            var endIndex = str.IndexOf(end, index);
            lastIndex = endIndex + end.Length;
            return str.Substring(index, endIndex - index);
        }

        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}