using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Common;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Center;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Center;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Common;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Interface;
using System.Linq;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Interface.Center;
using Specialist.Services.Order;
using Specialist.Services.Order.Extension;
using Specialist.Services.Common.Extension;
using Specialist.Services.UnityInterception;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Html;
using Specialist.Web.Util;
using Htmls = Specialist.Web.Common.Html.Htmls;

namespace Specialist.Services
{
    public class CourseVMService : ICourseVMService
    {
        [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public ISectionService SectionService { get; set; }

        [Dependency]
        public IResponseService ResponseService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public ITrackService TrackService { get; set; }

        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        [Dependency]
        public ICertificationService CertificationService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public ISiteObjectRelationService SiteObjectRelationService { get; set; }

        [Dependency]
        public IRepository2<SuccessStory> SuccessStoryService { get; set; }

        [Dependency]
        public ExtrasService ExtrasService { get; set; }

        [Dependency]
        public IRepository<Product> ProductService { get; set; }

        [Dependency]
        public IRepository<Profession> ProfessionService { get; set; }

        [Dependency]
        public IRepository2<Test> TestService { get; set; }
        [Dependency]
        public IRepository2<CourseContent> CourseContentService { get; set; }
        [Dependency]
        public IRepository<Vendor> VendorService { get; set; }

        [Dependency]
        public IRepository<SiteTerm> SiteTermService { get; set; }

        [Dependency]
        public IRepository<CoursesChain> CourseChainService { get; set; }

        [Dependency]
        public ISectionVMService SectionVMService { get; set; }

        [Dependency]
        public SuperJobService SuperJobService { get; set; }

        [Dependency]
        public IRepository<MarketingAction> MarketingActionService { get; set; }

	    public CourseBaseVM.SecondCourseDiscount SecondCourse(string courseTC, CourseLink course) {
            
		    if (course == null) {
			    return null;
		    }
		    var price = PriceService.GetPriceByType(courseTC, PriceTypes.Main, null);
		    var secondPrice = PriceService.GetPriceByType(course.CourseTC, PriceTypes.Main, null);
		    if (!secondPrice.HasValue || !price.HasValue) {
			    return null;
		    }
		    var authTypeTC = CourseService.GetValues(course.CourseTC, x => x.AuthorizationType_TC);
		    var discountPercent = AuthorizationTypes.GetSecondCourseDiscount(authTypeTC);
		    var secondpriceWithDiscount = OrderDetail.FloorToFifty(secondPrice.Value*(1.0m - discountPercent/100.0m));
		    return new CourseVM.SecondCourseDiscount {
				SecondCourse = course,
			    Discount = (secondPrice.Value - secondpriceWithDiscount),
				SumWithDiscount = secondpriceWithDiscount + price.Value
		    };
	    }

        public CourseVM GetByUrlName(string urlName) {
            var cityTC = UserSettingsService.CityTC;
            var course = CourseService.GetByUrlName(urlName);
			if (course == null)
				return null;
         
            var courseTC = course.Course_TC;
       
            var nextCourseTCList = CourseService.GetNextCourseTCs(_.List(course.ParentCourse_TC));

    		var nextCourses = CourseService.GetCourseLinkList(nextCourseTCList).ToList()
    			.OrderBy(x => nextCourseTCList.IndexOf(x.CourseTC)).ToList();
	        var notSecondCourseDiscount = CourseService.NotSecondCourses();
	        var secondCourse = SecondCourse(courseTC, nextCourses.Where(x => !notSecondCourseDiscount.Contains(x.CourseTC)).FirstOrDefault());
	        var successStories = SuccessStoryService.GetAll(x => x.Course_TC == courseTC).Take(1)
		        .OrderByDescending(x => x.SuccessStoryID).ToList();
        	var responceCount = successStories.Any() ? 1 : 2;
        	var responseQuery = ResponseService.GetAllForCourse(courseTC);
        	var responses = responseQuery
        		.Where(x => x.Rating >= ResponseRating.Good)
                .GetRandom(responceCount).ToList();
			if(responses.Count < responceCount)
				responses.AddRange(responseQuery
					.Where(x => x.Rating == ResponseRating.Common)
						.GetRandom(responceCount - responses.Count) );
			if (!responses.Any()) {
				responses = ResponseService.GetAll(x => x.IsActive &&
					x.Type == RawQuestionnaireType.OrganizingComment)
					.Take(responceCount).ToList();
			}
        	var responseTotalCount = responseQuery.Count();
            var nearestGroups = GroupService.GetNearestGroups(course, cityTC);


            var trainers = nearestGroups.All
                .Select(x => x.Teacher).Where(e => e != null && e.SiteVisible)
				.Distinct(x => x.Employee_TC).ToList();
	        var morningDiscount = NearestGroupSet.HasMorningDiscount(nearestGroups.All);
            var product = SiteObjectService.GetSingleRelation<Product>(course)
				.IsActive().OrderByDescending(x => x.WebSortOrder).FirstOrDefault();
        	var prices = PriceService.GetAllPricesForCourse(course.Course_TC, null);
            var certifications = CertificationService.GetAllForCourse(courseTC);
        	var vacancies = SuperJobService.GetVacancies(
        		SiteObjectRelationService.GetRelation(
        			typeof(Course), _.List(courseTC), 
        			typeof(Profession))
        			.Select(sor => (int)sor.RelationObject_ID)
					.ToList(), cityTC);
        	var withWebinar = PriceService.CourseWithWebinar();
	        var discount30 = Discount30Courses();
        	var actions = GetActionOnCoursePages().Where(x => {
        		var courseTCList = EntityUtils.GetCourseTCs(x, withWebinar);
				return !courseTCList.Any() || courseTCList.Contains(courseTC) ||
					(x.MarketingAction_ID == 150 && discount30.Contains(courseTC));
        	}).OrderBy(x => x.WebSortOrder).ToList();
	        if (course.IsExpensive)
		        actions = actions.Where(x => !x.CourseTCList.IsEmpty()).ToList();
        	var preTestIds = course.CoursePrerequisites.Select(x => x
				.Test_ID.GetValueOrDefault())
        		.Where(x => x > 0).ToList();

        	var preTests = preTestIds.Any()
        		? TestService.GetAll(x =>
        			preTestIds.Contains(x.Id)).ToList()
        		: new List<Test>();

            var sections = SiteObjectService.GetSingleRelation<Section>(course)
                .IsActive().Where(x => !Sections.NotImportant.Contains(x.Section_ID)).ByWebOrder().ToList();

        	var tests = SiteObjectService.GetByRelationObject<Test>(course)
				.Where(x => x.Status == TestStatus.Active).ToList();
	        var visitedCourseTCs = UserSettingsService.VisitedCourses.Except(_.List(courseTC)).ToList();
	        var visitedCourses = CourseService.GetCourseLinkList(visitedCourseTCs).ToList();
			visitedCourseTCs.Insert(0,courseTC);
	        UserSettingsService.VisitedCourses = visitedCourseTCs;
	        var courseContentTC = courseTC == "сопсв-ю" && Htmls.IsSecond ? CourseTC.WebText : courseTC;
			var courseContents = CourseContentService.GetAll(x => x.Course_TC == courseContentTC).ToList();
	        var certTypes = course.CourseCertificates.Select(cc => cc.CertType).Where(x => x.IsVisible).ToList();
	        var hasTracks = TrackService.GetAllTracksWithCourse(courseTC).Any();
	        var courseDetailsVM =
        		new CourseVM {
        			Course = course,
					SecondCourse = secondCourse,
					CourseInDiplom = CourseService.CoursesInDiploms().Contains(courseTC),
					PrerequisiteTests = preTests,
					HasPaperBook = ExtrasService.CoursesWithPaperBook().Contains(courseTC),
					Actions = actions,
					HasTracks = hasTracks,
					CourseContents = courseContents,
					Tests = tests,
					MaxDiscount = GroupService.GetGroupsForCourse(courseTC)
						.Where(x => !x.IsOpenLearning).Select(x => x.Discount).Max(),
					CompleteCourseCount = CompleteCountForCourses().GetValueOrDefault(courseTC),
                      Responses = responses.ToList(),
					  SuccessStories = successStories,
                      NextCourses = nextCourses.ToList(),
					  WebinarDiscount = PriceService.WebinarDiscouns().GetValueOrDefault(courseTC),
                      NearestGroups = nearestGroups,
                      Certifications = certifications, 
                      Prices = prices,
					  MorningDiscount = morningDiscount,
                      Trainers = trainers.ToList(),
					  Vacancies = vacancies.ToList(),
					  ResponseTotalCount = responseTotalCount,
					  UnlimitPrice = PriceService.GetUnlimitPrice(courseTC),
                      Sections = sections,
                      Product = product,
					  VisitedCourses = visitedCourses,
					  CertTypeList = certTypes,
					  WebinarDiscounts = PriceService.WebinarDiscouns()
                    };

            return courseDetailsVM;
        }

		int GetRootSectionId(int sectionId) {
	        var tree = SectionService.GetSectionsTree();
			return tree.FirstOrDefault(x => x.Section_ID == sectionId ||
				x.SubSections.Any(y => y.Section_ID == sectionId))
				.GetOrDefault(x => x.Section_ID);
		}

		public MobileCourseVM GetMobileByUrlName(string urlName) {
			var course = CourseService.GetByUrlName(urlName);
			if (course == null)
				return null;
			 var section = SiteObjectService.GetSingleRelation<Section>(course)
                .IsActive().OrderBy(x => x.IsMain).FirstOrDefault();
			var model = new MobileCourseVM {
				Course = course, 
				Groups = GroupService.GetGroupsForCourse(course.Course_TC).ToList(),
				Section = section
			};
			model.Prices = PriceService.GetAllPricesForCourse(course.Course_TC, null);
			return model;
		}
		
		public MobileCourseVM GetMobileByGroup(decimal groupId) {
			var group = GroupService.GetPlannedAndNotBegin()
				.FirstOrDefault(x => x.Group_ID == groupId);
			var canOrder = true;
			if (group == null) {
				group = GroupService.GetByPK(groupId);
				canOrder = false;
			}
			var model = GetMobileByUrlName(group.Course.UrlName);
			if (model == null) return null;
			model.Group = group;
			model.Groups = model.Groups.Where(x => x.Group_ID != groupId).ToList();
			model.CanOrder = canOrder;
			return model;

		}

    	public List<TagWithEntity> GetTags(string courseTC)
        {
            var result = new List<TagWithEntity>();
            var course = new Course{Course_TC = courseTC};
            var products = SiteObjectService.GetSingleRelation<Product>(course);
            foreach (var product in products)
            {
                result.Add(new TagWithEntity(product, 1/*SiteObjectRelationService.GetWeight(product)*/));
            }
            var siteTerms = SiteObjectService.GetSingleRelation<SiteTerm>(course);
            foreach (var siteTerm in siteTerms)
            {
                result.Add(new TagWithEntity(siteTerm,1 /*SiteObjectRelationService.GetWeight(siteTerm)*/));
            }

            return 
                result.Cast<TagWithEntity<object>>().ToList().Normalization().Cast<TagWithEntity>().ToList();  
        }

        [Cached]
        public virtual MainCoursesVM GetMainCourses()
        {
        	var allRootSections = SectionVMService.GetSectionWithEntityTree();
        	var professions = ProfessionService.GetAll()
        		.OrderByName().IsActive().ToDictionary(x => x.Profession_ID, x => x);
        	var professionSections =
        		SiteObjectRelationService.GetRelation(typeof (Profession), 
				Enumerable.Empty<object>(), typeof(Section)).ToList();
        	var sectionIds = new HashSet<int>(professionSections.Select(x => x.RelationObject_ID).Cast<int>());
        	var sectionForProfession = 
				allRootSections.Select(x => x.Entity.As<Section>()).Where(x => sectionIds.Contains(x.Section_ID));
        	return
                new MainCoursesVM
                {
                    Products = ProductService.GetAll()
                        .OrderByName().IsActive().ToList(),
                    SiteTerms = SiteTermService.GetAll()
                        .OrderByName().IsActive().ToList(),
                    Professions = sectionForProfession.Select(x => EntityWithList.New(x, 
						professionSections.Where(sor => sor.RelationObject_ID.Equals(x.Section_ID))
						.Select(y => professions.GetValueOrDefault((int)y.Object_ID))
						.Cast<IEntityCommonInfo>().Where(p => p != null))).ToList(),
                    RootSections = allRootSections,
					
                    Vendors = VendorService.GetAll()
                          .OrderByName().IsActive().ToList(),
                };
        }

		

        [Cached]
        public virtual List<MarketingAction> GetActionOnCoursePages() {
        	return MarketingActionService.GetAll(x => x.ShowOnCoursePages && x.IsActive).ToList();
        }

		[Cached]
        public virtual List<string> Discount30Courses() {
        	return GroupService.GetPlannedAndNotBegin().Where(x => x.Discount >= 30)
				.Select(x => x.Course_TC).Distinct().ToList();
        }

		public List<string> GetAllParents(Dictionary<string,List<string>> courseTree, string courseTC) {
			var parents = courseTree.FirstOrDefault(x => x.Value.Contains(courseTC)).Value;
			return parents ?? _.List(courseTC);
			/*.Union(parents.SelectMany(x => courseTree
				.FirstOrDefault(y => y.Value.Contains(x)).Value ?? new List<string>())).ToList()
				.AddFluent(courseTC).Distinct().ToList();*/
		}


    	public List<string> GetCourseTCListForTotalSection(int sectionId) {
			var sectionIds = _.List(sectionId)
				.AddFluent(SectionService.GetChildren(sectionId).Select(x => x.Section_ID));
			return CourseService.GetCourseTCListForSections(sectionIds);
    	}

		[Cached(HoursDuration = 24)]
		public virtual Dictionary<string, int> CompleteCountForCourses() {

			#if(DEBUG)
	            return new Dictionary<string, int>();
			#else
			var courseWithWrongParent = CourseTC.WithRealParent.Select(x => x.Key).ToList();
			var coursesParents = CourseService.GetAll()
				.Where(x => !courseWithWrongParent.Contains(x.Course_TC))
				.Select(x => new{x.Course_TC, x.ParentCourse_TC}).ToList()
				.Concat(CourseTC.WithRealParent.Select(x => new{Course_TC = x.Key, ParentCourse_TC = x.Value}));
			var courses = coursesParents
				.GroupBy(x => x.ParentCourse_TC)
				.ToDictionary(x => x.Key, x => x.Select(y => y.Course_TC).ToList());
			var activeCourses = CourseService.GetAll(x => x.IsActive).Select(x => x.Course_TC).ToList();

			var groupsCounts =
				(from g in GroupService.GetAll()
				group g by g.Course_TC
				into gr
				select new {Course_TC = gr.Key, Count = gr.Sum(g => 
					g.GroupCalc.NumOfStudents + g.GroupCalc.NumOfWebinarists)})
				.ToDictionary(x => x.Course_TC, x => x.Count);

			var result = activeCourses.Select(c => new {CourseTC = c, 
				Count = GetAllParents(courses, c).Sum(x => groupsCounts.GetValueOrDefault(x))})
				.ToDictionary(x => x.CourseTC, x => x.Count);
			return result;
			#endif
		}
    }
}
