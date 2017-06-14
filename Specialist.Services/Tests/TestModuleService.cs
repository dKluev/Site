using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using Specialist.Services.Catalog;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Tests {
	public class TestModuleService: Repository2<TestModule> {
		public TestModuleService(IContextProvider contextProvider) : base(contextProvider) {}

		[Dependency]
		public IRepository2<CourseContent> CourseContentService { get; set; }

		public IQueryable<TestModule> GetForTest(int testId) {
			return this.GetAll(x => x.TestId == testId);
		} 


		public void CreateModulesFromCourse(Test test) {
			if (test.CourseTCList.IsEmpty()) return;
			if (GetForTest(test.Id).Any()) return;

			var contents = CourseContentService.GetAll(x => x.Course_TC == test.CourseTCList)
				.Select(x => new {x.CourseContent_ID,x.ModuleName}).ToList().Select(x => 
					new TestModule{TestId = test.Id, Name = x.ModuleName, CourseContent_ID = x.CourseContent_ID}).ToList();
			contents.ForEach(Insert);
			SubmitChanges();
	
			

		}
	}
}