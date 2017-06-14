using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class AuthorizationTypeMD : CmsMetaData<AuthorizationType>
    {
        public override void Init()
        {
            this.Display("Тип авторизации");
            this.DisplayBy(x => x.AuthorizationName);
        }
    }
}