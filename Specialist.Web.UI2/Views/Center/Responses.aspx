<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.CommonVM<List<Response>>>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>


<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display:none">
    <%= Model.Title %> 
    </div> 
<%  var n = 0;
    foreach (var response in Model.Data)
   { %>
    <% Html.RenderPartial(Specialist.Web.Const.PartialViewNames.ResponseBlock, response); 
        n++; %>
<% } 
    if(n == 0) {%>
   <p>
   Отзывы не найдены.
   </p>
   <% } %>

</asp:Content>
