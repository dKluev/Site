<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MarketingAction>>" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%
	var notes =
		" <div class='attention2'><p><strong>Специальные</strong> <strong>предложения </strong>от Центра «Специалист» - <strong>это</strong> отличная <strong>возможность</strong> стать экспертом Microsoft на максимально выгодных условиях. Вы сможете пройти обучение по большому числу авторизованных курсов со скидкой, обучившись у суперспециалистов <strong><a href='/news/1459/' target='_blank'>MVP</a></strong><a href='/news/1459/'> (Most Valuable Professionals)</a> и получив именное свидетельство Microsoft!</p></div>"; %>

<% var tab = Model.Where(x => x.Type != null && !x.IsOrg).GroupBy(x => x.Type).OrderBy(x => x.Key).ToList(); %>
<% var types = tab.Select(x => x.Key).ToList(); %>
<% var tabs = _.List("Все акции").AddFluent(MarketingActionType.GetAll()
       .Where(x=> types.Contains(x.Id)).Select(x => x.Name).ToList()); %>
<% var orgs = Model.Where(x => x.IsOrg).ToList(); %>
<% if(orgs.Any()) tabs.Add("Корпоративные"); %>
<% var actions = _.List(Model.Where(x => !x.IsOrg).ToList()).AddFluent(tab.Select(x => x.ToList())).AddFluent(orgs); %>
<% var contents = actions.Select(x => Html.Partial(PartialViewNames.ActionList, x)).Cast<object>().ToArray(); %>

<div style='margin-left:5px;margin-right:5px;padding-left:5px;padding-right:5px;'>
<%= Htmls2.Tabs(tabs, contents , true, 2) %>
</div>