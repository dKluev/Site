
<%@  Page Title="" Language="C#" 
    Inherits="ViewPage<Tuple<List<Specialist.Entities.Context.Certification>, List<Specialist.Entities.Context.Exam>, bool>>" %>
<% if(Model.Item1.Any()){ %>
<p>
    Данный курс готовит к экзаменам, входящим в программы подготовки сертифицированных специалистов международного уровня:
</p>
<%= H.Ul(Model.Item1.Select(x => Html.CertificationLink(x))).Class(Model.Item3 ? "square_blue" : "") %>

<% } %>

<% if(Model.Item2.Any()){ %>
<p>Данный курс готовит к успешной сдаче международных сертификационных экзаменов:</p>
<%= H.Ul(Model.Item2.Select(x => Html.ExamLinkName(x))).Class(Model.Item3 ? "square_blue" : "") %>
<% } %>