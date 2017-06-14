using System.Web.Compilation;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Web.Cms.Core;
using SimpleUtils;

namespace Specialist.Web.Cms.Controllers {
    public class CompetitionEntityController: BaseController<Competition> {
        protected override string GetComboBoxFilter(string propertyName, object id) {
//            var strongID = LinqToSqlUtil.CorrectIdType(id, MetaData.EntityType);
            if (propertyName == "WinnerID" && id != null)
                return "UserCompetitions.Any(CompetitionID = {0})"
                    .FormatWith(id);
            if (id == null)
                return "false";
            return null;
        }
    }
}