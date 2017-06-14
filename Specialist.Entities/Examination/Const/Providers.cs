using Specialist.Entities.Catalog.Const;

namespace Specialist.Entities.Examination.Const
{
    public static class Providers
    {
        public const int Prometric = 2;
        public const int Vue = 1;

        public const string PrometricExamType = "Prometric";
        public const string VueExamType = "VUE";

		public static int GetByType(string type) {
			switch (type) {
				case VueExamType:
					return Vue;
				default:
					return Prometric;
			}
		}

		public static int? GetVendor(int providerId) {
			switch (providerId) {
				case Prometric:
					return Vendors.Prometric;
				case Vue:
					return Vendors.Vue;
			}
			return null;
		}

        
    }
}