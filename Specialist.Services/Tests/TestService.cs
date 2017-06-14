using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Services.Catalog;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Utils;

namespace Specialist.Services.Tests {
	public class TestService: Repository2<Test> {
		public TestService(IContextProvider contextProvider) : base(contextProvider) {}
			[Dependency]
		public IRepository2<SiteObjectRelation> SiteObjectRelationService { get; set; }
		public Test GetFullTest(int id) {
			LoadWith(b => b.Load(x => x.TestQuestions, x => x.TestPassRule)
				.And<TestQuestion>(q => q.TestAnswers));
			return GetByPK(id);
		}


		public IQueryable<SiteObjectRelation> GetTestSectionRelations() {
			var testType = LinqToSqlUtils.GetTableName(typeof (Test));
			var sectionType = LinqToSqlUtils.GetTableName(typeof (Section));
			var relations = SiteObjectRelationService.GetAll(x => x.ObjectType == testType
				&& x.RelationObjectType == sectionType);
			return relations;
		}

		public Dictionary<string, List<Test>> CourseTests() {
			return MethodBase.GetCurrentMethod().Cache(() => {
				var tests = this.GetAll(x => x.CourseTCList != null
					&& x.Status == TestStatus.Active).ToList()
					.SelectMany(x => x.CourseTCSplitList.Select(y =>
						new {CourseTC = y, Test = x})).ToList();
				return tests.GroupByToDictionary(x => x.CourseTC, x => x.Test);
			}, 24);
		} 

	}
}