<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.ViewModel.Responses.ResponseSearchVM>" %>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="DynamicForm.Utils" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>����� ������� ��� ��������</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2> ����� ������� ��� ��������</h2>

 <% using(Html.BeginForm<ResponseEntityController>(c => c.SearchPost(null))){ %>
 <p>
       <label>��� �������������</label>
       <%= Html.TextBoxFor(x => x.TrainerTC) %>
 </p>
 <p>
       <label>���� ������</label>
       <% Html.RenderPartial(PartialViewNames.CourseTCList, Model); %>
 </p>
  <p>
       <label>���</label>
       <%= Html.DropDownListFor(x => x.ResponseType, 
           SelectListUtil.GetSelectItemList(RawQuestionnaireType.GetAll(),
                  x => x.Name, x => x.Type,"���")) %>
 </p>
 <p>
        <label>��� ������</label>
        <%= Html.TextBoxFor(x => x.Group_ID) %>
 </p>
  <p>
        <label>�������</label>
        <%= Html.CheckBoxFor(x => x.IsWebinar) %>
 </p>
<%= HtmlControls.Submit("������") %>
 <% } %>


<% Html.RenderPartial(PartialViewNames.ExportListControl, Model.RawQuestionnaires); %>


</asp:Content>

