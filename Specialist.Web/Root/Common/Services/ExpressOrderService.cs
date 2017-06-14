using System;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using System.Linq;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.Order;
using Specialist.Services.Utils;

namespace Specialist.Web.Root.Common.Services {
	public class ExpressOrderService {

		[Dependency]
		public IRepository2<StudentEmail> StudentEmailService {
			get;
			set;
		}

		[Dependency]
		public IRepository2<StudentPhone> StudentPhoneService {
			get;
			set;
		}

		public decimal? CreateOrder(string name, string contact) {
			if (name.IsEmpty() || contact.IsEmpty()) {
				return null;
			}
			var names = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (!names.Any())
				return null;

			try {
				var studentId = SaveStudent(contact, names);
				return SpecialistExportService.SaveExpressOrder(studentId);

			}catch(Exception e) {
				Logger.Exception(e, "CreateOrder " + name + " " + contact);
				return null;
			}
			
		}

		private decimal SaveStudent(string contact, string[] names) {
			var firstName = string.Empty;
			var lastName = "Слушатель";
			if (names.Length == 1)
				firstName = names[0];
			else {
				lastName = names[0];
				firstName = names[1];
			}
			var student = new Student {
				LastName = lastName, 
				FirstName = firstName,
				WebKeyword = Guid.NewGuid().ToString("N").Substring(0,10),
				WebLogin = Guid.NewGuid().ToString("N").Substring(0,10),
				Sex = Sex.M,
				Terrain_ID = Cities.Terrains.Moscow
				
			};
			if (contact.Contains("@")) {
				var email = contact.Trim();
				var studentEmail = StudentEmailService.FirstOrDefault(x => x.Email == email);
				if (studentEmail != null) {
					return studentEmail.Student_ID;
				}
				studentEmail = SpecialistExportService.CreateStudentEmail(email);
				student.StudentEmails.Add(studentEmail);
			} else {
				var phone = Regex.Replace(contact, @"[+\s()-]", "");
				if (Regex.IsMatch(phone, @"\d+")) {
					var studentPhone = StudentPhoneService
						.FirstOrDefault(x => x.PhoneNumber == phone);
					if (studentPhone != null) {
						return studentPhone.Student_ID;
					}
					studentPhone = SpecialistExportService.CreateStudentPhone(null,
						phone);
					student.StudentPhones.Add(studentPhone);
				}
			}

			SpecialistExportService.SaveStudnet(student);
			return student.Student_ID;
		}
	}
}