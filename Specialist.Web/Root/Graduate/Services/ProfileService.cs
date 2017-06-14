using System;
using System.Collections.Generic;
using System.Data.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Education.ViewModel;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Education.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using System.Linq;
using SimpleUtils.Extension;
using System.Linq.Dynamic;
using System.Web.Mvc;
using SimpleUtils;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Services.Common.Extension;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Const;
using Specialist.Web.Root.Graduate.Services;
using Specialist.Web.Root.Profile.Logic;
using Htmls = Specialist.Web.Common.Html.Htmls;

namespace Specialist.Services.Profile
{
    public class ProfileService
    {
        [Dependency]
        public IAuthService AuthService { get; set; }

		[Dependency]
		public IRepository2<Video> VideoService { get; set; }


        [Dependency]
        public IRepository<UserExamQuestionnaire> UserExamQuestionnaireService { get; set; }

        [Dependency]
        public IRepository2<Response> ResponseService { get; set; }

        [Dependency]
        public IRepository<UserContactType> UserContractTypeService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        [Dependency]
        public StudentRatingService StudentRatingService { get; set; }

        [Dependency]
        public IRepository2<UserTest> UserTestService { get; set; }

        [Dependency]
        public IRepository2<Company> CompanyService { get; set; }

        [Dependency]
        public IRepository2<Group> GroupService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency]
        public IDictionariesService DictionariesService { get; set; }

        [Dependency]
        public IStudentService StudentService { get; set; }

        [Dependency]
        public IEmployeeService EmployeeService { get; set; }

        [Dependency]
        public IRepository<RawQuestionnaire> RawQuestionaireService { get; set; }

        [Dependency]
        public IRepository<SuccessStory> SuccessStoryService { get; set; }

        [Dependency]
        public IRepository<Competition> CompetitionService { get; set; }

        [Dependency]
        public IRepository2<StudentNote> StudentNoteService { get; set; }

		  [Dependency]
        public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

        [Dependency]
        public IRepository2<StudentCalc> StudentCalcService { get; set; }


        public UserExamQuestionnaire GetExamQuestionnaire()
        {
            var model = AuthService.CurrentUser.ExamQuestionnaire;
            return model ?? new UserExamQuestionnaire();
        }

       /* private void UpdateModel(UserExamQuestionnaire model,
            UserExamQuestionnaire newModel)
        {
            model.Update(newModel, 
                x => x.Country,
                x => x.City,
                x => x.FirstName,
                x => x.Flat,
                x => x.House,
                x => x.LastName,
                x => x.MiddleInitial,
                x => x.PhoneNumber,
                x => x.PostalCode,
                x => x.Street,
                x => x.SylvanID,
                x => x.City);
        }*/

        public void Update(UserExamQuestionnaire newModel)
        {
            var user = AuthService.CurrentUser;
            var model = user.ExamQuestionnaire;
            newModel.UserID = user.UserID;
            if (model != null)
                newModel.UserExamQuestionnaireID =
                    model.UserExamQuestionnaireID;
            UserExamQuestionnaireService.UpdateOrInsert(newModel);
            AuthService.CurrentUser = null;
        }

        public bool ChangeStatus(ChangeStatusVM model)
        {
            var student = StudentService.GetAll().FirstOrDefault(s =>
                s.WebLogin == model.WebLogin && s.WebKeyword == model.WebKeyword);
            if(student == null)
                return false;

            var user = UserService.GetByPK(AuthService.CurrentUser.UserID);
			if(user.IsCompany)
				return false;
            user.Student_ID = student.Student_ID;
            UserService.SubmitChanges();
            AuthService.RefreshUser();
            return true;
        }

