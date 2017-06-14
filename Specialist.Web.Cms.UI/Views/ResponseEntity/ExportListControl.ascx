<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Context.RawQuestionnaire>>" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="Specialist.Web.Cms.Controllers" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<% if (!Model.Any()) return;%>
<% using(Html.BeginForm()) { %>
<%--<% if(Model.Select(x => x.Questionnaire.InputDate).Distinct().Count() == 1) %>
<%= Html.Hidden("date", Model.First().Questionnaire.InputDate)%> 
<% } %>--%>
<table class="simple-table">
<% Model.ForEach((rq, i) => { %>
    <tr>
        <td width="50px">
            <%= Html.Hidden("[" + i + "].RawQuestionnaireID", rq.RawQuestionnaireID)%> 
            <%= Html.RadioButton("[" + i + "].IsGood", true) %> 
                <span class="ui-icon ui-icon-plus" style="float:left"></span>  <br />
            <%= Html.RadioButton("[" + i + "].IsGood", false) %> 
                <div class="ui-icon ui-icon-minus" style="float:left"></div>  <br />
        </td>
        <td><%= rq.Notes %></td>
        <td>
            <strong>Активный:</strong> <%= Html.CheckBox("[" + i + "].ResponseIsActive", false) %> <br />
            <strong>Тип:</strong> <%= Html.DropDownList("[" + i + "].Type", 
                                  SelectListUtil.GetSelectItemList(
                                  RawQuestionnaireType.GetAll(),x => x.Name, x => x.Type, currentValue: rq.Type)) %> <br />
            <strong>Курс:</strong> 
                <%= rq.Questionnaire.StudentInGroup.Group.Course.Name %> <br />
            <strong>Преподаватель:</strong> <%= rq.Teacher.FullName %>
        
        </td>
    </tr>
<% }); %>
</table>
<br />
<%= HtmlControls.Submit("Экспортировать") %>
<% } %>
