using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Const;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using System.Linq;
using System.Net.NetworkInformation;
using SimpleUtils;
using SimpleUtils.Extension;

namespace Specialist.Entities.Context
{
    public partial class Order
    {
        public bool IsEmpty
        {
            get
            {
                return OrderDetails.IsEmpty()
                    && OrderExams.IsEmpty();
            }
        }

		public string EduDocTypeTC { get; set; }

		public bool IsSig { get; set; }

		public string AdmitadId { get; set; }

	    private List<Dictionary<string, object>> admitadOrders = null;

	    public List<Dictionary<string, object>> GetAdmitad() {
		    if (admitadOrders != null) {
			    return admitadOrders;
		    }
			admitadOrders = new List<Dictionary<string, object>>();
		    if (AdmitadId.IsEmpty()) {
			    return admitadOrders;
		    }
			var courses = GetCourseOrderDetails();
		    var positionCount = courses.Count;
		    var i = 0;
			foreach (var orderDetail in courses) {
				i++;
				var x = new Dictionary<string, object> {
					{"uid", AdmitadId},
					{"tariff_code", User.IsStudent ? 1 : 2},
					{"order_id", CommonOrderId},
					{"position_id", i },
					{"currency_code", "RUB"},
					{"position_count", positionCount},
					{"price", (int)orderDetail.PriceWithDiscount },
					{"quantity", orderDetail.Count },
					{"product_id", orderDetail.Course_TC },
					{"payment_type", "sale"},
				};
				admitadOrders.Add(x);
			}
		    return admitadOrders;
	    }


	    public string CommonOrderId {
		    get { return (IsSig ? OrderCommonConst.prefixSpec : OrderCommonConst.prefixWeb) + OrderID; }
	    }

	    public static Tuple<bool, decimal> GetIdFromCommon(string commonId) {
		    var web = commonId.StartsWith(OrderCommonConst.prefixWeb);
		    var prefix = web ? OrderCommonConst.prefixWeb : OrderCommonConst.prefixSpec;
		    return Tuple.Create(web, decimal.Parse(commonId.Remove(prefix)));
	    }
	    public bool OnlyExam {
		    get { return !OrderDetails.Any() && OrderExams.Any(); }
	    }
	    public bool OnlyTestCert {
		    get { return OrderDetails.All(x => x.IsTestCert); }
	    }

	    public bool HasCourses {
		    get { return OrderDetails.Any(od => !od.IsTestCert); }
	    }

	    public string OurOrgOrDefault {
		    get {
			    if (OurOrg_TC == null) {
				    return OurOrgs.Spec;
			    }
			    if (IsSig) {
				    return OurOrg_TC;
			    }
			    return OurOrg_TC;
		    }
	    }

	    public bool HasComplex(HashSet<string> orderSpec) {
		    return OrderDetails.Any(od => {
			    return orderSpec.Contains(
				    od.Group.GetOrDefault(x => x.Complex_TC) ?? string.Empty);
		    });
	    }

	    public bool WithoutComplex {
		    get {
			    return OrderDetails.All(od => od.Group.GetOrDefault(x => x.Complex_TC) == null);
		    }
	    }

    	public string InvoiceNumber {
    		get {
    			var oldNewOrderIdStart = 656618;
    			var newOrderIdStart = 790672;
    			if (OrderID <= oldNewOrderIdStart) {
    				newOrderIdStart = 0;
    			}
    			if (OrderID > oldNewOrderIdStart && OrderID < newOrderIdStart) {
    				newOrderIdStart = oldNewOrderIdStart;
    			}
    			var postfix = OurOrgs.OrgOrderPostfix.GetValueOrDefault(OurOrgOrDefault) ?? "К";
				return "и" + (OrderID - newOrderIdStart + 1)+ postfix;
    		}
    	}



    	public bool HasPayment {
    		get { return !PaymentType_TC.IsEmpty(); }
    	}

    	public bool PaymentTypeIsSet {
    		get { return !PaymentType_TC.IsEmpty() && PaymentType_TC != PaymentTypes.NoPayment; }
    	}

        public bool IsComplete { get; set; }

        public string FullName { get; set; }

        public bool IsOrganization { get
        {
            return CustomerType == OrderCustomerType.Organization;
        } }

		public bool IsSigPaid { get; set; }

	    private string _descriptin;

	    public string FullDescription {
		    get { return GetDescription(true); }
	    }

        public string Description {
        	get {
        		return GetDescription(false);
        	}

	        set { _descriptin = value; }
        }

