using System.Web;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context.ViewModel;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Order;


namespace Specialist.Services.ViewModel
{
    public class ShoppingCartVMService 
    {

        [Dependency]
        public IOrderService OrderService { get; set; }

	    private const string StateKey = "CartStateTextKey";
	    private const string EduDocTypeKey = "EduDocTypeKey";

	    public string SessionState {
		    get { return HttpContext.Current.Session[StateKey].NotNullString(); }
		    set { HttpContext.Current.Session[StateKey] = value; }
	    }
	    public string EduDocType {
		    get { return HttpContext.Current.Session[EduDocTypeKey].NotNullString(); }
		    set { HttpContext.Current.Session[EduDocTypeKey] = value; }
	    }
	    public void Clear() {
		    SessionState = null;
	    }

        public string GetCartState() {
	        return SessionState ??
		        (SessionState = new ShoppingCartStateVM {Order = OrderService.GetCurrentOrder()}.ItemsText);
        }
    }
}
