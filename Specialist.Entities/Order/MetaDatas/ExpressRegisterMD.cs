using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Common.ViewModel;
using Specialist.Web.ViewModel.Orders;

namespace Specialist.Web.Root.Orders.MetaDatas {
	public class ExpressRegisterMD : BaseMetaData<ExpressRegisterVM> {
		public override void Init() {
			For(x => x.LastName).Display("Фамилия");
			For(x => x.FirstName).Display("Имя");
			For(x => x.SecondName).Display("Отчество");
			For(x => x.Phone).Display("Телефон");
			For(x => x.Email).Display("E-mail");
			For(x => x.PersonalData).Display("<a target='_blank' href='/soglasie_pers_dannye'>Согласие на обработку персональных данных</a>").Example("текст");
		}
	}
}