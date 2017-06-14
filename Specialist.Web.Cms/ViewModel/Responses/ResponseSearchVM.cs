using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Web.Cms.ViewModel.Responses {
    public class ResponseSearchVM {
        public string TrainerTC { get; set; }

        public decimal? Group_ID { get; set; }

        public bool IsWebinar { get; set; }

        public string CourseTCList { get; set; }

        public byte? ResponseType { get; set; }

        public List<RawQuestionnaire> RawQuestionnaires { get; set; }
    }
}