        public ProfileVM GetProfile()
        {
            var user =  UserService
                .GetByPK(AuthService.CurrentUser.UserID);

	    	var manager  = OrderService.GetUserManagerTC(user);
            var existgroup = OrderService.ExistGroup(user);
	        var companyName = string.Empty;
	        if (user.EmployeeCompanyID.HasValue) {
		        companyName = CompanyService.GetValues(user.EmployeeCompanyID, x => x.CompanyName);
	        }
            var result =
                new ProfileVM(Htmls.IsNewProfile)
                {
                    User = user,
                    IsBest = IsBest(user),
                    IsExcelMaster = IsExcelMaster(user),
                    EmployeeCompanyName = companyName,
                    Manager = manager,
                    HasUnlimit = HasUnlimit(user),
                    Videos = VideoService.GetAll(x => x.IsActive).OrderByDescending(x => x.VideoID).Take(5).ToList(),
                    ExistGroup = existgroup
                };
            return result;
        }


		public BangesVM GetBadges(UrlHelper url) {
            var user =  UserService
                .GetByPK(AuthService.CurrentUser.UserID);
	        var hours = user.IsStudent ? StudentInGroupService.GetAll(x => x.Student_ID == user.Student_ID
											&& BerthTypes.AllPaidForCourses.Contains(x.BerthType_TC)
											&& x.Group.DateEnd <= DateTime.Today)
											.Sum(x => (decimal?)x.Group.Hours).GetValueOrDefault() : 0;
			var best2016Url = user.IsStudent && Students.Best2016.Contains(user.Student_ID.Value)
				? url.Graduate().Urls.Best2016(user.Student_ID.Value)
				: null;
			return new BangesVM {
					TopBadge = GetTopBadge(user),
					HasUnlimit = HasUnlimit(user),
					Best2016Url = best2016Url,
					LearningHours = hours,
			};
		}

	    public bool HasUnlimit(User user) {
		    if (!user.IsStudent) return false;
		    var group = GroupService.GetAll().Where(x => x.Course_TC == CourseTC.Unlimit
			    && x.StudentInGroups.Any(sig => sig.Student_ID == user.Student_ID 
					&& sig.BerthType_TC == BerthTypes.Paid))
			    .Select(g => new {g.DateBeg, g.DateEnd}).FirstOrDefault();
		    return group != null && group.DateBeg < DateTime.Now && DateTime.Now < group.DateEnd;
	    }

	    public int? GetTopBadge(User user) {
		    if (!user.IsStudent) return null;
		    var realTop = StudentRatingService.GetForStudent(user.Student_ID.Value, x => x.TopX);
		    var maxTop = 30;
		    if (realTop == 0 || realTop > maxTop) return null;
		    var tops = _.List(10, 20, maxTop);
		    return tops.First(x => x >= realTop);
	    }

	    private bool IsBest(User user) {
    		if (user.IsStudent)
    			return FilterBestGraduate(_.List(user.Student_ID.Value)).Any();
    		return false;
    	}

		private bool IsExcelMaster(User user) {
    		if (user.IsStudent)
    			return FilterExcelMaster(_.List(user.Student_ID.Value)).Any();
    		return false;
    	}

    	public PublicProfileVM GetPublic(int userID) {
            var user = UserService.GetByPK(userID);
			if(user == null)
				return null;
        	var currentUser = AuthService.CurrentUser;
        	var tests = new List<Test>();
			if(!user.HideCourses)
        		tests = UserTestService.GetAll(x => x.UserId == userID
        			&& UserTestStatus.PassStatuses.Contains(x.Status)).OrderByDescending(x => x.RunDate)
        			.Select(x => x.Test).Distinct().ToList();
        	var model =
        		new PublicProfileVM() {
        			User = user,
					IsExcelMaster = IsExcelMaster(user),
        			IsOwner = currentUser != null && currentUser.UserID == userID,
        			Socials = GetSocials(user.UserContacts)
        				.Where(x => !x.Contact.IsEmpty()).ToList(),
        			SuccessStory = SuccessStoryService.GetAll()
        				.FirstOrDefault(ss => ss.UserID == user.UserID),
					Tests = tests,
        			Competitions = CompetitionService.GetAll()
        				.Where(c => c.WinnerID == user.UserID).ToList(),
					IsBest = IsBest(user),
                };
            return model;
        }


