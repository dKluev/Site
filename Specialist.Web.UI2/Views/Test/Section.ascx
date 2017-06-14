<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Tests.ViewModels.TestSectionVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Tests" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>



<%= Images.Entity(Model.Section).FloatLeft() %>
<%= Model.Section.DescriptionForTest %>

<% var certTests = Model.Tests.Where(x => x.Certified); %>
<% var notCertTests = Model.Tests.Where(x => !x.Certified); %>

<div class="clear"></div>
<% if(certTests.Any()) {%>
<h2>Сертификационные тесты</h2>
<%= Htmls.DefaultList(certTests.Select(x => Url.TestLink(x).ToString())) %>

<% } %>
<% if(notCertTests.Any()) {%>
<h2>Не сертификационные тесты</h2>
<%= Htmls.DefaultList(notCertTests.Select(x => Url.TestLink(x).ToString())) %>

<% } %>

<% Html.RenderAction<TestController>(c => c.Best(Model.Section.Section_ID)); %>