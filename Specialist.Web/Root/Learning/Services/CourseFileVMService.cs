using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Context;
using Specialist.Entities.Education;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils;

namespace Specialist.Services.Education {
	public class CourseFileVMService {
		 
        [Dependency]
        public IRepository2<CourseFile> CourseFileService { get; set; }

        [Dependency]
        public IRepository2<Course> CourseService { get; set; }

		public Dictionary<string, string> FtpUrls() {
			return MethodBase.GetCurrentMethod().CacheDay(() => 
				CourseService.GetAll(x => !x.WebinarEduURL.Equals(null))
				.Select(x => new {x.Course_TC, x.WebinarEduURL})
				.ToDictionary(x => x.Course_TC, x => x.WebinarEduURL));
		} 

		public List<CourseFileVM> GetSpecFiles(List<string> courseTCs) {
			return courseTCs.Select(x => new CourseFileVM("Учебные материалы по курсу", 
				FtpUrls().GetValueOrDefault(x),x,null, null)).Where(x => x.Url != null).ToList();
		}

		public List<CourseFileVM> GetFiles(List<Tuple<string, string>> courseTrainerTCs) {
			CourseFileService.LoadWith(c => c.Load(x => x.UserFile).And<UserFile>(x => x.User));
			var keys = courseTrainerTCs.Select(x => x.Item1 + "|" + x.Item2).ToList();
			var files = CourseFileService.GetAll(x => keys.Contains( 
				x.Course_TC + "|" + x.UserFile.User.Employee_TC)).ToList()
				.Select(x => new CourseFileVM(x.UserFile.Name, UserFiles.GetUserFileUrl(x.UserFile), x.Course_TC, x.UserFile.User.FullName, x.UserFile.User.Employee_TC));
			var specFiles = GetSpecFiles(courseTrainerTCs.Select(x => x.Item1).ToList());
			return files.Concat(specFiles).ToList();
		}

	}
}