<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Profile.ViewModel.RealSpecialistVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Services.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<% Html.RenderPartial(Views.Profile.ClabCard, Model.Student ?? new Student()); %>
    

<br/>
<% var card = Model.Student.GetOrDefault(x => x.Card); %>
<% if (card != null) { %>
    

<% var color =  card.ClabCardColor_TC; %>
    <div class="clear"></div>
    <br/> <%= H.Img(CdnFiles.FullUrls.ImageBadgeRealSpecialist + color + ".png").Size(100,100).FloatLeft() %><br><br>
    <script src="https://backpack.openbadges.org/issuer.js"></script>
    <button id="openbadge-button" style="margin-bottom: 10px;">Получите значок OpenBadge!</button>
    <br/><div class="clear"></div>

    <script type="text/javascript">
        $(function() {
            $("#openbadge-button").click(function() {
                OpenBadges.issue(["<%= CommonConst.SiteRoot + 
                        Url.Badge().Urls.UserRealSpecialist(Model.User.UserID) %>"],
                    function (errors, successes) {  });
            });
        })
    </script>

<% if (color != ClabCardColors.Diamond) { %>
Вы являетесь членом элитного клуба <%= SimpleLinks.RealSpecialist("«Настоящий Специалист» и карты «Настоящий Специалист»") %>.
Уже сейчас Вы можете воспользоваться многочисленными преимуществами, которые предоставляет Вам членство в клубе «Настоящий Специалист» и обладание картой клуба:
<% } else { %>
Поздравляем Вас с достижением высшего статуса в элитном клубе «Настоящий Специалист» и получением карты «Бриллиантового Специалист»! Ваше стремление к постоянному совершенствованию, повышению квалификации, получению новых знаний и навыков заслуживает глубочайшего уважения! Мы гордимся, что такой специалист, как Вы – выпускник нашего Центра!
Теперь для Вас доступны все многочисленные привилегии и бонусы элитного клуба «Настоящий Специалист: 
<% } %>

<h3><%= H.Anchor(SimplePages.FullUrls.RealSpecialist, "Описание всех причитающихся скидок и бонусов") %></h3>

<% if (color != ClabCardColors.Diamond) { %>
<p>Вы также можете предложить Вашим близким и друзьям воспользоваться 7% скидкой по Вашей карте! За каждого нового приглашенного слушателя Центра Вы получите дополнительные 0, 5 курса в Ваш накопительный фонд для достижения нового уровня и получения новой карты.
</p>
<p>
Сообщество Настоящих Специалистов – мир уникальных привилегий для тех, кто по-настоящему ценит знания, кто нацелен на профессиональный рост! Узнайте подробнее о <%= H.Anchor(MainMenu.Urls.SpecialOffers,"скидках и бонусах") %>, которые предоставляют членам клуба «Настоящий Специалист» наши партнеры!
</p>
<% } %>
<p>
Вы также можете распечатать <%= Url.Link<GraduateController>(c => c.RealSpecialist(), "сертификат «Настоящего Специалиста»") %> , подтвердив таким образом, свою принадлежность к  элитному клубу Центра.  
</p>
<p>
Сегодня технологии меняются столь стремительно, что обучение превратилось в непрерывный процесс.  Центр «Специалист» как никто другой понимает это.  Мы  надеемся  увидеть Вас в числе своих постоянных слушателей и в дальнейшем, а также  будем благодарны за любые отзывы и рекомендации по улучшению качества обучения и обслуживания в нашем Центре.
</p>
<% }else{ %>
<p>
Не останавливайтесь на достигнутом! Заканчивая курсы, кроме новых знаний и навыков, которые приведут Вас к новым вершинам успеха, Вы получите карту элитного клуба «Настоящий Специалист» и доступ к <%= SimpleLinks.RealSpecialist("многочисленным бонусам и привилегиям") %>!
</p>
<% } %>



</asp:Content>
