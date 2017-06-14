using System;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using System.Linq;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Lms;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Order;
using Specialist.Services.Utils;
using Specialist.Web.Util;

namespace Specialist.Web.Root.Common.Services {
	public class PiStudentEmailService : Repository2<PiStudentEmail> {
		public PiStudentEmailService(IContextProvider contextProvider) : base(contextProvider) {}

		[Dependency]
		public IRepository2<PiStudent> StudentService {
			get;
			set;
		}

		public string SaveEmail(string name, string contact) {
			var names = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (names.Length != 3) {
				return "Введите ваше ФИО";
			}
			var student = new PiStudent {
				LastName = names[0].Trim(), 
				FirstName = names[1].Trim(),
				MiddleName = names[2].Trim(),
				IsSiteSubscriber = true,
				WebKeyword = Guid.NewGuid().ToString("N").Substring(0,10),
				WebLogin = Guid.NewGuid().ToString("N").Substring(0,10),
				Sex = Sex.M,
				Terrain_ID = Cities.Terrains.Moscow
				
			};
			EntityUtils.TitleNames(student);
			if (contact.Contains("@")) {
				var email = contact.Trim();
				var studentEmail = this.FirstOrDefault(x => x.Email == email);
				if (studentEmail != null) {
					return null;
				}
				studentEmail = CreateStudentEmail(email);
				student.PiStudentEmails.Add(studentEmail);
			}
			else {
				return "Введите ваш Емейл";
			}

			StudentService.EnableTracking();
			StudentService.InsertAndSubmit(student);
			return null;
		}

    	public static PiStudentEmail CreateStudentEmail(string email) {
    		return new PiStudentEmail {
    			Email = email,
    			InputDate = DateTime.Now,
    			Employee_TC = Employees.Specweb,
    			LastChangeDate = DateTime.Now,
    			LastChanger_TC = Employees.Specweb,
				IsSiteSubscriber = true
    		};
    	}
	}
}