        public EditProfileVM GetEditProfileVM()
        {
            var user = AuthService.CurrentUser;
            var result = 
                new EditProfileVM
                {
                    User = AuthService.CurrentUser,
                };
            SetDictionaries(result);

            if (result.User != null) {
                var contactsVM = result.Contacts;
                if (user.IsCompany)
                {
                    contactsVM.Phone = GetContact(user.Company.UserContacts,
                        ContactTypes.Phone);
                    contactsVM.Mobile =
                            GetContact(user.Company.UserContacts, ContactTypes.Mobile);
                    contactsVM.WorkPhone =
                            GetContact(user.Company.UserContacts, ContactTypes.WorkPhone);
                    result.UserAddress = user.Company.UserAddresses.FirstOrDefault();
                }
                else
                {
                    result.UserAddress = user.UserAddresses.FirstOrDefault();
                  
                    contactsVM.Socials = GetSocials(user.UserContacts);

                    InitPhones(user, contactsVM);
                }
            }
            else
            {
                result.User = new User();
            }

            if (result.UserAddress == null) result.UserAddress = new UserAddress();
            if (result.Company == null) result.Company = new Company();
            result.ContactTypes = UserContractTypeService.GetAll()
                .Where(uct => ContactTypes.ForProfile().Contains(uct.ContactTypeID))
                .ToList();
                 
          
            return result;

        }

        public static void InitPhones(User user, ContactsVM contactsVM) {
            contactsVM.Phone = GetContact(user.UserContacts, ContactTypes.Phone);
            contactsVM.Mobile =
                GetContact(user.UserContacts, ContactTypes.Mobile);
            contactsVM.WorkPhone =
                GetContact(user.UserContacts, ContactTypes.WorkPhone);
        }

        public MyResponses GetResponses(int index)
        {
            var user = AuthService.CurrentUser;
            var responses = ResponseService.GetAll()
                .Where(rq => rq.RawQuestionnaire.Questionnaire.Student_ID == user.Student_ID 
					&& rq.IsActive)
                .ToPagedList(index, 5);
            return new MyResponses
                       {
                           Responses = responses,
                       };
        }

        public void SetDictionaries(EditProfileVM result)
        {
            result.Countries = DictionariesService.GetCountries();
        }

        private static string GetContact(EntitySet<UserContact> contacts, int contactType)
        {
            return contacts
                .FirstOrDefault(x => x.ContactTypeID == contactType)
                .GetOrDefault(x => x.Contact);
        }

        private List<UserContact> GetSocials(EntitySet<UserContact> contacts)
        {
            var allSocials = ContactTypes.GetAllSocialServices();
            var currentContacts = contacts
                .Where(x => allSocials.Contains(x.ContactTypeID));
            var forAdd = allSocials.Where(x =>
                currentContacts.All(y => y.ContactTypeID != x));
            if (forAdd.Any()) {
	            var userContacts = UserContractTypeService.GetByPK(forAdd.Cast<object>())
	                .ToList()
	                .Select(x => new UserContact { ContactTypeID = x.ContactTypeID, 
						UserContactType = x });
	            currentContacts = currentContacts.Union(userContacts).ToList();

            }
	        return currentContacts.OrderBy(x => allSocials.IndexOf(x.ContactTypeID)).ToList();

        }

