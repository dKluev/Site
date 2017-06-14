<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<City>>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
<div id="content" class="longlist">
<h2>Оплата наличными</h2>
Оплатить обучение Вы можете во всех комплексах Центра
<br/>
<%= Url.Complexes().Class("link") %>
</div>
</asp:Content>
