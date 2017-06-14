using System;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel {
    public class VacancyVM: IViewModel {

        public Vacancy Vacancy { get; set; }

        public SimplePage Career { get; set; }

        public string Title {
            get { return "Вакансия: " + Vacancy.Name; }
        }
    }
}