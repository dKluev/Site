using System;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;

namespace Specialist.Web.Root.Catalog.Views {
	public class CourseListView {
		public static string ShowCoursePrice(Tuple<decimal?, decimal?> price, decimal discount) {
			if (discount >= 100) {
				return CommonTexts.FreeCourse;
			}
			if (discount > 0) {
				var coef = (1 - discount / 100);
				return Htmls2.OldNewPrice(price.Item1,
					PriceUtils.GetPriceWithCoefficient(price.Item1, false, coef).Item1);
			}
			return Htmls2.OldNewPrice(price.Item2, price.Item1);
		}
	}
}