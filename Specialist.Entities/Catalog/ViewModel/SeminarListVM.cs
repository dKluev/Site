using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.ViewModel
{
    public class SeminarListVM : IViewModel {

        public List<GroupSeminar> GroupSeminars { get; set; }
        public string Title {
            get { return Consultation ? "Консультации" : "Бесплатные семинары Центра"; }
        }

        public List<GroupSeminar> ProbWebinars { get; set; }

        public bool Consultation { get; set; }

	    public SeminarListVM() {
		    ProbWebinars = new List<GroupSeminar>();
	    }
    }
   
}