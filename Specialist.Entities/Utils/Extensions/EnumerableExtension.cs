using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Util;
using SimpleUtils.Collections.Extensions;

namespace SimpleUtils.Extension
{
    public static class EnumerableExtension
    {
        public static IEnumerable<IGrouping<string, T>>
        GroupByFirstLetter<T>(this IEnumerable<T> list, Func<T, string> nameSelector)
        {
            var result =
                from e in list
                group e by nameSelector(e).First().ToString().ToUpper() into gr
                select gr;
            return result;
        }

        public static Dictionary<K, M> DistinctToDictionary<T, K, M>(this IEnumerable<T> list, 
			Func<T,K> keySelector, Func<T,M> elementSelector) {
        	return list.Distinct(x => keySelector(x)).ToDictionary(keySelector, elementSelector);
        }

		public static List<List<T>> CutInPartCount<T>(this List<T> list, int inPartCount) {
			if(!list.Any())
				return new List<List<T>>();
			var partCount = list.Count / inPartCount;
			if (list.Count % inPartCount != 0)
				partCount += 1;
			return list.Cut(partCount);
		}

		public static List<List<T>> GetRows<T>(this IList<T> list, int rowSize)
        {
            var result = new List<List<T>>();
			var part = new List<T>();
            for (int i = 0; i < list.Count;i++) {
            	var last = i == list.Count - 1;
            	var elem = list[i];
				part.Add(elem);
				if(part.Count == rowSize || last) {
					result.Add(part);
					part = new List<T>();
				}
            }
            return result;
        }

		 public static List<List<IGrouping<Tk, Te>>> GetColumns<Tk, Te>(
            this List<IGrouping<Tk, Te>> groupList, int columnCount, 
			 int headWeight)
        {
            var result = new List<List<IGrouping<Tk, Te>>>();

		 	var groupsCounts = groupList.Select(x => new {
		 		Count = headWeight + x.Count(),
		 		Group = x
		 	});

		 	var itemCount = groupsCounts.Sum(x => x.Count);
		 	var columnLength = itemCount%columnCount == 0
		 		? itemCount/columnCount
		 		: itemCount/columnCount + 1;
		 	var currentLength = 0;
		 	var currentList = new List<IGrouping<Tk, Te>>();
		 	foreach (var group in groupsCounts) {
		 		if(currentLength >= columnLength 
					|| (currentLength + group.Count) >= columnLength + 5) {
		 			result.Add(currentList);
					currentList = new List<IGrouping<Tk, Te>>();
		 			currentLength = 0;
		 		}
				currentList.Add(group.Group);
		 		currentLength += group.Count;
		 	}
			if (currentList.Any()) {
				if (result.Count == columnCount) {
					result.Last().AddRange(currentList);
				}
				else {
					result.Add(currentList);
				}
				
			}
   
            return result;
        }
    }
}