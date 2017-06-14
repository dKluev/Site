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
			return "������ �� �������";
		}
		private string GetTitle(WorkPlace model) {
			return "���������� � ����� ������";
		}
		private string GetTitle(JubileeFormVM model) {
			return "������������ c �������";
		}
		private string GetTitle(MyBusinessUser model) {
			return "����������� ��������� " + StringUtils.AngleBrackets("��� ����");
		}
		private string GetTitle(Video model) {
			return model.Name;
		}
		private string GetTitle(GroupTestResultVM model) {
			return "���������� ������������ ������ " + model.GroupInfo.Group_ID;
		}

		private string GetTitle(JubileeGroupsVM model) {
			return "��������� ������ �� ������������";
		}
		private string GetTitle(TestListVM model) {
			return "�����";
		}
		private string GetTitle(UserTestsVM model) {
			return "��� �����";
		}
		private string GetTitle(TestCertificatesVM model) {
			return "������� ������ ������������ ������������";
		}
		private string GetTitle(TestVM model) {
			return "���� " + StringUtils.AngleBrackets(model.Test.Name);
		}

		private string GetTitle(MainTestsVM model) {
			return "����������� ������������";
		}

		private string GetTitle(TestRunVM model) {
			return model.Test.Name;
		}

		private string GetTitle(TestRunDetailsVM model) {
			return model.CourseName ?? model.Test.Name;
		}

		private string GetTitle(GroupInfosVM model) {
			return "������ ������������";
		}

		private string GetTitle(GroupPrepareVM model) {
			return "���������� ������ � ������������";
		}
		private string GetTitle(TestSectionVM model) {
			return model.Section.Name;
		}
		private string GetTitle(AjaxGridVM model) {
			return model.Caption;
		}
		private string GetTitle(TestEditVM model) {
			return GetCrudTitle(model.Test.Id, "�����", model.Test.Name);
		}

		private string GetTitle(TestModule model) {
			return GetCrudTitle(model.Id, "������");
		}

		private string GetTitle(TestQuestion model) {
			return GetCrudTitle(model.Id, "�������");
		}
		private string GetTitle(ModuleSetVM model) {
			return GetCrudTitle(model.ModuleSet.Id, "��������� ������������");
		}

		private string GetTitle(TestAnswer model) {
			return GetCrudTitle(model.Id, "������");
		}
		private string GetTitle(GroupTestEditVM model) {
			return GetCrudTitle(model.GroupTest.Id, "����� ������");
		}
		private string GetTitle(TestResultVM model) {
			return "��������� ������������";
		}

		private string GetTitle(AssignTestsVM model) {
			return "����������� �����";
		}

		private string GetTitle(TestReadOnlyVM model) {
			return "���� " + StringUtils.AngleBrackets(model.Test.Name);
		}

		private string GetCrudTitle(int id, string name, string title = null) {
			if (id > 0)
				return "�������������� " + name + " " + title.GetOrDefault(x => StringUtils.AngleBrackets(title));
			return "�������� " + name;
		}
	}
}