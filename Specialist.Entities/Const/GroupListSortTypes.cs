using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class GroupListSortTypes {
		public const int Date = 0;
		public const int PriceInc = 1;
		public const int PriceDesc = 2;
		public const int Popular = 3;
		public const int New = 4;
		public const int Discount = 5;

		public static readonly List<Tuple<int,string>> All = _.List(
			Tuple.Create(Date, "по дате"),
			Tuple.Create(PriceInc, "по возрастанию цены"),
			Tuple.Create(PriceDesc, "по убыванию цены"),
			Tuple.Create(Popular, "по популярности"),
			Tuple.Create(New, "по новинкам"),
			Tuple.Create(Discount, "по скидке")
			);

		public static List<Group> Sort(int type, Dictionary<string,int> priceIndex, List<Group> groups) {
			switch (type) {
				case(Date): return groups.OrderBy(x => x.DateBeg).ToList();
				case(PriceInc): return groups.OrderBy(x => priceIndex.GetValueOrDefault(x.Course_TC)).ToList();
				case(PriceDesc): return groups.OrderByDescending(x => priceIndex.GetValueOrDefault(x.Course_TC)).ToList();
				case(Popular): 
					return groups.OrderByDescending(x => x.GroupCalc.NumOfStudents + x.GroupCalc.NumOfWebinarists)
						.ToList();
				case(New): return groups.OrderByDescending(x => x.Course.Course_ID).ToList();
				case(Discount): return groups.OrderByDescending(x => x.Discount.GetValueOrDefault()).ToList();
			}
			return groups;
		} 
	}
}