<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Tests.ViewModels.PrerequisiteTestsVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Tests" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<%= Model.Courses.Select(x => H.l(H.h3[x.Key.Name], H.table[H.Head("Курс","Тест предварительной подготовки"), x.OrderBy(c => c.WebSortOrder).Select(c =>H.tr[H.td[Url.CourseLink(c)].Style("text-align:left;"),H.td[ Url.Link<TestController>(k => 
	k.Prerequisite(c.Course_ID),"Пройти тестирование")]])].Class("table")).ToString()).JoinWith("") %>