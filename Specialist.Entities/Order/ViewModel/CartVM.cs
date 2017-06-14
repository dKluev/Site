using System;
using System.Collections.Generic;
using SimpleUtils.Common;
using SimpleUtils.Common.Enum;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Const;
using System.Linq;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Passport;
using Tuple = System.Tuple;

namespace Specialist.Entities.Context
{
    public class CartVM: OrderSeparator, IViewModel
    {
        public CartVM(Order order) : base(order) {
        	WebinarRecords = new List<WebinarRecord>();
			WithWebinar = new List<string>();
			IsNearestGroupOrderDetails = new List<decimal>();
			NearestGroups = new List<Group>();
			ExtrasTexts = new List<ExtrasText>();
        }

		public class ExtrasText {

	    	public List<decimal> Extrases { get; set; }
			public List<string> Courses { get; set; }
			public string Text { get; set; }
			public bool Alert { get; set; }

			public ExtrasText(List<decimal> extrases, List<string> courses, string text, bool alert = false) {
				Alert = alert;
				Extrases = extrases;
				Courses = courses;
				Text = text;
			}
		}

	    public string BossFullName { get; set; }
	    public string AccounterFullName { get; set; }

		public List<Group> NearestGroups { get; set; } 
		public string FavTrainer { get; set; } 

		public Order LastCompleteOrder { get; set; } 

		public List<ExtrasText> ExtrasTexts { get; set; }

    	public List<decimal> IsNearestGroupOrderDetails { get; set; }

        public OrderSeparator InPlan  { get; set;}

    	public List<string> CourseTCHasExtrases { get; set; }



    	public User User { get; set; }

	    public OurOrg OurOrg { get; set; }


        public bool OnlyIsCompaty
        {
            get
            {
                return User != null && User.IsCompany;
            }
        }

        public bool HasBusiness {
            get {
                return
                    Order.OrderDetails.Any(
                        od => PriceTypes.IsBusiness(od.PriceType_TC));
            }
        }


    	public List<TrackDiscount> TrackDiscounts { get; set; }

    	public List<WebinarRecord> WebinarRecords { get; set; }

    	public List<string> WithWebinar { get; set; } 

        public bool IsEmpty
        {
            get { return InPlan.Order.IsEmpty && Order.IsEmpty; }
        }

	    public bool HasNsDiscount {
		    get { return Order.OrderDetails.Any(x => x.CheckHasDiscount(Discounts.RealSpecId)); }
	    }

		public List<OrderDetail> OrdersAndTrackFirstCourses {get {
			return CourseOrderDetails.Concat(Tracks.Select(x => x.OrderDetails.First()))
				.ToList();
		}} 

        public string Title {
            get { return EnumUtils.GetDisplayName(OrderStep.Cart); }
        }

	    public OurOrgBank Bank { get; set; }
    }
}