        private void CreateProfile(EditProfileVM model)
        {
            var user = model.User;
            var contacts = model.Company != null
              ? user.Company.UserContacts
              : user.UserContacts;
            var userAddresses = model.Company != null
                ? user.Company.UserAddresses
                : user.UserAddresses;
            if(model.Contacts.Phone != null)
                contacts.Add( new UserContact(ContactTypes.Phone, model.Contacts.Phone));
            if (model.Contacts.Mobile != null)
                contacts.Add(
                    new UserContact(ContactTypes.Mobile, model.Contacts.Mobile));
            if (model.Contacts.WorkPhone != null)
                contacts.Add(
                    new UserContact(ContactTypes.WorkPhone, model.Contacts.WorkPhone));
            userAddresses.Add(model.UserAddress);
          /*  if(model.IsCompany)
            {*/
                
            /*    contacts.Add( new UserContact(ContactTypes.Fax, model.Contacts.Fax));
                contacts.Add( new UserContact(ContactTypes.LegalAddress, 
                    model.LegalAddress));*/
       /*     }
            else
            {*/
               
//            }

            UserService.CreateUser(user);
            AuthService.SignIn(user.Email, true);
            OrderService.UpdateSessionOrderUser();
        }


        public void Update(EditProfileVM model)
        {
            model.UserAddress.ContactTypeID = ContactTypes.Address;
                /*model.IsCompany ? ContactTypes.LegalAddress : */
            var currentUser = AuthService.CurrentUser;
            if(currentUser == null)
            {
                CreateProfile(model);
                return;
            }

            var user = UserService.GetByPK(currentUser.UserID);
            
            user.Update(model.User, 
                    x => x.LastName,
                    x => x.SecondName,
                    x => x.FirstName
                    );
			user.Update(model.User, 
				x => x.HideCourses, 
				x => x.EngFullName, 
				x => x.BirthDate,
				x => x.HideContacts);
            if(model.IsCompany)
            {
                if (user.Company == null)
                {
                    var company = model.Company;
                    model.Company.Users.Clear();
                    user.Company = company;
                }
                else
                    user.Company.UpdateByMeta(model.Company, x => x.CompanyID);
            }
            UpdateAddress(model, user);
            UpdateContacts(model, user);
            UserService.SubmitChanges();
            AuthService.RefreshUser();
        }

        private void UpdateContacts(EditProfileVM model, User user) {
            var contacts = model.IsCompany 
                ? user.Company.UserContacts 
                : user.UserContacts;
            var contactsVM = model.Contacts;
            UpdatePhones(contacts, contactsVM);
            if(!model.IsCompany)
            {
                if (contactsVM.Socials != null)
                    foreach (var contact in contactsVM.Socials) {
                        UpdateContact(contacts, contact.Contact, contact.ContactTypeID);
                    }
            }
          
        }

        public static void UpdatePhones(
            EntitySet<UserContact> contacts, ContactsVM contactsVM) {
            UpdateContact(contacts, contactsVM.Phone, ContactTypes.Phone);
            UpdateContact(contacts, contactsVM.Mobile, ContactTypes.Mobile);
            UpdateContact(contacts, contactsVM.WorkPhone, ContactTypes.WorkPhone);
        }

        private void UpdateAddress(EditProfileVM model, User user) {
            var userAddresses = model.IsCompany 
                ? user.Company.UserAddresses 
                : user.UserAddresses;
         /*   var contactType = model.IsCompany 
                ? ContactTypes.LegalAddress
                : ContactTypes.Address;*/
            UpdateAddress(userAddresses, model.UserAddress);
        }

        public void UpdateAddressAndPhones(UserAddress newUserAddress, ContactsVM contactsVM)
        {
            var user = UserService.GetByPK(AuthService.CurrentUser.UserID);
            var userAddresses = user.IsCompany
                ? user.Company.UserAddresses 
                : user.UserAddresses;
            UpdateAddress(userAddresses, newUserAddress);
			if(contactsVM != null)
	            UpdatePhones(user.UserContacts, contactsVM);
            UserService.SubmitChanges();
            AuthService.CurrentUser = null;
        }

        private void UpdateAddress(EntitySet<UserAddress> userAddresses, 
            UserAddress newUserAddress) {
            var address = userAddresses.FirstOrDefault();
            if (address == null)
                userAddresses.Add(newUserAddress);
            else {
				address.City = newUserAddress.City;
				address.Index = newUserAddress.Index;
				address.Address = newUserAddress.Address;
				if(newUserAddress.CountryID > 0)
					address.CountryID = newUserAddress.CountryID;
            	
            }
        }

