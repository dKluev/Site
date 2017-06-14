using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Newtonsoft.Json;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;

namespace Specialist.Entities.Context
{
    public partial class OrderDetail
    {


	    public decimal CalcGroupId {
		    get {
			    if (Group_ID.HasValue) {
				    return Group_ID.Value;
			    }
			    if (IsTestCert) {
				    return Groups.TestCert;
			    }
                return Groups.NotChoiceGroupID;
		    }
	    }

    	public bool IsTestCert {
    		get { return UserTestId.HasValue || 
				((Course_TC != null) && (Course_TC.StartsWith(CourseTC.Sert) || Course_TC == CourseTC.Srt)); }
    	}

    	public bool IsDopUsl {
    		get { return PriceType_TC == PriceTypes.DopUsl; }
    	}

	    public bool IsCourseOrder {
		    get { return !IsDopUsl && !IsTestCert; }
	    }

    	public bool IsWebinar {
    		get { return  PriceTypes.Webinars.Contains(PriceType_TC); }
    	}

	    public bool IsOrg {
		    get {
				if(PriceType_TC == null)
					return false;
			    return PriceType_TC.Contains("ОР");
		    }
	    }
    	public bool IsTrack {
    		get { return Track_TC != null; }
    	}
    	public decimal GetAllDiscountsInPercent()
        {
			if(MoneyDiscount > 0)
	            return ((Price - PriceWithDiscount) / Price) * 100;
        	return PercentDiscount.GetValueOrDefault();
        }

	    public bool CheckHasDiscount(int discountId) {
		    return StringUtils.IntListSplit(Notes).Contains(discountId);
	    } 

	    public void ClearDiscount() {
		    Notes = null;
            PercentDiscount = null;
            MoneyDiscount = null;
	    }

    	private OrderDetailParams _orderDetailParams = null;

    	public OrderDetailParams Params {
    		get {
    			if(_orderDetailParams == null) {
					if(JsonParams.IsEmpty())
						_orderDetailParams = new OrderDetailParams();
					else {
    				_orderDetailParams = JsonConvert.DeserializeObject<OrderDetailParams>(this.JsonParams);
					}
    				
    			}
				return _orderDetailParams;
    		}
    		set { _orderDetailParams = value; }
    	}

		public void UpdateXmlParams() {
			JsonParams = JsonConvert.SerializeObject(Params);
		}

	/*	public void SetTestNote(int type, int lang) {
			Notes = TestCertType.GetName(type) + " " + TestCertLang.GetName(lang);
		}*/
        public string Description
        {
            get {
            	var desc = string.Empty;
	            if (IsDopUsl) {
		            return Course.WebName;
	            }
				if(IsTestCert) {
					desc = UserTest.Test.Name + " " 
						+ TestCertLang.GetName(Params.Lang) + " " + TestCertType.GetName(Params.Type);
				}else if (Course_TC.StartsWith(CourseTC.Podar)) {
					desc = "Оплата спецзаказа за обучение";
				} else {
					desc = Course.WebName + " " + Group.GetOrDefault(x => x.Description)
						+ " (" + PriceType_TC + ") ";
				}
                return desc 
					+ (OrderExtras.Any() 
						? "Доп: " + OrderExtras.Select(oe => oe.Extras.ExtrasName)
					.JoinWith(", ") + "." : string.Empty) + " ";
            }
        }

        public decimal PriceWithDiscount { get {
        	var newPrice = (Price * ((decimal)100 - PercentDiscount.GetValueOrDefault()) / 100 -
        		MoneyDiscount.GetValueOrDefault());
	        if (newPrice <= 0) {
		        return 50;
	        }
			if(PercentDiscount > 0 || MoneyDiscount > 0) {
				if(Order.IsOrganization)
					return (int)newPrice;
	        	return FloorToFifty(newPrice);
			}
        	return newPrice;
        }
        }

    	public static decimal FloorToFifty(decimal newPrice) {
    		var floorPrice = Math.Floor(newPrice/100)*100;
    		var floorDifference = newPrice - floorPrice;
    		if(floorDifference == 50)
    			return newPrice;
    		if(floorDifference > 50)
    			return floorPrice + 50;
    		return floorPrice;
    	}

    	public bool HasDiscount {
            get {
                return PercentDiscount > 0 || MoneyDiscount > 0;
            }
        }

        private EntityRef<PriceType> _PriceType = default(EntityRef<PriceType>);
        [Association(Storage = "_PriceType", ThisKey = "PriceType_TC", 
            OtherKey = "PriceType_TC", IsForeignKey = true)]
        public PriceType PriceType
        {
            get
            {
                return _PriceType.Entity;
            }
        }

        private EntityRef<Course> _Course = default(EntityRef<Course>);
        [Association(Storage = "_Course", ThisKey = "Course_TC",
            OtherKey = "Course_TC", IsForeignKey = true)]
        public Course Course 
        {
            get { return _Course.Entity; }
            set { _Course.Entity = value; }
        }

        private EntityRef<Course> _Track = default(EntityRef<Course>);
        [Association(Storage = "_Track", ThisKey = "Track_TC",
            OtherKey = "Course_TC", IsForeignKey = true)]
        public Course Track
        {
            get
            {
                return _Track.Entity;
            }
            set
            {
                _Track.Entity = value;
            }
        }

        private EntityRef<Group> _Group = default(EntityRef<Group>);
        [Association(Storage = "_Group", ThisKey = "Group_ID",
            OtherKey = "Group_ID", IsForeignKey = true)]
        public Group Group
        {
            get
            {
                return _Group.Entity;
            }
            set
            {
                _Group.Entity = value;
            }
        }
        private EntityRef<UserTest> _UserTest = default(EntityRef<UserTest>);
        [Association(Storage = "_UserTest", ThisKey = "UserTestId",
            OtherKey = "Id", IsForeignKey = true)]
        public UserTest UserTest
        {
            get
            {
                return _UserTest.Entity;
            }
            set
            {
                _UserTest.Entity = value;
            }
        }

        private EntityRef<City> _City = default(EntityRef<City>);
        [Association(Storage = "_City", ThisKey = "City_TC",
            OtherKey = "City_TC", IsForeignKey = true)]
        public City City
        {
            get
            {
                return _City.Entity;
            }
            set
            {
                _City.Entity = value;
            }
        }

        private EntityRef<StudentInGroup> _StudentInGroup = default(EntityRef<StudentInGroup>);
        [Association(Storage = "_StudentInGroup", ThisKey = "StudentInGroup_ID",
            OtherKey = "StudentInGroup_ID", IsForeignKey = true)]
        public StudentInGroup StudentInGroup
        {
            get
            {
                return _StudentInGroup.Entity;
            }
            set
            {
                _StudentInGroup.Entity = value;
            }
        }
    }
}