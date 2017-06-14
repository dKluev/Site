<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TrackVM>" %>
<%@ Import Namespace="SimpleUtils.Common" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Services.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="Specialist.Services.Interface.Passport" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Cms" %>
<%@ Import Namespace="Microsoft.Practices.Unity" %>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="head" runat="server">
    <title>Specialist CMS</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <% Func<List<Tree<string>>, int, string> list = null;
       list = (z, f) => {
           if (!z.Any()) return "";
           var maxDeep = z.Max(x => x.Deep);
           return H.div[z.Select(x => {
               return H.div[maxDeep == 0 ? x.Value : StringUtils.Tag(x.Value,
                   "h" + (f == 0 ? 2 : (4 - maxDeep))), list(x.Nodes, f + 1)];
           })].ToString();
       }; %>
<table><tr>
           <% foreach (var nodes in Html.MainMenu().Nodes.Cut(6)) { %>
           <td style="vertical-align: top;"><%= list(nodes,0) %></td>
            <%  } %>
       </tr></table>
    

<br />
<% if(MvcApplication.Container.Resolve<IAuthService>().CurrentUser.GetOrDefault(x => x.Email) 
	   == MailService.motorina.Address) {%>
<h2><%= H.Anchor("http://www.specialist.ru/center/polls/1","Все опросы") %></h2>
<% } %>

</asp:Content>
