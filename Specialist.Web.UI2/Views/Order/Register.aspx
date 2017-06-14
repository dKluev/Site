<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Order.ViewModel.RegisterVM>" %>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<% Html.RenderPartial(PartialViewNames.OrderSteps, Model); %>

<%= Htmls.BorderBegin() %>
<div class="tabs-control">
		<ul class="bookmarks">
        	<li class="active"><a href="#" rel="tab-logon"> Вход </a></li>
        	<li><a href="#" rel="tab-register"> Новый пользователь </a></li>
        </ul>
		<div class="tab-logon tab_content"  >
		    <div style="margin:5px">
    			<% Html.RenderAction<AccountController>(c => c.LogOnControl(null));%>
		    </div>
		</div>
		<div class="tab-register tab_content" style="display:none">
		    <div style="margin:5px">
            <% Html.RenderAction<ProfileController>(c => c.RegisterControl(
                Url.Action<OrderController>(c2 => c2.Contract()),
                Model.Order.CustomerType)); %>
		    </div>
		</div>
</div>
<%= Htmls.BorderEnd %>
</asp:Content>
