using SimpleUtils.Util;
using System.Linq;

namespace Specialist.Entities.Context.ViewModel
{
    public class ShoppingCartStateVM
    {
        public Order Order { get; set; }

	    public const string ZeroText = "0 ������";
        public string ItemsText
        {
            get
            {
	            if (Order == null || Order.IsEmpty) {
		            return ZeroText;
	            }
                var count = 0;
                var item = "����";
                if(Order != null)
                {
                    count = Order.OrderDetails.Count(x => !x.IsTestCert);
                    if(count == 0)
                    {
                        if (Order.OrderExams.Count > 0)
                        {
                            count = Order.OrderExams.Count;
                            item = "�������";
                        }else {
                        	count = Order.OrderDetails.Count(x => x.IsTestCert);
							if(count > 0) {
								item = "����������";
							}
                        }
                    }
                }
                return count + " " + Linguistics.Plural(item, count);
            }
        }
    }
}