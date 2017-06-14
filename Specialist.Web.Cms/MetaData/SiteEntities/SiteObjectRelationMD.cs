using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class SiteObjectRelationMD : CmsMetaData<SiteObjectRelation>
    {
        public override void Init()
        {
            For(x => x.Object_ID);
            For(x => x.ObjectType);
            For(x => x.RelationObject_ID);
            For(x => x.RelationObjectType);
        }
    }
}