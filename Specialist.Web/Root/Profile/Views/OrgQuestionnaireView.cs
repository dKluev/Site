using System;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Helpers;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgQuestionnaireView:BaseView<OrgQuestionnaireVM> {
		public override object Get() {
			var q = Model.Questionnaire;
			if(Model.Questionnaire == null)
				return p["Пока информации нет"];

			var items = _.List(
				Tuple.Create("Как бы Вы оценили полученные Вами на курсе знания и приобретенные навыки?",
					Tuple.Create(q.SkillsLetter, q.SkillsComment)),
				Tuple.Create("Как бы Вы оценили курс?",
					Tuple.Create(q.CourseLetter, q.CourseComment)),
				Tuple.Create("Как бы Вы оценили работу Центра по организации Вашего обучения и выдачу документов?",
					Tuple.Create(q.OrganizingLetter, q.OrganizingComment)),
				Tuple.Create("Что Вам больше всего понравилось в пройденном курсе?",
					Tuple.Create((char?)null,q.ExpectationComment)),
				Tuple.Create("Есть ли у Вас знакомые, которым Вы могли бы порекомендовать обучиться в нашем Центре?",
					Tuple.Create((char?)null,q.AdministrationComment)),
				Tuple.Create("Что нам следовало бы сделать по-другому, чтобы Ваше впечатление о Центре было бы лучше?",
					Tuple.Create((char?)null,q.StudentNotes))).Where(x => x.Item2.Item1.HasValue 
						|| !x.Item2.Item2.IsEmpty());
			return div[items.Select(x => p[h3[x.Item1], (x.Item2.Item1.HasValue
				? "Оцентка: {0} ".FormatWith(x.Item2.Item1): "") + x.Item2.Item2])];
		}
	}
}