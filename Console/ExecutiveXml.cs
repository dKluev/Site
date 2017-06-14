using System;
using System.Collections.Generic;
using System.Data.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Common;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Util;
using System.Linq;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Web.Common.Extension;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;

namespace Console {
	public class ExecutiveXml {

		public void Write() {
			var context = new SpecialistDataContext();

			var options = new DataLoadOptions();
			options.LoadWith<Group>(c => c.Course);
			context.LoadOptions = options;

			var content = context.CourseContents.Where(x => x.Course.IsActive)
				.Select(x => new {x.ModuleName, x.ModuleNumber, x.Course_TC})
				.ToList().GroupBy(x => x.Course_TC).ToDictionary(
					x => x.Key, z => z.OrderBy(x => x.ModuleNumber)
						.Select(x => x.ModuleName).ToList());
			var groups = context.Groups.PlannedAndNotBegin()
				.NotSeminars().Where(x => x.DateBeg > DateTime.Today
				&& x.BranchOffice.TrueCity_TC == Cities.Moscow).ToList();
			var events = groups.Select(g => new Conference {
				Name = g.Course.GetName(),
				OrgName = "Центр компьютерного обучения \"Специалист\" при МГТУ им. Н.Э.Баумана",
				PublishDate = DateTime.Today.DefaultString(),
				Description = (content.GetValueOrDefault(g.Course_TC)
					?? new List<string>())
					.AddFluent(
					H.Anchor("http://www.specialist.ru/course/{0}?utm_source=executive&utm_medium=text&utm_campaign=2012".FormatWith(g.Course.UrlName), 
						"Подробнее программу курса смотрите на странице курса").ToString()
						).AddFluent(Text).JoinWith("<br/>"),
				BeginDate = g.DateBeg.DefaultString(),
				EndDate = g.DateEnd.DefaultString(),
				TypeId = 1479,
				SubjectId = 1815,
				Location = "",
				CountryId = 1779,
				RegionId = 5560,
				City = "Москва",
				RegForm = "",
				Contact = "",
				Email = "info@specialist.ru",
				Phone = "(495) 232-3216, 780-4844 (для корпоративных клиентов)"
			}).ToArray();
			SerializationUtils.Serialize(events).Save("events.xml");
			/*var container = new UnityContainer();
			UnityRegistrator.RegisterServices(container);
			var groupService = container.Resolve<IGroupService>();*/

		}
		 
		public class Conference {
			public string Name { get; set; }

			public string OrgName { get; set; }

			public string PublishDate { get; set; }

			public string Description { get; set; }
	
			public string BeginDate { get; set; }

			public string EndDate { get; set; }

			public int TypeId { get; set; }

			public int SubjectId { get; set; }

			public string Location { get; set; }

			public int CountryId { get; set; }

			public int RegionId { get; set; }

			public string City { get; set; }

			public string RegForm { get; set; }

			public string Contact { get; set; }

			public string Email { get; set; }

			public string Phone { get; set; }
		}

		public const string Text =
			@"<a href=""http://www.specialist.ru/courses?utm_source=executive&utm_medium=text&utm_campaign=2012"">Полный каталог курсов Центра «Специалист»</a><br/>
<a href=""http://www.specialist.ru/center/about-center/testingcenter?utm_source=executive&utm_medium=text&utm_campaign=2012"">БЕСПЛАТНОЕ онлайн тестирование</a> <br/>
<a href=""http://www.specialist.ru/center/video/50?utm_source=executive&utm_medium=text&utm_campaign=2012"">Смотрите видеоролик о Центре</a> <br/>
<a href=""http://www.specialist.ru/center/about-center/awards?utm_source=executive&utm_medium=text&utm_campaign=2012"">Награды Центра</a> <br/>
<a href=""http://www.specialist.ru/about-testingcenter?utm_source=executive&utm_medium=text&utm_campaign=2012"">Центр тестироваания и сертификации</a> ";
	}
}