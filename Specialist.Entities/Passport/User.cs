using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using SimpleUtils.Collections;
using SimpleUtils.FluentAttributes.Const;
using Microsoft.Linq.Translations;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentAttributes.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using SimpleUtils;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Utils.Logic;
using Specialist.Web.Const;

namespace Specialist.Entities.Passport
{
    public partial class User
    {
        [DisplayName("ФИО")]
        public string FullName { get { return FullNameExpression.Evaluate(this); } }

	    public string FirstSecondName {
		    get {
			    if (SecondName.IsEmpty()) {
				    return FirstName;
			    }
				return FirstName + " " + SecondName;
		    }
	    }

	    public bool IsRestricted {
		    get { return UserID == 1110552; }
	    }

	    [UIHint(Controls.Hidden)]
        public bool ForEdit { get; set; }

        private static readonly CompiledExpression<User, string> FullNameExpression = DefaultTranslationOf<User>
            .Property(e => e.FullName).Is(e => e.LastName + " " + e.FirstName + " " + e.SecondName);

	    public DateTime RegCouponEndDate {
		    get {
				
			    return CreateDate.AddDays(14).Date;
		    }
	    }

	    public string ShortFullName {
		    get { return FirstName == CommonTexts.EmptyFirstName ? Email : LastName + (LastName.IsEmpty() ? FirstName : " " + FirstName.FirstOrDefault() + "."); }
	    }

	    public bool CanHaveCoupon {
		    get {
			    return WorkBranch_ID.HasValue && (IsCompany || (BirthDate.HasValue && Metier_ID.HasValue));
		    }
	    }

	    public bool RegCouponIsValid {
		    get {
			    return CouponUtils.RegistrationIsActive && RegCouponEndDate >= DateTime.Today
						&& CreateDate > CouponUtils.RegStartDate && CanHaveCoupon;
		    }
	    }

	    public bool IsTestCorpManager {
		    get { return IsCompany && Companies.TestService.Contains(CompanyID.Value); }
	    }

		public Role GetAllRoles() {
			var role = (Role)Roles;
			if(IsStudent)
				role |= Role.Graduate;
			if(IsCompany)
				role |= Role.CorpManager;
			if(IsTestCorpManager)
				role |= Role.TestCorpManager;
			if(IsEmployee)
				role |= Role.Employee;
			return role;
		}

	    public bool IsTrainerRole {
		    get { return ((Role) Roles).HasFlag(Role.Trainer); }
	    }
		public bool InRole(Role role) {
				var allRoles = GetAllRoles();
			if((allRoles & Role.Admin) == Role.Admin)
				return true;
			return (role & allRoles) != Role.None;
		}





	    public bool IsFacebook
	    {
		    get { return !FbUserId.IsEmpty(); }
	    }

	    public bool HasFullAddress {
            get {

                var address = GetAddress();
            	if (address == null)
                    return false;
            	return address.HasFullAddress;
            }
        }

    	public UserAddress GetAddress() {
    		if(IsCompany)
    			return Company.UserAddresses.FirstOrDefault();
    		return UserAddresses.FirstOrDefault();
    	}

    	public bool IsStudent
        {
            get
            {
                return Student_ID != null;
            }
        }


        public bool IsEmployee
        {
            get
            {
                return Employee_TC != null;
            }
        }

        public bool IsSpecOrg {
            get {
                return Org_ID  > 0;
            }
        }

        public bool IsCompany {
            get {
                return CompanyID > 0;
            }
        }

        public string AddressDescription
        {
            get
            {
                var address = UserAddresses.FirstOrDefault();
                if(address == null)
                    return null;
                return address.FullAddress;
                
        
            }
        }

        public string MailDescription {
            get {
                var userContacts = UserContacts.Where(uc => !uc.Contact.IsEmpty())
                .ToDictionary(x => x.UserContactType.Name, x => x.Contact);
                if (IsCompany)
                {
                    userContacts.Add("Название компании", Company.CompanyName);
					userContacts.Add("ИНН", Company.INN);
					userContacts.Add("КПП", Company.KPP);
					userContacts.Add("Юридический адрес", Company.LegalAddress);
                    userContacts.Add("Адрес", 
						Company.UserAddresses.First().FullAddress);
                	foreach (var userContact in Company.UserContacts
                		.Where(uc => !uc.Contact.IsEmpty())) {
                		var value = userContacts.GetValueOrDefault(userContact.UserContactType.Name);
						if(value == null)
							userContacts.Add(userContact.UserContactType.Name, userContact.Contact);
						else
							userContacts[userContact.UserContactType.Name] += ", " +value;
                	}
                }
				else {
                	var userAddress = UserAddresses.FirstOrDefault();
                	if(userAddress != null)
	                    userContacts.Add("Адрес", 
							userAddress.FullAddress);
                }
            return DictionaryUtils.ToHtml(
                EntityUtils.ToStrings(this, x => x.FullName, x => x.Email))
                    + DictionaryUtils.ToHtml(userContacts);
            }
        }

        private EntityRef<Student> _Student = default(EntityRef<Student>);
        [System.Data.Linq.Mapping.Association(Storage = "_Student", ThisKey = "Student_ID",
            OtherKey = "Student_ID")]
        public Student Student
        {
            get { return _Student.Entity; }
            set { _Student.Entity = value; }
        }

        private EntityRef<Employee> _Employee = default(EntityRef<Employee>);
        [System.Data.Linq.Mapping.Association(Storage = "_Employee", ThisKey = "Employee_TC",
            OtherKey = "Employee_TC")]
        public Employee Employee {
            get { return _Employee.Entity; }
            set { _Employee.Entity = value; }
        }

        private EntityRef<SpecOrg> _SpecOrg = default(EntityRef<SpecOrg>);
        [System.Data.Linq.Mapping.Association(Storage = "_SpecOrg", ThisKey = "Org_ID",
            OtherKey = "Org_ID")]
        public SpecOrg SpecOrg {
            get { return _SpecOrg.Entity; }
            set { _SpecOrg.Entity = value; }
        }

        private EntitySet<UserCompetition> _UserCompetitions = new EntitySet<UserCompetition>();
        [System.Data.Linq.Mapping.Association(Storage = "_UserCompetitions", ThisKey = "UserID",
            OtherKey = "UserID")]
        public EntitySet<UserCompetition> UserCompetitions {
            get { return this._UserCompetitions; }
            set { this._UserCompetitions.Assign(value); }
        }
        
    }
}