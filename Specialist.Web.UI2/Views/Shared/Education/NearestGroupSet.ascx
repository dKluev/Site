<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NearestGroupSet>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Entities"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>

<%	if(Model.IsEmpty) return; %>
<% 
	var tabs = new List<string> { "���" };
	var minTimeFrom = Model.EveningMinTimeFrom();
	tabs.AddRange(Model.DayShiftGroups.Select(x => 
	   x.Entity.Name + " [" + x.Entity.GetTimeInterval(minTimeFrom) + "]"));
	tabs.Add("�������� ���"); 
	if(Model.Webinars.Any())
		tabs.Add("��������");
%>


<% var tabContents = Model.GetAllSectionGroups(); %>
<% var isTest = Model.All.First().Course_TC == "��1-�"; %>

<%= Htmls2.Tabs(tabs.ToArray(), tabContents.Select(x =>
		Html.Partial(PartialViewNames.NearestGroupList,
				 new NearestGroupsVM(x.V2, true) {
				 	HideCart = CourseTC.HalfTracks.ContainsKey(Model.CourseTC),
					WebinarTab = x.V1 == "sectionwebinars",
					Prices = Model.Prices,
					IsTest = isTest
				 })).Cast<object>().ToArray(), 
		tabContentType: 2)%>