        private static void UpdateContact(EntitySet<UserContact> contacts,
         string phoneNumber, int contactType)
        {
            
            var contact = contacts.FirstOrDefault(
                x => x.ContactTypeID == contactType);
            if (contact == null) {
                if (phoneNumber.IsEmpty())
                    return;
                    contacts.Add(new UserContact(contactType, phoneNumber));
            }
            else {
                if (phoneNumber.IsEmpty()) {
                    contacts.Remove(contact);
                    return;
                }
                contact.Contact = phoneNumber;
            }
                
        }

/*
        public List<Group> Calendar()
        {
            var user = UserService
                .GetByPK(AuthService.CurrentUser.UserID);
            if (user.Student == null)
                return new List<Group>();
            var groups = 
                from sig in user.Student.StudentInGroups
                select sig.Group;
            groups = groups.Union(user.UserInGroups.Select(uig => uig.Group)); 
            groups = groups.Where(g => g != null /*&& g.DateEnd > DateTime.Now#1#);

            return groups.OrderByDescending(g => g.DateBeg).ToList();
        }
*/

    	static readonly List<string> BestGraduateTypes = 
			_.List("2008-2009","2008","2009-2010", "2010", "2011");

    	private const string BestGraduateText = "Ëó÷øèé Âûïóñêíèê 20";
    	private const string BestWebinarText = "Ëó÷øèé âåáèíàðèñò";

		public List<decimal> FilterBestGraduate(List<decimal> studentIds ) {
			if(!studentIds.Any())
				return studentIds;
			var students = StudentNoteService.GetAll(x => 
				studentIds.Contains(x.Student_ID))
				.Where(x => x.Note.Contains(BestGraduateText) 
					|| x.Note.Contains(BestWebinarText))
				.Select(x => x.Student_ID).Distinct().ToList();
			return students;
		}

		public List<decimal> FilterExcelMaster(List<decimal> studentIds ) {
			if(!studentIds.Any())
				return studentIds;
			var tracks = _.List(CourseTC.ExcelA, CourseTC.ExcelB);
			var students = StudentInGroupService.GetAll(x => 
				studentIds.Contains(x.Student_ID) && x.BerthType_TC == BerthTypes.Paid
				&& tracks.Contains(x.Track_TC))
				.Select(x => x.Student_ID).Distinct().ToList();
			return students;
		}

		public Dictionary<decimal, string> FilterRealGraduate(List<decimal> studentIds) {
			if(!studentIds.Any())
				return new Dictionary<decimal, string>();
			return StudentCalcService.GetAll(x =>
				studentIds.Contains(x.Student_ID) && x.ÂestClabCard_ID.HasValue)
				.Select(x => new {
					x.StudentClabCard.ClabCardColor_TC,
					x.StudentClabCard.Student_ID
				}).ToDictionary(x => x.Student_ID, x => x.ClabCardColor_TC);
		} 

	/*	public List<string> GetBestGraduateTypes(decimal studentID) {
			var notes = StudentNoteService.GetAll(x => x.Student_ID == studentID)
				.Where(x => x.Note.Contains(BestGraduateText))
				.Select(x => x.Note).ToList();
			var result = notes.Select(GetBestGraduateType).Where(x => x != null).ToList();
			var webinar2011 = StudentNoteService.GetAll(x => x.Student_ID == studentID)
				.Where(x => x.Note.Contains(BestWebinarText))
				.Select(x => x.Note).ToList();
			if(webinar2011.Any())
				result.Add("webinar2011");
			return result;
		}

    	static string GetBestGraduateType(string note) {
			note = note.Replace('/', '-');
			return BestGraduateTypes.FirstOrDefault(x => note.Contains(x));
		}*/

     
    }
}
