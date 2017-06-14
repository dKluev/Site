using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Passport;
using User=Specialist.Entities.Passport.User;
using System.Linq;

namespace Specialist.Services.Passport
{
    public class UserService:Repository<User>, IUserService
    {

        public UserService(IContextProvider contextProvider) : base(contextProvider) {}

        public bool ValidateUser(string email, string password)
        {

            var user = GetByEmail(email);
            if(user == null)
                return false;

            return user.Password == password;
           
        }

        public void CreateUser(User user)
        {
            user.IsActive = true;
            this.InsertAndSubmit(user);
        }

    /*    public bool CreateUser(string email, string password)
        {
            var user = 
                new User
                    {
                        Email = email, 
                        Password = password,
                        FirstName = string.Empty,
                        LastName = string.Empty,
                        SecondName = string.Empty,
                        
                        IsActive = true
                    };

                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();

            return true;
            
            
        }*/

        public User GetByEmail(string email)
        {
            return GetAll().FirstOrDefault(u => u.Email == email);
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = GetByEmail(email);
            if(user == null)
                return false;
            if(user.Password != oldPassword)
                return false;

            user.Password = newPassword;

            SubmitChanges();
            return true;

        }

    }
}
