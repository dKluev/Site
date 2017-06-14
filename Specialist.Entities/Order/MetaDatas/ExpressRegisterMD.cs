using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Common.ViewModel;
using Specialist.Web.ViewModel.Orders;

namespace Specialist.Web.Root.Orders.MetaDatas {
	public class ExpressRegisterMD : BaseMetaData<ExpressRegisterVM> {
		public override void Init() {
			For(x => x.LastName).Display("�������");
			For(x => x.FirstName).Display("���");
			For(x => x.SecondName).Display("��������");
			For(x => x.Phone).Display("�������");
			For(x => x.Email).Display("E-mail");
			For(x => x.PersonalData).Display("<a target='_blank' href='/soglasie_pers_dannye'>�������� �� ��������� ������������ ������</a>").Example("�����");
		}
	}
}