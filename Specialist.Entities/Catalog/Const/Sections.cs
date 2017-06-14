using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Catalog.Const {
	public static class Sections {

		public const int User = 297;

		public const int NetworkBasic = 390;
		public const int Network = 372;

		public const int Hr = 314;
		public const int Kadr = 371;
		public const int Smeta = 323;
		public const int Buh = 318;
		public const int Director = 376;
		public const int Soul = 353;
		public const int Marketing = 319;
		public const int Student = 424;
		public const int SoftSkills = 403;
		public const int Logistic  = 315;
		public const int MamaSpec  = 385;
		public const int School = 308;

		public static List<int> NotImportant = _.List(MamaSpec); 

		public static List<int> ForInvoice = _.List(Buh, Director, Hr);

		public static class Terms {
			public const int Webinar = 66;
			public const int IntraExtra = 82;
			public const int OpenClasses = 84;
		}

	}
}