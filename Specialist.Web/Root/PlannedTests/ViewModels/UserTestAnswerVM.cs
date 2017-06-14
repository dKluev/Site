using System.Collections.Generic;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.PlannedTests.ViewModels {
	public class UserTestAnswerVM:IViewModel {
		public List<TestQuestion> Questions { get; set; }
		public UserTest UserTest { get; set; }
		public string Title { get { return "Правильные ответы на неправильно отвеченные вопросы по тесту " +
			StringUtils.AngleBrackets(UserTest.Test.Name); } }
	}
}