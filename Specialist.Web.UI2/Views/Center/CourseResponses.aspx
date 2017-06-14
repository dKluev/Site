<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Contents.ViewModels.CourseResponsesVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">


<%= Htmls2.Tabs(Model.Tabs(),new [] {
	Html.Partial(PartialViewNames.ResponseList, Model.Responses.Select(x => x.FluentUpdate(z => z.Employee_TC = null)))
    + Html.GetNumericPager(Model.Responses, "{0}"),
	Html.Partial(PartialViewNames.OrgResponseList, Model.OrgResponses).ToString()}, tabContentType:2) %>

</asp:Content>
