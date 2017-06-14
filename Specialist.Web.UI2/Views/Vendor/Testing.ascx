<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExamListVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Examination.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="MvcContrib.Unity"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="System.Linq"%>

    <% Html.RenderPartial(PartialViewNames.CommonExamList, Model.Exams); %>
    <%= Html.GetNumericPagerPretty(Model.Exams) %>
