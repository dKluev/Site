using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.UnityInterception;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Utils;

namespace Specialist.Services.Order {
	public class ExtrasService:Repository<Extras> {


		[Dependency]
		public IRepository2<ExtrasCourse> ExtrasCourseService { get; set; }
		[Dependency]
		public IRepository2<CoursesCourseGiveOutItem> CoursesCourseGiveOutItemService { get; set; }

		public ExtrasService() 
			: base(new ContextProvider()) {}

		public HashSet<string> CoursesWithPaperBook() {
			return MethodInfo.GetCurrentMethod().CacheDay(() => new HashSet<string>(
				CoursesCourseGiveOutItemService.GetAll(x => 
					x.CourseGiveOutItem.Quantity > 0 
					&& x.CourseGiveOutItem.ItemType_TC == GiveOutItemTypes.Book)
					.Select(x => x.Course_TC).Distinct()));
		} 
		[Cached]
		public override System.Linq.IQueryable<Extras> GetAll() {
			return base.GetAll().Where(x => x.CurrentPrice > 0 && x.IsVisible).ToList().AsQueryable();
		}
		[Cached]
		public virtual Dictionary<string, Extras> EnglishBooks() {
			return GetAll().Where(e => e.IsEngBook)
				.DistinctToDictionary(x => x.ExtrasName.Remove("Учебник").Trim(),
					x => x);
		} 

		string GetPaintCourseTC(string name) {
			return Regex.Replace(name, "Раздаточный материал к курсу (.*?): .*", "$1");
		}

		string GetTravelCourseTC(string name) {
			return Regex.Replace(name, ".*/(.*?)/.*", "$1");
		}
		[Cached]
		public virtual Dictionary<string, Extras> Paint() {
			return GetAll().Where(e => e.IsPaint)
				.DistinctToDictionary(x => GetPaintCourseTC(x.ExtrasName), x => x);
		} 
		[Cached]
		public virtual Dictionary<string, Extras> Travel() {
			return GetAll().Where(e => e.IsTravel)
				.DistinctToDictionary(x => GetTravelCourseTC(x.ExtrasName), x => x);
		} 

		public List<string> CourseWithCourier = new List<string> {
			"ПОДАР1", "ПОДАР2", "ПОДАР3"
	};
		public decimal GetPrice(decimal extrasId) {
			return GetAll().FirstOrDefault(x => x.Extras_ID == extrasId).CurrentPrice.GetValueOrDefault();
		}
		public IEnumerable<Extras> GetFor(OrderDetail orderDetail) {
			return new List<Extras>();
/*
			var courseTC = orderDetail.Course_TC;
			if(CourseWithCourier.Contains(courseTC))
				return GetAll().Where(e => e.Extras_ID == Extrases.Courier);
			if(courseTC == CourseTC.Depifr)
				return GetAll().Where(e => e.Extras_ID == Extrases.Depifr);
			if(courseTC.In(CourseTC.C8b1, CourseTC.C8b2))
				return GetAll().Where(e => e.Extras_ID == Extrases.BuhTrial);

			if(orderDetail.IsTestCert && orderDetail.Params.IsPaper)
				return GetAll().Where(e => Extrases.ForTestCert.Contains(e.Extras_ID));
			var extrases = new List<Extras>();
			var extras = EnglishBooks().GetValueOrDefault(courseTC);
			if(extras != null) extrases.Add(extras);
			extras = Paint().GetValueOrDefault(courseTC);
			if(extras != null) extrases.Add(extras);
			extras = Travel().GetValueOrDefault(courseTC);
			if(extras != null) extrases.Add(extras);

			var courseExtraseIds = CourseExtarses()
				.GetValueOrDefault(courseTC) ?? new List<decimal>();
			if (courseExtraseIds.Any()) {
				extrases.AddRange(GetAll().Where(x => courseExtraseIds.Contains(x.Extras_ID)));
			}
			extrases = ClearExtrases(orderDetail, extrases);
			return extrases;
*/
		}

		private List<Extras> ClearExtrases(OrderDetail orderDetail, List<Extras> extrases) {
			if (orderDetail.Group != null && orderDetail.Group.DateBeg <= DateTime.Today.AddDays(3)) {
				var labIds = MicrosoftLabs();
				extrases = extrases.Where(x => !labIds.Contains(x.Extras_ID)).ToList();
			}
			return extrases;
		}

		public Dictionary<string, List<decimal>> CourseExtarses() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var courseWithPaperBook = CoursesWithPaperBook().Select(x => new ExtrasCourse {
					Course_TC = x, Extras_ID = Extrases.PaperBook
				});
				return ExtrasCourseService.GetAll().ToList().Concat(courseWithPaperBook)
					.GroupByToDictionary(x => x.Course_TC, x => x.Extras_ID);
			});
		} 

		public List<decimal> MicrosoftLabs() {
			return MethodBase.GetCurrentMethod().CacheDay(() => 
				GetAll().Where(x => x.IsMicrosoftLab).Select(x => x.Extras_ID).ToList());
		} 

		public List<string> MicrosoftLabCourses() {
			var extraseIds = MicrosoftLabs();
			return CourseExtarses().Where(x => x.Value.Intersect(extraseIds).Any()).Select(x => x.Key)
				.ToList();
		}

		public List<CartVM.ExtrasText> Texts() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {

				var msLabs = new CartVM.ExtrasText(
					MicrosoftLabs(),
					MicrosoftLabCourses(),
					"С этим курсом Вы можете заказать доступ к виртуальным лабораторным работам Labs Online. Внимание: чтобы Вы успели получить доступ вовремя, просим оплачивать обучение и доступ к Labs Online не менее чем за 3 рабочих дня до старта группы! Узнать подробнее об услуге <a href='/microsoft-labs-online' >Labs Online</a>. Для заказа доступа к виртуальным лабораторным работам воспользуйтесь выбором дополнительных услуг.");

				var engs = new CartVM.ExtrasText(
					EnglishBooks().Select(x => x.Value.Extras_ID).ToList(),
					EnglishBooks().Select(x => x.Key).ToList(),
					"Вы не заказали учебное пособие. Обращаем Ваше внимание, что для успешного обучения учебное пособие необходимо. Вы можете приобрести учебное пособие в нашем Центре или прийти на занятия со своим пособием. Для заказа учебного пособия воспользуйтесь выбором дополнительных услуг.",
					true );
				var paint = new CartVM.ExtrasText(
					Paint().Select(x => x.Value.Extras_ID).ToList(),
					Paint().Select(x => x.Key).ToList(),
					"Вы не заказали художественные товары, необходимые для обучения и творчества. Обращаем Ваше внимание, что для успешного обучения художественные товары (бумага, средства и инструменты рисунка и живописи, скульптурный пластилин и др.) необходимы. Вы можете приобрести художественные товары в нашем Центре или прийти на занятия со своими. Для заказа художественных товаров воспользуйтесь выбором дополнительных услуг.",
					true );
				return _.List(msLabs, engs, paint);

			});
		}

	}
}