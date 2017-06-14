using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog;
using Specialist.Entities.Center;
using Specialist.Entities.Context;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;

namespace Specialist.Services {
	public class ResponseService : Repository<Response>, IResponseService {
		public ResponseService(IContextProvider contextProvider) : base(contextProvider) {}

		public List<Response> GetRandomResponsesByCourse(string CourseTC, int rows) {
			var context = new SpecialistDataContext();
			return context.GetTable<Response>().IsActive()
				.Where(x => x.Type == RawQuestionnaireType.CourseComment).Where(r => r.Course_TC == CourseTC)
				.OrderBy(x => context.GetNewID()).Take(rows).ToList();
		}


		public List<Advice> GetRandomAdvices() {
			var context = new SpecialistDataContext();
			return context.GetTable<Advice>().IsActive()
				.OrderBy(x => context.GetNewID()).Take(CommonConst.AdviceCount).ToList();
		}

		public Response GetRandomForMainPage(List<string> courseTCs) {
			var context = new SpecialistDataContext();
			var response = 
				context.GetTable<Response>().IsActive()
				.Where(x => x.Rating == ResponseRating.ForMain 
					&& courseTCs.Contains(x.Course_TC))
				.OrderBy(x => context.GetNewID()).FirstOrDefault();
			if(response != null)
				return response;
			return context.GetTable<Response>().IsActive()
				.Where(x => x.Rating == ResponseRating.ForMain)
				.OrderBy(x => context.GetNewID()).FirstOrDefault();
		}

		public OrgResponse GetOrgRandomResponseForMainPage() {
			var context = new SpecialistDataContext();
			return context.GetTable<OrgResponse>().Where(x => x.ShortDescription != null)
				.OrderBy(x => context.GetNewID()).FirstOrDefault();

		}

		public List<Response> GetRandomForWebinar() {
			var context = new SpecialistDataContext();
			return context.GetTable<Response>().IsActive()
				.Where(x => x.IsWebinar)
				.OrderBy(x => context.GetNewID()).Take(15).ToList();
		}

		public IQueryable<Response> GetAllForCourse(string courseTC) {
			return GetAll().Where(x => x.Course_TC == courseTC)
				.IsActive()
				.Where(r => r.Type == RawQuestionnaireType.CourseComment)
				.OrderByDescending(x => x.UpdateDate);
		}

		public IQueryable<Response> GetAllForEmployee(string employeeTC) {
			return GetAll().IsActive()
				.Where(r => r.Employee_TC == employeeTC)
				.Where(r => r.Type == RawQuestionnaireType.Teacher);
		}

	}
}