using Specialist.Entities.Context;
using Specialist.Entities.Passport;

namespace Specialist.Services.Interface.Passport
{
    public interface IAuthService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
        User CurrentUser { get; set; }
        void RefreshUser();
    	Student GetCurrentStudent();
    }
}