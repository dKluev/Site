using System.Linq;
using System.Text;
using System.Web.Mvc;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Core;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Html;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Cms.Controllers
{
	[CmsAuth(RoleList = Role.ContentManager)]
    public class UserEntityController: BaseController<User>
    {


		public ActionResult Subscribers() {
			var users = (
				from x in  Repository.GetAll(x => x.Subscribes != 0).ToList()
				let address = x.GetAddress()
				where x.HasFullAddress
				select 
				new {x.Student_ID, 
					x.LastName, 
					x.FirstName, 
					x.SecondName,
					address.Index,
					address.Address,
					address.City,
					CountryName = address.Country.GetOrDefault(y => y.CountryName),
					x.Subscribes,
				}).ToList();
			var table = H.table[
				H.Head("Код", "Фамилия", "Имя", "Отчество", 
				"ID (индекс)", "AD (адрес)" , "Каталог" ),
				users.Select(u => H.Row(
					u.Student_ID,
					u.LastName, 
					u.FirstName, 
					u.SecondName,
					u.Index,
					string.Join(" ", u.CountryName, u.City, u.Address),
					(SubscribeType)u.Subscribes)
				)];
			return File(new UTF8Encoding().GetBytes(table.ToString()), 
				"application/ms-excel", "subscribers.xls");
		}
    }
}