<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<CompetitionsVM>" %>
<%@ Import Namespace="System.EnterpriseServices.CompensatingResourceManager"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% var user = (User)HttpContext.Current.Session["CurrentUserSessionKey"]; %>
    <% if (HttpContext.Current.User.Identity.IsAuthenticated && user != null)
        Model.CurrentUserID = user.UserID; %>

    <%= Htmls2.Tabs(new [] {"Открытые", "Мои конкурсы", "Закрытые"},  new object[]{ 

				Html.Partial(PartialViewNames.CompetitionList, 
                new CompetitionsVM(
                    Model.Competitions.Where(c => !c.WinnerID.HasValue 
						&& c.WinnerName == null))),
						      Html.Partial(PartialViewNames.CompetitionList, 
       new CompetitionsVM(
        Model.Competitions.Where(c => c.UserCompetitions
            .Any(uc => uc.UserID == Model.CurrentUserID)), true)),
					Html.Partial(PartialViewNames.CompetitionList, 
                        new CompetitionsVM(
                            Model.Competitions.Where(c => c.WinnerID.HasValue || c.WinnerName != null)))}, tabContentType:2) %>
  

</asp:Content>
