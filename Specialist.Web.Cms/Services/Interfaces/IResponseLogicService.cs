using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;

namespace Specialist.Web.Cms.Services
{
    public interface IResponseLogicService {
        void ExportToRaws();
        IQueryable<RawQuestionnaire> GetRawQuestionnaires(DateTime? date);
        void ExportToResponse(List<RawQuestionnaire> model);
    }
}