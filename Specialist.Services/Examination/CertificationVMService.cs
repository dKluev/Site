using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Common.Extension;
using System.Linq;
using Specialist.Services.Catalog.Extension;
using SimpleUtils.Extension;

namespace Specialist.Services
{
    public class CertificationVMService: ICertificationcVMService
    {
        [Dependency]
        public ICertificationService CertificationService { get; set; }

        [Dependency]
        public ITrackService TrackService { get; set; }

		[Dependency]
		public IRepository<MarketingAction> MarketingActionService { get; set; }

		[Dependency]
        public IRepository2<EmployeeCertification> EmployeeCertificationService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        public CertificationVM GetByUrlName(string urlName)
        {
            var certification = CertificationService.GetAll().ByUrlName(urlName);
			if(certification.IsNull())
				return null;
            var tracks = certification.Exams.SelectMany(e => e.Courses)
                .Where(c => c.IsTrack.GetValueOrDefault()).Distinct().ToList();

			
			var trainers = EmployeeCertificationService.GetAll(
					x => x.Certification_ID == certification.Certification_ID 
						&& x.Certification.IsActive)
					.Select(x => x.Employee).Where(x => x.SiteVisible).Take(20).ToList();


	        var children = new List<Certification>();
            if (!certification.Exams.Any())
                children = CertificationService.GetAll().IsActive()
                    .Where(c => c.UrlName.StartsWith(certification.UrlName)
                    && c.UrlName != certification.UrlName).OrderBy(x => x.SortOrder)
                    .ToList();

            var groups = certification.Exams.Any() 
				? GetCertificationGroups(_.List(certification.Certification_ID)) 
				: GetCertificationGroups(children.Select(x => x.Certification_ID).ToList());

            var model = new CertificationVM
            {
                Certification = certification,
				Children = children,
                Tracks = TrackService.GetTrackDiscounts(tracks).ToList(),
                Groups = groups,
				Trainers = trainers
            };
			if (model.IsMicrosoft) {
				model.Actions = MarketingActionService.GetAll(x => x.Type == MarketingActionType.Microsoft
					&& !x.IsSecret && x.IsActive).ToList();
			}

            return model;
        }

	    private List<Group> GetCertificationGroups(List<decimal> certifictionIds) {
			if(!certifictionIds.Any())
				return new List<Group>();
		    var courseTCs = CertificationService.GetAll(x =>
			    certifictionIds.Contains(x.Certification_ID))
			    .SelectMany(y => y.CertificationExams.SelectMany(z =>
				    z.Exam.ExamCourses.Select(x => x.Course_TC))).ToList().Distinct()
					.ToList();
		    return courseTCs.SelectMany(c => GroupService.GetGroupsForCourse(c))
				.OrderBy(x => x.DateBeg).Take(30).ToList();
	    }
    }
}