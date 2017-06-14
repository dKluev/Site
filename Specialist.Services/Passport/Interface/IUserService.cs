using Specialist.Entities.Passport;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Interface.Passport
{
    public interface IUserService: IRepository<User>
    {
        bool ValidateUser(string email, string password);
        User GetByEmail(string email);
        bool ChangePassword(string email, string oldPassword, string newPassword);
        void CreateUser(User user);
    }
}