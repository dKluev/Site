using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Extension;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Util;

namespace Specialist.Services.Tests {
	public class UserTestResultService {
		[Dependency]
		public TestModuleService TestModuleService { get; set; }

		public List<List<string>> GetResultData(List<List<UserTest>> userTests, bool trackTest = false) {
			var items = _.List("Тест", "Фио", "Результат", "Ответы(П \\ Н)", "Дата", "Модули:");
			var data = new List<List<string>>();
			foreach (var userTestList in userTests) {
				if(!userTestList.Any())
					continue;
				var modules = TestModuleService.GetForTest(userTestList.First().TestId).OrderBy(x => x.Id).ToList();
				data.Add(new List<string>(items).AddFluent(modules.Select(x => x.Name)));;
				foreach (var userTest in userTestList) {
					var stats = EntityUtils.GetStats(userTest);
					data.Add(_.List(userTest.Test.Name, userTest.User.FullName, 
						trackTest ? UserTestStatus.TrackNames[userTest.Status] : UserTestStatus.GetName(userTest.Status), 
						new UserTestStats.RightWrong{R = userTest.RightCount.GetValueOrDefault(),
						W = userTest.WrongCount.GetValueOrDefault()}.ToString(),  
						userTest.RunDate.DefaultWithTimeString()).AddFluent("").AddFluent(
						stats.Select(x => x.Value.ToString())));
				}
			}
			return data;
		}
		 
	}
}