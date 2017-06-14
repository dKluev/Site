using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class Banners {
		public static Dictionary<int, List<string>> Target = _.Dict(681, _.List("ПРГСУБД", "СЕТИ", "МС_СЕТИ", "МС_ДРУГ"
			));
	}
}