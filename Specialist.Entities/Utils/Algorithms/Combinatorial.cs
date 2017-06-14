using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleUtils
{
    public static class Combinatorial
    {
        public static List<List<int>> GetAllSubset(int n)
        {
            var result = new List<List<int>>();
            for (var i = 0; i < Math.Pow(2, n); i++)
            {
                var str = Convert.ToString(i, 2).Reverse().ToArray();
                var subResult = new List<int>();
                for (var j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                        subResult.Add(j);

                }
                result.Add(subResult);
            }

            return result;
        }
    }
}