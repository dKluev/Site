using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Utils;

namespace Specialist.Web.Root.Profile.Logic {
	public static class HoursBadge {

		public static List<Tuple<int, string>> AllHoursBadge = _.List(
			Tuple.Create(1,
				"Знак отличия «Новичок» - выдается слушателям Центра «Специалист» после завершения ими своего первого учебного курса"),
			Tuple.Create(50,
				"Знак отличия «Специалист» - выдается слушателям Центра «Специалист» после прохождения 50 часов обучения в Центре"),
			Tuple.Create(100,
				"Знак отличия «Эксперт» - выдается слушателям Центра «Специалист» после прохождения 100 часов обучения в Центре"),
			Tuple.Create(150,
				"Знак отличия «Мастер» - выдается слушателям Центра «Специалист» после прохождения 150 часов обучения в Центре"));

	}
}