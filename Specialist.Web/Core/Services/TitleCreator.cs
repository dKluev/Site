using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.ViewModel;
using Specialist.Web.Core.Views;
using Specialist.Web.Pages;
using Specialist.Web.Root.Center.ViewModels;
using Specialist.Web.Root.Learning.ViewModels;
using Specialist.Web.Root.Profile.Logic;
using Specialist.Web.Root.Tests.ViewModels;

namespace SpecialistTest.Web.Common {
	public class TitleCreator {
		public string Get(object model) {
			if (model == null || model.GetType() == typeof (object))
				return string.Empty;
			return GetTitle((dynamic) model);
		}


		private string GetTitle(object model) {
			return "";
		}

		private string GetTitle(IViewModel model) {
			return model.Title;
		}
		private string GetTitle(PrerequisiteTestVM model) {
			return model.Course.GetName();
		}
		private string GetTitle(GroupsWithDiscountVM model) {
			return "Группы со скидкой";
		}
		private string GetTitle(WorkPlace model) {
			return "Информация о месте работы";
		}
		private string GetTitle(JubileeFormVM model) {
			return "Поздравление c юбилеем";
		}
		private string GetTitle(MyBusinessUser model) {
			return "Регистрация промокода " + StringUtils.AngleBrackets("Мое дело");
		}
		private string GetTitle(Video model) {
			return model.Name;
		}
		private string GetTitle(GroupTestResultVM model) {
			return "Результаты тестирования группы " + model.GroupInfo.Group_ID;
		}

		private string GetTitle(JubileeGroupsVM model) {
			return "Юбилейные скидки от «Специалиста»";
		}
		private string GetTitle(TestListVM model) {
			return "Тесты";
		}
		private string GetTitle(UserTestsVM model) {
			return "Мои тесты";
		}
		private string GetTitle(TestCertificatesVM model) {
			return "Текущие заказы сертификатов тестирования";
		}
		private string GetTitle(TestVM model) {
			return "Тест " + StringUtils.AngleBrackets(model.Test.Name);
		}

		private string GetTitle(MainTestsVM model) {
			return "Направления тестирования";
		}

		private string GetTitle(TestRunVM model) {
			return model.Test.Name;
		}

		private string GetTitle(TestRunDetailsVM model) {
			return model.CourseName ?? model.Test.Name;
		}

		private string GetTitle(GroupInfosVM model) {
			return "Группы тестирования";
		}

		private string GetTitle(GroupPrepareVM model) {
			return "Подготовка группы к тестированию";
		}
		private string GetTitle(TestSectionVM model) {
			return model.Section.Name;
		}
		private string GetTitle(AjaxGridVM model) {
			return model.Caption;
		}
		private string GetTitle(TestEditVM model) {
			return GetCrudTitle(model.Test.Id, "теста", model.Test.Name);
		}

		private string GetTitle(TestModule model) {
			return GetCrudTitle(model.Id, "модуля");
		}

		private string GetTitle(TestQuestion model) {
			return GetCrudTitle(model.Id, "вопроса");
		}
		private string GetTitle(ModuleSetVM model) {
			return GetCrudTitle(model.ModuleSet.Id, "планового тестирования");
		}

		private string GetTitle(TestAnswer model) {
			return GetCrudTitle(model.Id, "ответа");
		}
		private string GetTitle(GroupTestEditVM model) {
			return GetCrudTitle(model.GroupTest.Id, "теста группы");
		}
		private string GetTitle(TestResultVM model) {
			return "Результат тестирования";
		}

		private string GetTitle(AssignTestsVM model) {
			return "Назначенные тесты";
		}

		private string GetTitle(TestReadOnlyVM model) {
			return "Тест " + StringUtils.AngleBrackets(model.Test.Name);
		}

		private string GetCrudTitle(int id, string name, string title = null) {
			if (id > 0)
				return "Редактирование " + name + " " + title.GetOrDefault(x => StringUtils.AngleBrackets(title));
			return "Создание " + name;
		}
	}
}