using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using Specialist.Services.Education;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Interface.Center;

namespace Specialist.Services.ViewModel
{
    public class TrackVMService: ITrackVMService
    {
        [Dependency]
        public ICertificationService CertificationService { get; set; }

        [Dependency]
        public ITrackService TrackService { get; set; }

        [Dependency]
        public IRepository2<CourseCertificate> CourseCertificateService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public StudentInGroupService StudentInGroupService { get; set; }

        public TrackVM GetByUrlName(string urlName)
        {
            var track = TrackService.GetByUrlName(urlName);
			if(track == null)
				return null;
            var courseTC = track.Course_TC;
//            var certifications = CertificationService.
//                GetAllForCourse(courseTC);
            var sections = SiteObjectService.GetSingleRelation<Section>(track)
                .IsActive().ByWebOrder().ToList();


        	var prices = PriceService.GetAllPricesForCourse(courseTC, null);

	        var courseTCs = _.List(courseTC);
			courseTCs.AddRange(CourseService.GetActiveTrackCourses().GetValueOrDefault(track.Course_TC)
		        ?? new List<string>());
	        var certTypes = CourseCertificateService.GetAll(x => courseTCs.Contains(x.Course_TC))
		        .Select(x => new {x.Course_TC, x.CertType}).Distinct().ToList().Where(x =>
				x.Course_TC != courseTC || x.CertType.CertTypeName.Contains(CertTypes.InterName))
				.Select(x => x.CertType).ToList();
	        var trackDetailsVM =
		        new TrackVM {
			        Certifications = new List<Certification>(),
			        Course = track,
			        Prices = prices,
			        Sections = sections,
					CertTypeList = certTypes,
			        CompleteCourseCount = StudentInGroupService.CompleteCountForTracks()
						.GetValueOrDefault(track.ParentCourse_TC)
		        };
            return trackDetailsVM;
        }

       
       
    }
}