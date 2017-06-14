using System;
using System.Linq.Expressions;
using Specialist.Entities.Order.Const;

namespace Specialist.Entities.Context {
	public partial class Extras {

		public bool IsEngBook {
			get {
				return ExtrasName.StartsWith("Учебник АНГЛ");
			}
		}

		public bool IsMicrosoftLab {
			get {
				return ExtrasName.StartsWith("Лабораторная работа к курсу ");
			}
		}

		public bool IsPaint {
			get {
				return ExtrasName.StartsWith("Раздаточный материал к курсу");
			}
		}


		public bool IsTravel {
			get { return Extrases.IsTravel(Extras_ID); }
		}
	}
}