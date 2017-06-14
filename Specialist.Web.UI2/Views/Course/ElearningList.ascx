<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<ElearningCourse>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context.Extension" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>

<% var site = Html.Site(); %>
<%= 
	H.table.Class("table")[
	site.TableHead("Курс","Самостоятельное обучение",
	"Обучение с преподавателем", Images.Main("ico_signup2.gif")),
	Model.Select(course => site.TableRow(
		H.td[Html.CourseLink(course.CourseLink)],
	H.td[course.Prices.GetPrice(PriceTypes.ElearningS).MoneyString()],
	H.td[course.Prices.GetPrice(PriceTypes.ElearningP).MoneyString()],
	H.td[ Html.AddToCart(x => x.AddCourse(course.CourseLink.CourseTC,
                PriceTypes.ElearningS))]
		))
	
	
	] %>


  