	    public string GetDescription(bool isFull) {
		    if (!_descriptin.IsEmpty())
			    return _descriptin;
		    var result = string.Empty;
		    var courses = OrderDetails.Where(x => !x.IsTestCert && !x.IsDopUsl);
		    if (courses.Any())
			    result += "Курсы: " +
				    courses.Select(od =>
					    od.Description).JoinWith(",") + ".";
		    var dopUsls = OrderDetails.Where(x => x.IsDopUsl);
		    if (dopUsls.Any())
			    result += "Спецзаказ: " +
				    dopUsls.Select(od =>
					    od.Description).JoinWith(",") + ".";
		    var testCerts = OrderDetails.Where(x => x.IsTestCert);
		    if (testCerts.Any())
			    result += "Серт. тест.: " +
				    testCerts.Select(od =>
					    od.Description).JoinWith(",") + ".";
		    if (OrderExams.Count > 0)
			    result += OrderExams.Select(oe => oe.Exam.Exam_TC + " " + oe.Exam.ExamName).JoinWith(",") + ".";
		    return result;
	    }

	    public string SocialUrl {
		    get { return OrderDetails.FirstOrDefault(x => x.SocialUrl != null).GetOrDefault(x => x.SocialUrl); }
	    }

	    public List<OrderDetail> GetCourseOrderDetails() {
			return OrderDetails.Where(x => !x.IsTestCert).ToList();
		}

	    public bool HasSchool {
		    get {
			    var orderDetails = GetCourseOrderDetails();
			    return orderDetails.Any(od => od.Course.IsSchool);
		    }
	    }

	    public bool HasEnglish {
		    get {
			    var orderDetails = GetCourseOrderDetails();
			    return orderDetails.Any(od => od.Course.IsEnglish);
		    }
	    }

    	public bool NotOnlyWebinar
        {
            get
            {
                return OrderDetails.Any(od => !od.IsWebinar) || OrderExams.Any() ;
            }
        }

    	public bool HasWebinar
        {
            get
            {
                return OrderDetails.Any(od => od.IsWebinar);
            }
        }

    	public bool GetHasWebinar() {
    		return OrderDetails.Any(od => od.IsWebinar);
    	}

    	public bool NotOnlyDistance
        {
            get
            {
                return OrderDetails.Any(od => !PriceTypes.IsDistanceOrWebinar(
                    od.PriceType_TC))
                    || OrderExams.Any() ;
            }
        }

		

        public bool Exported
        {
            get
            {
                return OrderDetails.Any(od => od.StudentInGroup_ID.HasValue)
                    || OrderExams.Any(oe => oe.StudentInGroup_ID.HasValue);
            }
        }

        public decimal TotalPrice
        {
            get
            {
                var result = OrderDetails.Sum(od => (od.Price +
					od.OrderExtras.Sum(oe => oe.Price)) * od.Count) +
                    OrderExams.Sum(oe => oe.Price);
                
                return result;
            }
        }

    	private decimal? _totalPriceWithDescount;

        public decimal TotalPriceWithDescount
        {
            get
            {
				if(_totalPriceWithDescount.HasValue)
					return _totalPriceWithDescount.Value;
                var result = OrderDetails.Sum(od => (od.PriceWithDiscount
					+ od.OrderExtras.Sum(oe => oe.Price)) * od.Count) +
                    OrderExams.Sum(oe => oe.Price);
              
                return result;
            }set { _totalPriceWithDescount = value; }
        }

        public decimal TotalPriceWithDescountForSber {
            get {
                decimal percent = 0;
                if (OrderDetails.Where(od => od.Group != null)
                   .Any(od => od.Group.BranchOffice
                       .GetOrDefault(bo => bo.TrueCity_TC) != Cities.Moscow))
                    percent = OrderCommonConst.SberReceiptPercent;
                if (percent == 0 && OrderDetails.Where(od => od.City_TC != null)
                    .Any(od => od.City_TC != Cities.Moscow))
                    percent = OrderCommonConst.SberReceiptPercent;

               

                return TotalPriceWithDescount * (decimal.One - percent);
            }
        }


        partial void OnCreated()
        {
            this.UpdateDate = DateTime.Now;
        }

        private EntityRef<User> _User = default(EntityRef<User>);
        [Association(Storage = "_User", ThisKey = "UserID",
            OtherKey = "UserID", IsForeignKey = true)]
        public User User
        {
            get { return _User.Entity; }
            set { _User.Entity = value; }
        }

   /*     private EntityRef<PaymentType> _PaymentType = default(EntityRef<PaymentType>);
        [Association(Storage = "_PaymentType", ThisKey = "PaymentType_TC",
            OtherKey = "PaymentType_TC", IsForeignKey = true)]
        public PaymentType PaymentType
        {
            get { return _PaymentType.Entity; }
            set { _PaymentType.Entity = value; }
        }
*/
        private EntityRef<Company> _Company = default(EntityRef<Company>);
        [Association(Storage = "_Company", ThisKey = "CompanyID",
            OtherKey = "CompanyID", IsForeignKey = true)]
        public Company Company
        {
            get { return _Company.Entity; }
            set { _Company.Entity = value; }
        }
    }
}
