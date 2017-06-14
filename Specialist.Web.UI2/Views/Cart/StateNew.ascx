<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Tuple<string>>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<% var parts = Model.Item1.Split(' '); %>
<% var count = parts[0]; %>
<% var isEmpty = count == "0"; %>
<% var items = parts[1]; %>

<% var url = isEmpty ? SimplePages.FullUrls.Payment : Url.Cart().Urls.Details(); %>

    <div class="basket_center">

 <%= H.Anchor(url, "���������� �� �����").Class("discount_color").Id("link2") %>
 <p>
    <span>�������:</span>
    <%= Url.Link<CartController>(oc => oc.Details(), isEmpty ? "�������" : items).Class("link").Style("white-space:nowrap;") %>
 </p>

       <%=  
		Images.Main("ico_timetable.gif").Alt("����� ����������")
			.Title("����� ����������").Class("timetable") %>
            <%= Url.Link<GroupController>(c => c.Search(null), "����� ����������").Style("margin-left:18px") %>
    </div>

<%= isEmpty ? null : H.Div("notice")[count] %>