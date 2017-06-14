using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.Profile.ViewModel {
    public class CompetitionsVM: IViewModel {

        public List<Competition> Competitions { get; set; }

        public int CurrentUserID { get; set; }

        public bool MyCompetitions { get; set; }

        public CompetitionsVM(IEnumerable<Competition> competitions) {
            Competitions = competitions.ToList();
        }

        public CompetitionsVM(IEnumerable<Competition> competitions, bool myCompetitions) {
            Competitions = competitions.ToList();
            MyCompetitions = myCompetitions;
        }

        public string Title {
            get { return "Конкурсы"; }
        }
    }
}