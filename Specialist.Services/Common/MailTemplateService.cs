using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;
using Specialist.Web.Common.Utils;

namespace Specialist.Services.Common
{
    public class MailTemplateService : Repository<MailTemplate>
    {
        public MailTemplateService() : base(new ContextProvider()) {}


        [Dependency]
        public IRepository2<HtmlBlock> HtmlBlockService { get; set; }

        public Dictionary<string, MailTemplate> List() {
	        return MethodBase.GetCurrentMethod().CacheDay(() => {
		        var template = HtmlBlockService.GetValues(HtmlBlocks.MailTemplate, x => x.Description);
		        var all = GetAll().ToList();
		        foreach (var mailTemplate in all) {
			        mailTemplate.Description = template.Replace("[Text]", mailTemplate.Description);
		        }
		        return all.ToDictionary(x => x.SysName, x => x);
	        });
        }

	    public MailTemplate GetTemplate(string sysName, string userName) {
			var x = List()[sysName];
			return new MailTemplate {
				Name = x.Name,
				SysName = x.SysName,
				Description = x.Description.Replace("[UserName]", userName)
			} ;
		}

    }
}