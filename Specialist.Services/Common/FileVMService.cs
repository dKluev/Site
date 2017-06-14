using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Passport;

namespace Specialist.Services.Common
{
    public class FileVMService : IFileVMService
    {
        [Dependency]
        public IAuthService AuthService { get; set; }

        [Dependency]
        public IRepository<UserFile> FileService { get; set; }

        public List<UserFile> GetUserFiles(string courseTC)
        {
            var user = AuthService.CurrentUser;
            if (user != null && user.InRole(Role.Trainer)) {
	            var userFiles = FileService.GetAll().Where(f => f.UserID == user.UserID);
	            if (courseTC != null) {
		            userFiles = userFiles.Where(x => !x.CourseFiles.Select(y => y.Course_TC).Contains(courseTC));
	            }
	            return userFiles
                .ToList();
            }
	        return new List<UserFile>();
        }
    }
}