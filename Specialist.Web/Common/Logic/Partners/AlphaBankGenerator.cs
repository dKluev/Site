using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using MvcContrib;
using NLog;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Services.Interface;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Lms;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Root.Common.Services;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Services.Common {
	public class AlphaBankGenerator:XmlShopGenerator {
		public const string ActionMin5000 = "ILKD";
		public const int ActionMax = 10000;
		public const int MinPrice = 5000;

		class ListRow {
			public string Code { get; set; }
			public int Amount { get; set; }
			public int Price { get; set; }
			public string Description { get; set; }
			public ListRow(string code, int amount, int price, string description) {
				Code = code;
				Amount = amount;
				Price = price;
				Description = StringUtils.SafeSubstring(StringUtils.RemoveTags(description),50);
			}

			public static ListRow FromSig(StudentInGroup sig) {
				return new ListRow(sig.Group.Course_TC, 1, (int)sig.Charge, sig.Group.Course.Name);
			}

			public static ListRow FromOrderDetail(OrderDetail od) {
				return new ListRow(od.Course_TC, 1, (int)od.PriceWithDiscount, od.Course.Name);
			}
			public static ListRow FromOrderExam(OrderExam od) {
				return new ListRow("exam" + od.Exam_ID, 1, (int)od.Price, od.Exam.ExamName);
			}
		}

		[Dependency]
    	public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

		public class CreditDataException : Exception {
			public CreditDataException(string message) : base(message) {}
		}


		public string FromCredit(decimal creditId) {

		    var context = new PioneerDataContext();
			StudentInGroupService.LoadWith(x => x.Student,x => x.Group);
			StudentInGroupService.LoadWith(c => c.Load(x => x.Student, x => x.Group).And<Group>(x => x.Course));
	        var sig = StudentInGroupService.GetAll(x => x.CreditRequest_ID == creditId).ToList();
		    var ourOrgTC = sig.Select(x => context.fnGetDefaultOurOrgTC(x.StudentInGroup_ID, null)).ToList();
	        if (ourOrgTC.Distinct().Count() != 1) {
		        throw new CreditDataException("Кредитование недоступно");
	        }
    		var inn = OrderCommonConst.AlphaCredit.GetValueOrDefault(ourOrgTC.First());
	        if (inn == null) {
		        throw new CreditDataException("Кредитование недоступно");
	        }
	        var price = sig.Sum(x => x.Charge);
			var dataCheck = CorrectData(sig.Select(x => x.Group.DateBeg).ToList(), price.Value);
	        if (!dataCheck.Item1) {
		        throw new CreditDataException("Некорректная " + (dataCheck.Item2 ? "дата" : "цена"));
	        }
	        var student = sig.First().Student;
			var rows = sig.Select(ListRow.FromSig);
	        var id = "cred" + creditId;
	        return CreateXml(inn, id, student.LastName, student.FirstName, student.MiddleName, price, rows);
        }

		static Tuple<bool, bool> CorrectData(List<DateTime?> dates, decimal price) {
			var minDate = MinAvailableDate();
			var correctDates = dates.Where(x => x.HasValue).All(x => x >= minDate);
			var correctPrice = MinPrice <= price && price <= 200000;
			return Tuple.Create(correctDates && correctPrice, correctPrice);
		}


		public static DateTime MinAvailableDate() {
			var count = 21;
			var dayOffList = CalendarService.DayOffList();
			var workDays = Enumerable.Range(0, count)
				.Select(x => DateTime.Today.AddDays(x))
				.Where(x => !(dayOffList.Contains(x) || DateUtils.IsWeekend(x)));
			return workDays.Skip(5).First();

		}

        public static string FromOrder(Entities.Context.Order order) {
    		var inn = OrderCommonConst.AlphaCredit.GetValueOrDefault(order.OurOrgOrDefault);
	        if (inn == null || order.OrderExams.Any()) {
		        return null;
	        }
			var rows = order.OrderDetails.Select(ListRow.FromOrderDetail)
				.Concat(order.OrderExams.Select(ListRow.FromOrderExam));
	        var price = order.TotalPriceWithDescount;

	        if (!CorrectData(order.OrderDetails.Select(x => x.Group.GetOrDefault(y => y.DateBeg)).ToList(), price).Item1) {
		        return null;
	        }
	        var id = order.CommonOrderId;
	        return CreateXml(inn, id, order.User.LastName, order.User.FirstName, order.User.SecondName, price, rows);
        }

		static string CreateXml(string inn, string id, string lastName, string firstName, 
			string middleName, decimal? price, IEnumerable<ListRow> rows) {
	        var action = price < ActionMax ? ActionMin5000 : null;
			var xml =
				X("inParams",
					Company(inn),
					X("creditInfo", X("reference", id)),
					ClientInfo(lastName, firstName, middleName),
					X("specificationList", rows.Select(x => SpecificationListRow(x,action))));
			return xml.ToString();
		}

		static object Company(string inn) {
			return X("companyInfo", X("inn", inn));
		}

		static object ClientInfo(string lastName, string firstName, string middleName) {
			return X("clientInfo",
				X("lastname", lastName),
				X("firstname", firstName),
				X("middlename", middleName)
			);
		}

		static object SpecificationListRow(ListRow sig, string action) {
			return X("specificationListRow",
				X("category", "EDUCATION"),
				X("code", sig.Code),
				X("amount", sig.Amount),
				action.IsEmpty() ? null : X("action", action),
				X("price", sig.Price),
				X("description", sig.Description)
				);
		}

	}
}