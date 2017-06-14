<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EditExamVM>" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<h4>Экзамен:</h4>
<p> <%= Html.ExamLink(Model.OrderExam.Exam) %> </p>

<% using (Html.BeginForm<EditCartController>(c => c.EditExam(null))) { %>
    <%= Html.HiddenFor(x => x.OrderExam.Exam_ID) %>
    
    <h4>Выберите дату:</h4>
    
    <%= Html.DropDownListFor(x => x.OrderExam.Group_ID, Model.GetAllGroups()) %>
    <p><%= Images.Submit("ok") %></p>
<% } %>


 


