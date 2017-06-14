using System;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel {
    public class VacancyListVM: IViewModel {

        public PagedList<Vacancy> Vacancies { get; set; }

        public SimplePage Career { get; set; }

	    public bool IsPartner { get; set; }
        public string Title {
            get { return "Вакансии " + (IsPartner ? "партнеров" : ""); }
        }
    }
}