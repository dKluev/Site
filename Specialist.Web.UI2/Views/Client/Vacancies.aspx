<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<VacanciesVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p><h2>Добавить вакансию</h2><br/>
Данная форма заполняется работодателем!
</p>
<p>

   <% var user = (User)HttpContext.Current.Session["CurrentUserSessionKey"];
      if (HttpContext.Current.User.Identity.IsAuthenticated && user!=null && user.IsCompany)
      { %> 
    <%= Htmls.BorderBegin(Model.Result)%>
    <%= Html.ValidationSummary()%>
    <% using (Html.BeginForm())
       { %>
<h3>Требования</h3>
     <table class="table_form">
     <tr>
     <td class="name">* Должность </td>
     <td class="field">
     <%= Html.TextBox("YourPosition", "", new { @class = "course-name", size = 55 })%></td>
    </tr>
    <tr>
     <td class="name"> Возраст </td>
     <td class="field">
     от <%= Html.TextBox("YourAgeSince")%> до <%= Html.TextBox("YourAgeTill")%></td>
    </tr>
    <tr>
     <td class="name">* Образование </td>
     <td class="field">
     <%= Html.DropDownList("YourEducation", new[] {
        new SelectListItem { Text = "Не имеет значения" , Value = "Не имеет значения"},
        new SelectListItem { Text = "Высшее" , Value = "Высшее"},
        new SelectListItem { Text = "Неполное высшее" , Value = "Неполное высшее"},
        new SelectListItem { Text = "Среднее" , Value = "Среднее"},
        new SelectListItem { Text = "Среднее специальное" , Value = "Среднее специальное"},
        new SelectListItem { Text = "Студент" , Value = "Студент"}
             }, "-выберите из списка-")%></td>
    </tr>
     <tr>
     <td class="name">Опыт работы </td>
     <td class="field">
     <%= Html.DropDownList("YourExperience", new[] {
        new SelectListItem { Text = "Не обязателен" , Value = "Не обязателен"},
        new SelectListItem { Text = "Желателен" , Value = "Желателен"},
        new SelectListItem { Text = "Обязятелен" , Value = "Обязятелен"},
        })%></td>
    </tr>    
     <tr>
     <td class="name">Пол </td>
     <td class="field">
     <%= Html.DropDownList("YourSex", new[] {
        new SelectListItem { Text = "Не имеет значения" , Value = "Не имеет значения"},
        new SelectListItem { Text = "Муж." , Value = "Муж."},
        new SelectListItem { Text = "Жен." , Value = "Жен."},
        })%></td>
    </tr>    
     <tr>
     <td class="name">Занятость </td>
     <td class="field">
     <%= Html.DropDownList("YourBusy", new[] {
        new SelectListItem { Text = "Любая" , Value = "Любая"},
        new SelectListItem { Text = "Полная" , Value = "Полная"},
        new SelectListItem { Text = "По совместительству" , Value = "По совместительству"},
        new SelectListItem { Text = "Частичная" , Value = "Частичная"},
        })%></td>
    </tr> 
     <tr>
     <td class="name">График работы </td>
     <td class="field">
     <%= Html.DropDownList("YourSchedule", new[] {
        new SelectListItem { Text = "Любой" , Value = "Любой"},
        new SelectListItem { Text = "Полный день" , Value = "Полный день"},
        new SelectListItem { Text = "Свободный график" , Value = "Свободный график"},
        })%></td>
    </tr> 
    <tr>    
     <td class="name">Иностранный язык </td>
     <td class="field">
     <%= Html.DropDownList("YourLang", new[] {
        new SelectListItem { Text = "Не требуется" , Value = "Не требуется"},
        new SelectListItem { Text = "Английский" , Value = "Английский"},
        new SelectListItem { Text = "Французский" , Value = "Французский"},
        new SelectListItem { Text = "Немецкий" , Value = "Немецкий"},
        })%> уровень 
     <%= Html.DropDownList("YourLangLevel", new[] {
        new SelectListItem { Text = "Не имеет значения" , Value = "Не имеет значения"},
        new SelectListItem { Text = "Свободный" , Value = "Свободный"},
        new SelectListItem { Text = "Разговорный" , Value = "Разговорный"},
        new SelectListItem { Text = "Чтение и перевод со слов." , Value = "Чтение и перевод со словарем"},
        new SelectListItem { Text = "Базовый" , Value = "Базовый"},
        })%></td>    
    </tr>   
    <tr>
     <td class="name">* Заработная плата от </td>
     <td class="field">
     <%= Html.TextBox("YourProfit")%>
     <%= Html.DropDownList("YourCurrency", new[] {
     new SelectListItem { Text = "Рублей" , Value = "Рублей"},
     new SelectListItem { Text = "у.е." , Value = "у.е."},
     })%> в месяц
     </td>
    </tr>
     <tr>
     <td class="name">* Город </td>
     <td class="field">
     <%= Html.DropDownList("YourCity", new[] {
        new SelectListItem { Text = "Москва" , Value = "Москва"},
        new SelectListItem { Text = "Санкт-Петербург" , Value = "Санкт-Петербург"},
        new SelectListItem { Text = "Ростов-на Дону" , Value = "Ростов-на Дону"},
        new SelectListItem { Text = "Казань" , Value = "Казань"},
        }, "-выберите из списка-")%></td>
    </tr>    
     <tr>
     <td class="name">Ближайшая станция метро </td>
     <td class="field">
     <%= Html.DropDownList("YourMetro", new[] {
        new SelectListItem { Text =  "-выберите из списка-" , Value = "не выбрана"},
        new SelectListItem { Text = "Авиамоторная" , Value = "Авиамоторная"},
        new SelectListItem { Text = "Автозаводская" , Value = "Автозаводская"},
        new SelectListItem { Text = "Академическая" , Value = "Академическая"},
        new SelectListItem { Text = "Алексеевская" , Value = "Алексеевская"},
        new SelectListItem { Text = "Алтуфьево" , Value = "Алтуфьево"},
        new SelectListItem { Text = "Аннино" , Value = "Аннино"},
        new SelectListItem { Text = "Арбатская" , Value = "Арбатская"},
        new SelectListItem { Text = "Аэропорт" , Value = "Аэропорт"},
        new SelectListItem { Text = "Бабушкинская" , Value = "Бабушкинская"},
        new SelectListItem { Text = "Багратионовская" , Value = "Багратионовская"},
        new SelectListItem { Text = "Баррикадная" , Value = "Баррикадная"},
        new SelectListItem { Text = "Бауманская" , Value = "Бауманская"},
        new SelectListItem { Text = "Беговая" , Value = "Беговая"},
        new SelectListItem { Text = "Белорусская" , Value = "Белорусская"},
        new SelectListItem { Text = "Беляево" , Value = "Беляево"},
        new SelectListItem { Text = "Бибирево" , Value = "Бибирево"},
        new SelectListItem { Text = "Библиотека им. Ленина" , Value = "Библиотека им. Ленина"},
        new SelectListItem { Text = "Битцевский парк" , Value = "Битцевский парк"},
        new SelectListItem { Text = "Боровицкая" , Value = "Боровицкая"},
        new SelectListItem { Text = "Ботанический Сад" , Value = "Ботанический Сад"},
        new SelectListItem { Text = "Братиславская" , Value = "Братиславская"},
        new SelectListItem { Text = "Бульвар Дмитрия Донского" , Value = "Бульвар Дмитрия Донского"},
        new SelectListItem { Text = "Бунинская аллея" , Value = "Бунинская аллея"},
        new SelectListItem { Text = "Варшавская" , Value = "Варшавская"},
        new SelectListItem { Text = "ВДНХ" , Value = "ВДНХ"},
        new SelectListItem { Text = "Владыкино" , Value = "Владыкино"},
        new SelectListItem { Text = "Водный стадион" , Value = "Водный стадион"},
        new SelectListItem { Text = "Войковская" , Value = "Войковская"},
        new SelectListItem { Text = "Волгоградский проспект" , Value = "Волгоградский проспект"},
        new SelectListItem { Text = "Волжская" , Value = "Волжская"},
        new SelectListItem { Text = "Воробьевы Горы" , Value = "Воробьевы Горы"},
        new SelectListItem { Text = "Выхино" , Value = "Выхино"},
        new SelectListItem { Text = "Динамо" , Value = "Динамо"},
        new SelectListItem { Text = "Дмитровская" , Value = "Дмитровская"},
        new SelectListItem { Text = "Добрынинская" , Value = "Добрынинская"},
        new SelectListItem { Text = "Домодедовская" , Value = "Домодедовская"},
        new SelectListItem { Text = "Дубровка" , Value = "Дубровка"},
        new SelectListItem { Text = "Измайловская" , Value = "Измайловская"},
        new SelectListItem { Text = "Измайловский парк" , Value = "Измайловский парк"},
        new SelectListItem { Text = "Калужская" , Value = "Калужская"},
        new SelectListItem { Text = "Кантемировская" , Value = "Кантемировская"},
        new SelectListItem { Text = "Каховская" , Value = "Каховская"},
        new SelectListItem { Text = "Каширская" , Value = "Каширская"},
        new SelectListItem { Text = "Киевская" , Value = "Киевская"},
        new SelectListItem { Text = "Китай-город" , Value = "Китай-город"},
        new SelectListItem { Text = "Кожуховская" , Value = "Кожуховская"},
        new SelectListItem { Text = "Коломенская" , Value = "Коломенская"},
        new SelectListItem { Text = "Комсомольская" , Value = "Комсомольская"},
        new SelectListItem { Text = "Коньково" , Value = "Коньково"},
        new SelectListItem { Text = "Красногвардейская" , Value = "Красногвардейская"},
        new SelectListItem { Text = "Краснопресненская" , Value = "Краснопресненская"},
        new SelectListItem { Text = "Красносельская" , Value = "Красносельская"},
        new SelectListItem { Text = "Красные Ворота" , Value = "Красные Ворота"},
        new SelectListItem { Text = "Крестьянская застава" , Value = "Крестьянская застава"},
        new SelectListItem { Text = "Кропоткинская" , Value = "Кропоткинская"},
        new SelectListItem { Text = "Крылатское	", Value = "Крылатское"},
        new SelectListItem { Text = "Кузнецкий мост" , Value = "Кузнецкий мост"},
        new SelectListItem { Text = "Кузьминки" , Value = "Кузьминки"},
        new SelectListItem { Text = "Кунцевская" , Value = "Кунцевская"},
        new SelectListItem { Text = "Курская" , Value = "Курская"},
        new SelectListItem { Text = "Кутузовская" , Value = "Кутузовская"},
        new SelectListItem { Text = "Ленинский проспект" , Value = "Ленинский проспект"},
        new SelectListItem { Text = "Лубянка" , Value = "Лубянка"},
        new SelectListItem { Text = "Люблино" , Value = "Люблино"},
        new SelectListItem { Text = "Марксистская" , Value = "Марксистская"},
        new SelectListItem { Text = "Марьино" , Value = "Марьино"},
        new SelectListItem { Text = "Маяковская" , Value = "Маяковская"},
        new SelectListItem { Text = "Медведково" , Value = "Медведково"},
        new SelectListItem { Text = "Менделеевская" , Value = "Менделеевская"},
        new SelectListItem { Text = "Молодежная" , Value = "Молодежная"},
        new SelectListItem { Text = "Нагатинская" , Value = "Нагатинская"},
        new SelectListItem { Text = "Нагорная" , Value = "Нагорная"},
        new SelectListItem { Text = "Нахимовский пр-т" , Value = "Нахимовский пр-т"},
        new SelectListItem { Text = "Новогиреево" , Value = "Новогиреево"},
        new SelectListItem { Text = "Новокузнецкая" , Value = "Новокузнецкая"},
        new SelectListItem { Text = "Новослободская" , Value = "Новослободская"},
        new SelectListItem { Text = "Новые Черемушки" , Value = "Новые Черемушки"},
        new SelectListItem { Text = "Октябрьская" , Value = "	Октябрьская"},
        new SelectListItem { Text = "Октябрьское поле" , Value = "Октябрьское поле"},
        new SelectListItem { Text = "Орехово" , Value = "Орехово"},
        new SelectListItem { Text = "Отрадное" , Value = "Отрадное"},
        new SelectListItem { Text = "Охотный Ряд" , Value = "Охотный Ряд"},
        new SelectListItem { Text = "Павелецкая" , Value = "Павелецкая"},
        new SelectListItem { Text = "Парк Культуры" , Value = "Парк Культуры"},
        new SelectListItem { Text = "Парк Победы" , Value = "Парк Победы"},
        new SelectListItem { Text = "Первомайская" , Value = "Первомайская"},
        new SelectListItem { Text = "Перово" , Value = "Перово"},
        new SelectListItem { Text = "Петровско-Разумовская" , Value = "Петровско-Разумовская"},
        new SelectListItem { Text = "Печатники" , Value = "Печатники"},
        new SelectListItem { Text = "Пионерская" , Value = "Пионерская"},
        new SelectListItem { Text = "Пл. Ильича" , Value = "Пл. Ильича"},
        new SelectListItem { Text = "Пл. Революции" , Value = "Пл. Революции"},
        new SelectListItem { Text = "Планерная" , Value = "Планерная"},
        new SelectListItem { Text = "Полежаевская" , Value = "Полежаевская"},
        new SelectListItem { Text = "Полянка" , Value = "Полянка"},
        new SelectListItem { Text = "Пражская" , Value = "Пражская"},
        new SelectListItem { Text = "Преображенская пл." , Value = "Преображенская пл."},
        new SelectListItem { Text = "Пролетарская" , Value = "Пролетарская"},
        new SelectListItem { Text = "Проспект Вернадского" , Value = "Проспект Вернадского"},
        new SelectListItem { Text = "Проспект Мира" , Value = "Проспект Мира"},
        new SelectListItem { Text = "Профсоюзная" , Value = "Профсоюзная"},
        new SelectListItem { Text = "Пушкинская" , Value = "Пушкинская"},
        new SelectListItem { Text = "Речной вокзал" , Value = "Речной вокзал"},
        new SelectListItem { Text = "Рижская" , Value = "Рижская"},
        new SelectListItem { Text = "Римская" , Value = "Римская"},
        new SelectListItem { Text = "Рязанский проспект" , Value = "Рязанский проспект"},
        new SelectListItem { Text = "Савеловская" , Value = "Савеловская"},
        new SelectListItem { Text = "Свиблово" , Value = "Свиблово"},
        new SelectListItem { Text = "Севастопольская" , Value = "Севастопольская"},
        new SelectListItem { Text = "Семеновская" , Value = "Семеновская"},
        new SelectListItem { Text = "Серпуховская" , Value = "Серпуховская"},
        new SelectListItem { Text = "Смоленская" , Value = "Смоленская"},
        new SelectListItem { Text = "Сокол" , Value = "Сокол"},
        new SelectListItem { Text = "Сокольники" , Value = "Сокольники"},
        new SelectListItem { Text = "Спортивная" , Value = "Спортивная"},
        new SelectListItem { Text = "Студенческая" , Value = "Студенческая"},
        new SelectListItem { Text = "Сухаревская" , Value = "Сухаревская"},
        new SelectListItem { Text = "Сходненская" , Value = "Сходненская"},
        new SelectListItem { Text = "Таганская" , Value = "Таганская"},
        new SelectListItem { Text = "Тверская" , Value = "Тверская"},
        new SelectListItem { Text = "Театральная" , Value = "Театральная"},
        new SelectListItem { Text = "Текстильщики" , Value = "Текстильщики"},
        new SelectListItem { Text = "Теплый Стан" , Value = "Теплый Стан"},
        new SelectListItem { Text = "Тимирязевская" , Value = "Тимирязевская"},
        new SelectListItem { Text = "Третьяковская" , Value = "Третьяковская"},
        new SelectListItem { Text = "Тульская" , Value = "Тульская"},
        new SelectListItem { Text = "Тургеневская" , Value = "Тургеневская"},
        new SelectListItem { Text = "Тушинская" , Value = "Тушинская"},
        new SelectListItem { Text = "ул. Адмирала Ушакова" , Value = "ул. Адмирала Ушакова"},
        new SelectListItem { Text = "Ул. Академика Янгеля" , Value = "Ул. Академика Янгеля"},
        new SelectListItem { Text = "ул. Горчакова" , Value = "ул. Горчакова"},
        new SelectListItem { Text = "Ул. Подбельского" , Value = "Ул. Подбельского"},
        new SelectListItem { Text = "ул. Старокачаловская" , Value = "ул. Старокачаловская"},
        new SelectListItem { Text = "Улица 1905 года" , Value = "Улица 1905 года"},
        new SelectListItem { Text = "Университет" , Value = "Университет"},
        new SelectListItem { Text = "Филевский парк" , Value = "Филевский парк"},
        new SelectListItem { Text = "Фили" , Value = "Фили"},
        new SelectListItem { Text = "Фрунзенская" , Value = "Фрунзенская"},
        new SelectListItem { Text = "Царицыно" , Value = "Царицыно"},
        new SelectListItem { Text = "Цветной Бульвар" , Value = "Цветной Бульвар"},
        new SelectListItem { Text = "Черкизовская" , Value = "Черкизовская"},
        new SelectListItem { Text = "Чертановская" , Value = "Чертановская"},
        new SelectListItem { Text = "Чеховская" , Value = "Чеховская"},
        new SelectListItem { Text = "Чистые Пруды" , Value = "Чистые Пруды"},
        new SelectListItem { Text = "Чкаловская" , Value = "Чкаловская"},
        new SelectListItem { Text = "Шаболовская" , Value = "Шаболовская"},
        new SelectListItem { Text = "Шоссе Энтузиастов" , Value = "Шоссе Энтузиастов"},
        new SelectListItem { Text = "Щелковская" , Value = "Щелковская"},
        new SelectListItem { Text = "Щукинская" , Value = "Щукинская"},
        new SelectListItem { Text = "Электрозаводская" , Value = "Электрозаводская"},
        new SelectListItem { Text = "Юго-Западная" , Value = "Юго-Западная"},
        new SelectListItem { Text = "Южная" , Value = "Южная"},
        new SelectListItem { Text = "Ясенево" , Value = "Ясенево"},    
        })%></td>
     </tr>
    </table>
    <table class="table_form">
    <tr>
     <td class="name">* Текст объявления </td><td class="field"></td>
    </tr>
    </table>
    <table>
    <tr>
    <td style="width:20%"></td>
    <td>
     <%= Html.TextArea("YourText","", 5, 80, 50)%></td>
     </tr>
    </table>
    <p>* Выберите разделы для публикации:</p>
    <table>
    <tr>
    <td>
    <%= Html.CheckBox("s1", false)%>
     <label for="s1"> Веб-технологии</label>
    </td>
    <td class="field">
    <%= Html.CheckBox("s2", false)%> 
    <label for="s2"> Системное администрирование</label>
    </td>
    </tr>
    <tr>
    <td>
    <%= Html.CheckBox("s3", false)%>
     <label for="s3"> Программирование</label>
    </td>
    <td class="field">
    <%= Html.CheckBox("s4", false)%> 
    <label for="s4"> Бухгалтерия / Финансы</label>
    </td>
    </tr>
    <tr>
    <td>
    <%= Html.CheckBox("s5", false)%>
     <label for="s5"> Дизайн, графика, верстка, 3D</label>
    </td>
    <td class="field">
    <%= Html.CheckBox("s6", false)%> 
    <label for="s6"> Кадры/управление персоналом</label>
    </td>
    </tr>
    <tr>
    <td>
    <%= Html.CheckBox("s7", false)%>
     <label for="s7"> Административный персонал</label>
    </td>
    <td class="field">
    <%= Html.CheckBox("s8", false)%> 
    <label for="s8"> Проектирование</label>
    </td>
    </tr>
    <tr>
    <td>
    <%= Html.CheckBox("s9", false)%>
     <label for="s9"> Техническое обслуживание ПК, HelpDesk</label>
    </td>
    <td class="field">
    <%= Html.CheckBox("s10", false)%> 
    <label for="s10"> Складское хозяйство / Логистика / ВЭД</label>
    </td>
    </tr>
    <tr>
    <td>
    <%= Html.CheckBox("s11", false)%>
     <label for="s11"> Продажи / Закупки</label>
    </td>
    <td class="field">
    <%= Html.CheckBox("s12", false)%> 
    <label for="s12"> Информационная безопасность</label>
    </td>
    </tr>
    <tr>
    <td>
    <%= Html.CheckBox("s13", false)%>
     <label for="s13"> Маркетинг / Реклама / PR</label>
    </td>
    <td class="field">
    <%= Html.CheckBox("s14", false)%> 
    <label for="s14"> Разное</label>
    </td>
    </tr>
    </table>
    <table class="table_form">
     <tr>
     <td class="name">Срок публикации </td>
     <td class="field">
     <%= Html.DropDownList("YourPeriod", new[] {
        new SelectListItem { Text = "1 месяц" , Value = "1 месяц"},
        new SelectListItem { Text = "2 месяца" , Value = "2 месяца"},
        new SelectListItem { Text = "3 месяца" , Value = "3 месяца"},
        })%></td>
    </tr>    
    </table>
    <h3>Контактная информация</h3>
    <table class="table_form">
     <tr>
     <td class="name">* Компания </td>
     <td class="field">
     <%= Html.TextBox("YourCompany", "", new { @class = "course-name", size = 55 })%></td>
    </tr>
     <tr>
     <td class="name">* Фамилия И.О. </td>
     <td class="field">
     <%= Html.TextBox("YourFIO", "", new { @class = "course-name", size = 55 })%></td>
    </tr>
     <tr>
     <td class="name">* Должность </td>
     <td class="field">
     <%= Html.TextBox("YourPos", "", new { @class = "course-name", size = 55 })%></td>
    </tr>
     <tr>
     <td class="name">* Телефон </td>
     <td class="field">
     <%= Html.TextBox("YourTel", "", new { @class = "course-name", size = 55 })%></td>
    </tr>
     <tr>
     <td class="name">* E-mail </td>
     <td class="field">
     <%= Html.TextBox("YourEmail", "", new { @class = "course-name", size = 55 })%></td>
    </tr>

    </table>
    <div>Разделы помеченные * обязательны для заполнения</div>
	<%= Htmls.Submit("send") %>
    <% } %>
    <%= Htmls.BorderEnd%>
     <%}
      else
      {%>
   <p>
     Сервис "Добавить вакансию" доступен только зарегистрированным пользователям-организациям.<br />
     <%= Html.ActionLink<AccountController>(c => c.LogOn(""), "Аутентификация на сайте") %>
     </p>
    <% } %> 
</asp:Content>
