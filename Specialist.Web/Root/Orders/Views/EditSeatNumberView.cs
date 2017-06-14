using System.Web.Mvc;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Shop;
using Specialist.Web.Core.Logic;
using Specialist.Web.ViewModel.Orders;
using Specialist.Web.Common.Extension;
using System.Linq;

namespace Specialist.Web.Root.Orders.Views {
	public class EditSeatNumberView:BaseView<EditSeatNumberVM> {
		public override object Get() {
			return div[Images.ClassRoom(Model.ClassRoomTC), 
				h4["Свободные места"],
				Form(Url.Action<EditCartController>(c => c
					.EditSeatNumber(null)))[
						Model.Available.Select(x => span[x, 
							InputRadio(Model.For(y => y.Current),x.ToString())
							.SetChecked(x == Model.Current)]),
						Hidden(Model.For(x => x.OrderDetailId),
							Model.OrderDetailId),
							Htmls.Submit("ok")
							] ].ToString();


		}
	}
}