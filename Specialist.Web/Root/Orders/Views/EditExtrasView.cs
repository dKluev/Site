using System.Web.Mvc;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Order.Const;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Shop;
using Specialist.Web.Core.Logic;
using Specialist.Web.ViewModel.Orders;
using System.Linq;
using Specialist.Web.Common.Extension;
using Htmls = SimpleUtils.FluentHtml.Tags.Htmls;
using SimpleUtils.Extension;

namespace Specialist.Web.Root.Orders.Views {
	public class EditExtrasView: BaseView<ExtrasesVM> {

		public const string WebinarNote =
			"Учебные материалы, включая доступ к записи вебинара, предоставляются только для личного использования. Распространение в любой форме не допускается и влечет ответственность в установленном порядке";

		public decimal ForDisable = Extrases.ForeignDelivery;
		public override object Get() {
			var model = Model;
			return div[
				strong["Дополнительные услуги"],
				Form(Url.Action<EditCartController>(c => c
				.EditExtras(Model.OrderDetail.OrderDetailID)))[
				Hidden(model.For(x => x.OrderDetail.OrderDetailID),
					model.OrderDetail.OrderDetailID),
				model.Extrases.Select(e => l(
					p[Extrases.IsTravel(e.Extras_ID) ? null : CheckBox(model, e), 
					e.ExtrasName + " " +
					model.ExtrasPrices[e.Extras_ID].MoneyString() + " руб."],
					e.Extras_ID.If(id => Extrases.WebinarRecord == id, _ =>
					p.Style("width:300px;")[strong[WebinarNote]]))),
					Web.Common.Html.Htmls.Submit("ok")]].ToString();
		}

		private TagInput CheckBox(ExtrasesVM model, Extras e) {
			return input.Type("checkbox")
				.Name(model.For(x => x.SelectedExtrases)).SetChecked(
					model.SelectedExtrases.Contains(e.Extras_ID))
				.Value(e.Extras_ID);
		}

	}
}