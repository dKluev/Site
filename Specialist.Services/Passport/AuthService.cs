using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education.Interface;
using Specialist.Services.Interface.Passport;
using SimpleUtils.Common.Extensions;
using Specialist.Services.Utils;

namespace Specialist.Services.Passport
{
    public class AuthService : IAuthService
    {
        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency]
        public IStudentService StudentService { get; set; }

        public void SignIn(string userName, bool createPersistentCookie)
        {
            var user = SetCurrentUserByName(userName);
	        var remember = createPersistentCookie && !user.IsTrainerRole;
            FormsAuthentication.SetAuthCookie(userName, remember);
            
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
            CurrentUser = null;
        }

        public void RefreshUser()
        {
            CurrentUser = null;
        }

    	public Student GetCurrentStudent() {
    		var user = CurrentUser;

			if(user != null && user.Student_ID.HasValue)
    			return StudentService.GetByPK(user.Student_ID.Value);
    		return null;
    	}

        public const string CurrentUserSessionKey = "CurrentUserSessionKey";
        public User CurrentUser
        {
            get
            {
                if(HttpContext.Current.Session == null) {
                    var identity = HttpContext.Current.User.Identity;
                    if (identity == null || !identity.IsAuthenticated)
                        return null;
                	return UserService.GetByEmail(identity.Name);
                }
                var user = HttpContext.Current.Session[CurrentUserSessionKey] as User;
                if (user == null)
                {

                    var identity = HttpContext.Current.User.Identity;
                    if (identity == null || !identity.IsAuthenticated)
                        return null;
                    user = SetCurrentUserByName(identity.Name);
                }
                if(user != null && !user.IsActive)
                {
                    SignOut();
                    user = null;
                }
                return user;
            }
            set
            {
                 HttpContext.Current.Session[CurrentUserSessionKey] = value;
            }
        }

        private User SetCurrentUserByName(string name) {
            var user = (User)HttpContext.Current.Session[CurrentUserSessionKey];
            if (user == null)
            {
                user = UserService.GetByEmail(name);
				if(user == null) {
					SignOut();
				}
                CurrentUser = user;

            }
            return user;
        }
    }
}