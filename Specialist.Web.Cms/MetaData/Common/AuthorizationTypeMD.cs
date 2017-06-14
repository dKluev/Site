using FluentMetaData.Core;
using Specialist.Entities.Context;
using FluentMetaData.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class AuthorizationTypeMD : BaseMetaData<AuthorizationType>
    {
        public override void Init()
        {
            this.DisplayBy(x => x.AuthorizationName); 
        }
    }
}