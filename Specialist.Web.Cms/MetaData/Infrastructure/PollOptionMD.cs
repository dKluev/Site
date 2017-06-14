using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class PollOptionMD: CmsMetaData<PollOption>
    {
        public override void Init()
        {
            this.Display("Вариант ответа");
            this.Deletable();
            For(x => x.Text).Display("Ответ");
            For(x => x.PollID).Display("Опрос");
            For(x => x.Url).Display("Ссылка");
            For(x => x.Message).Display("Сообщение").UIHint(Controls.TextArea);
			this.TryAddStandartProperties();

        }
    }
}