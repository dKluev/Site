using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;

namespace Specialist.Entities.Profile.ViewModel.Common {
	public class GroupPhotosVM:IViewModel {
		public List<Tuple<decimal,CourseLink>>  Groups { get; set; }
		public Dictionary<decimal, Tuple<string, DateTime>> GroupTrainer { get; set; }
		public string Title { get { return "Фотографии"; } }
	}
}