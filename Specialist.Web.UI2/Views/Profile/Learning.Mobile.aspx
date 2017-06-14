<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<LearningVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Center" runat="server">
<% if(Model.Student.IsNull()){ return; }%>
   <% Func<string, List<Specialist.Entities.Context.Group>, object> renderGroups = (title, groups) =>
          groups.Any() ?
              MHtmls.LongList(MHtmls.Title(title),
                  MHtmls.MainList(groups.Select(x => H.l(x.DateInterval, 
                      Html.GroupLink(x.Group_ID, x.Course.GetName()))))) : null; %> 
<%= renderGroups("Текущие курсы",Model.Student.CurrentGroups) %>
<%= renderGroups("Предстоящие курсы",Model.Student.ComingGroups) %>
<%= renderGroups("Законченные курсы",Model.Student.EndedGroups) %>

</asp:Content>
