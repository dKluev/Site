using System;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Profile.ViewModel
{
    public class CustomerTypeChoiceVM: IViewModel
    {
        public string CustomerType { get; set; }
		public bool IsRegister { get; set; }

	    public string NextUrl { get; set; }
        public string ActionUrl { get; set; }
        public string Title {
            get { return "Выбор статуса"; }
        }
    }
}