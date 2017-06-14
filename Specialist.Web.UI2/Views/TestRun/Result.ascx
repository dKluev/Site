<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Tests.ViewModels.TestResultVM>" %>
<%@ Import Namespace="System.Activities.Statements" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Entities.Tests.Consts" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<p>
	<% if(Model.UserTest.IsPrerequisite){ %>
		<strong>Курс:</strong> <%= Model.CourseName %>
	
	<% }else if(Model.UserTest.IsCoursePlanned){ %>
	<% }else{ %>
		<strong>Тест:</strong>
			<%= Url.TestActiveOnlyLink(Model.UserTest.Test) %>
	
	<% } %>
</p>
<p>
	<strong>Результат:</strong>
	<%= Model.ResultMessage %>
</p>
<% if (Model.UserTest.ShowAnswers && Model.UserTest.WrongCount > 0) { %>
<p>
	<%= Url.TestRun().UserTestAnswers(Model.UserTest.Id, "Правильные ответы") %>
</p>
<% } %>
<p>
	<strong>Время:</strong>
	<%= TimeSpan.FromSeconds(Model.UserTest.Time).ToString("mm\\:ss") %>
</p>
<p>
	<strong>Дата тестирования:</strong>
	<%= Model.UserTest.RunDate.DefaultWithTimeString() %>
</p>
<% if (Model.CanBuy) { %>
<% if (Model.UserTest.Test.Certified) { %>
<%= Html.AddToCart(c => c.AddTestCert(Model.UserTest.Id), Urls.Button("testcertorder")) %> <br/>
<b>Ваш квалификационный уровень будет указан в сертификате после названия теста</b>
<% }else{ %>
<p>Сертификат по данному тесту не выдается.</p>
<% } %>
<% } %>
<% var rule = Model.UserTest.TestPassRule; %>
<% var points = _.List<int?>(Model.UserTest.RightCount.GetValueOrDefault(), rule.PassMark, rule.AverageMark, rule.ExpertMark, rule.QuestionCount)
	   .Where(x => x.HasValue); %>
<%= HtmlControls.Image("http://chart.apis.google.com/chart?" +
    "cht=p3&chd=t:{0},{1}&chco=599AF1|F8BB5A&chs=450x200&chdl=Правильные ответы - {0}|Неправильные ответы - {1}"
		.FormatWith(Model.UserTest.RightCount.GetValueOrDefault(), Model.UserTest.WrongCount.GetValueOrDefault()))%>
<br />

<% if (Model.WithLevels) { %>
    <% foreach (var level in TestRecomendations.EnglishLevels) { %>
        <%= H.strong[level.Item3] %>: 
            <%= level.Item1 + (level.Item2 == int.MaxValue ? "+" : " - " + level.Item2) %> <br/>
    <% } %>
    <br/>
<% } else { %>
<% var names = Model.IsTrack ? "Удовлетворительно|Хорошо|Отлично" : "Пользователь|Опытный пользователь|Специалист"; %>
<%= HtmlControls.Image("http://chart.apis.google.com/chart?" +
                       "cht=bhs&chd=t:{0}&chs=600x{2}&chdl=Набранный балл|{1}|Максимальный балл&chco=599AF1|FFC6A5|FFFF42|DEF3BD|DEBDDE&chds=a&chxt=x"
                           .FormatWith(points.Select(x => x.ToString()).JoinWith(","),
                               points.Count() == 3 ? "Проходной балл" : names, points.Count() == 3 ? 120 : 200)) %>
<br />
<% } %>
<% if(Model.UserTest.IsCoursePlanned) return;%>
<% if (Model.Stats.Count > 1) { %>
<h3>По модулям</h3>
<% foreach (var module in Model.Stats) { %>
<%= Model.Modules.FirstOrDefault(x => x.Id == module.Key).GetOrDefault(x => x.Name) + " - " %><strong>
<%= (module.Value.R * 100) / (module.Value.R + module.Value.W) %>%</strong> <br />
<% } %>
<% } %>
<% if (Model.RecCourses.Any() && (Model.IsOwned || Model.UserTest.IsPrerequisite)) { %>
<div class="attention2">
<p>
<% if (Model.UserTest.IsPass) { %>
<% if (!Model.IsEnglish) { %>
Поздравляем, Вы успешно сдали тест!
<% } %>
<% if (Model.UserTest.IsPrerequisite) { %>
    
Ваших знаний достаточно для прохождения <%= Model.IsEnglish ? "" : "выбранного Вами " %> курса:
<% } else { %>


<% if (Model.UserTest.Test.Certified) { %>
Вы хорошо ориентируетесь в этой области и можете получить сертификат, подтверждающий, что Ваши знания по предмету тестирования находятся на высоком уровне.  
<% } %>
Для дальнейшего повышения Вашей квалификации, мы рекомендуем Вам пройти следующие курсы в Центре «Специалист»:
<% } %>
<% } else { %>
<% if (Model.UserTest.IsPrerequisite) { %>
К сожалению, Ваших знаний недостаточно для успешного обучения на выбранном Вами курсе. Получить необходимые знания Вы сможете на следующих курсах в Центре «Специалист»
<% } else { %>
К сожалению, Ваших знаний недостаточно для успешного прохождения теста. Получить необходимые знания Вы сможете на следующих курсах в Центре «Специалист»:
<% } %>
<% } %>
</p>
<%= Htmls.DefaultList(Model.RecCourses.Select(s => Html.CourseLink(s))) %>
</div>
<% } %>

<% if (Model.UserTest.Test.IsEnglish && (Model.IsOwned || Model.UserTest.IsPrerequisite)) { %>
    <%= Url.Link<PageController>(c => c.EnglishOrder(Model.UserTest.Id), "Отправить заявку на обучение").Class("as-button") %>
<% } %>



<%--<% if(Model.IsOwned && !Model.UserTest.Test.CompanyId.HasValue){ %>--%>
<%----%>
<%--    <%= Url.Link<PageController>(c => c.TestResponse("Комментарии о тесте " + (--%>
<%--    	Model.CourseName ?? Model.UserTest.Test.Name)), "Отправить комментарии о тесте").Style("margin-top:30px;").Class("as-button") %>--%>
<%--<% } %>--%>
<%----%>
	
<%= H.JQuery("$('.as-button').button();") %>
<script type="text/javascript">
    var _gaq = _gaq || [];
    <% if (Model.UserTest.IsPass) { %>
    _gaq.push(['_setCustomVar',2,'Pretest','Passed',1]);
    <% } else { %>
    _gaq.push(['_setCustomVar',2,'Pretest','Failed',1]);
    <% } %>

</script>