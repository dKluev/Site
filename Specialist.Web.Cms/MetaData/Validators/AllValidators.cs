using System;
using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Web.Cms.MetaData.Validators {
	public static class AllValidators {
		public static Dictionary<Type, object> List = new Dictionary<Type, object>{
		{typeof (CityInfo), new CityInfoValidator()} };
}
}