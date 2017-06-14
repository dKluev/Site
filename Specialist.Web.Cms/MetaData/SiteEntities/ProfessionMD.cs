using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class ProfessionMD : CmsMetaData<Profession>
    {
        public override void Init()
        {
            this.Display("���������");
            this.DisplayByName();
            this.AddName();
        	this.ReadOnly();
        	/*   this.TryAddStandartProperties();
            For(x => x.Salary).Display("��������");
            For(x => x.ForMainPage).Display("�� �������");*/

        }
    }
}