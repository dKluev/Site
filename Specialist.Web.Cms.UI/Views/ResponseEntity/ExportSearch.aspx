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
	<title>Поиск отзывов для экспорта</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2> Поиск отзывов для экспорта</h2>

 <% using(Html.BeginForm<ResponseEntityController>(c => c.SearchPost(null))){ %>
 <p>
       <label>Код преподавателя</label>
       <%= Html.TextBoxFor(x => x.TrainerTC) %>
 </p>
 <p>
       <label>Коды курсов</label>
       <% Html.RenderPartial(PartialViewNames.CourseTCList, Model); %>
 </p>
  <p>
       <label>Тип</label>
       <%= Html.DropDownListFor(x => x.ResponseType, 
           SelectListUtil.GetSelectItemList(RawQuestionnaireType.GetAll(),
                  x => x.Name, x => x.Type,"Нет")) %>
 </p>
 <p>
        <label>Код группы</label>
        <%= Html.TextBoxFor(x => x.Group_ID) %>
 </p>
  <p>
        <label>Вебинар</label>
        <%= Html.CheckBoxFor(x => x.IsWebinar) %>
 </p>
<%= HtmlControls.Submit("Искать") %>
 <% } %>


<% Html.RenderPartial(PartialViewNames.ExportListControl, Model.RawQuestionnaires); %>


</asp:Content>

