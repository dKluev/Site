using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using FluentValidation.Validators;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using Specialist.Web.Util;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Root.Tests.Validators {
	public class TestValidators {
		private ModelStateDictionary modelState = null;
		public TestValidators(ModelStateDictionary modelState) {
			this.modelState = modelState;
		}

		private bool EmailValidation(string emial) {
			return Regex.IsMatch(emial, new EmailValidator<string>().Expression,
				RegexOptions.IgnoreCase | RegexOptions.Compiled);
		}
		public void Validate(TestModuleSet set) {
			ErrorIfFalse(!set.Description.IsEmpty(),
				"Описание не должно быть пустым");
		}
		public void Validate(Student student) {
			ErrorIfFalse(student.StudentEmails.Any(),
				student.FullName + " " + "отсутсвует email");
			ErrorIfFalse(student.StudentEmails.Any(x => EmailValidation(x.Email.Trim())),
				student.FullName + " " + "не корректный email");
			
		}
		public void Validate(TestPassRule rule) {
			var totalPercents = EntityUtils.GetModulePercents(rule).Sum(x => x.Value);
			ErrorIfFalse(totalPercents == 100 || totalPercents == 0, "Сумма процентов модулей должна быть 100");
			ErrorIfFalse(rule.QuestionCount > 0, "Количество вопросов должно быть больше 0");
			ErrorIfFalse(rule.Time > 0, "Время должно быть больше 0");
			ErrorIfFalse((!rule.AverageMark.HasValue && !rule.ExpertMark.HasValue)
				|| (rule.PassMark < rule.AverageMark && rule.AverageMark < rule.ExpertMark)
				, "Проходные баллы должны идти по возрастанию");
		} 


		private void ErrorIfFalse(bool b, string error) {
			if (!b)
				modelState.AddModelError("", error);
		}
	}
}