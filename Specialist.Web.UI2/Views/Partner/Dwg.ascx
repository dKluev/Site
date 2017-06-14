<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Partners.ViewModels.DwgVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%= Images.Common("dwg/specialist_logo.gif").Attr(new {alt="specialist", border=0, 
	style="margin-right:10px;margin-bottom:5px;float:left;"}) %>

<p>
	По результатам авторитетных рейтингов, Центр Компьютерного Обучения при МГТУ им. Н.Э.Баумана является учебным центром N1 в области информационных технологий. 
</p>
<p>
	Центр «Специалист» — <a href="http://www.specialist.ru/center/about-center/authorizations">авторизованный учебный центр по <strong>курсам  Autodesk</strong></a> c 2001 года, <a href="http://www.specialist.ru/center/about-center/authorizations">лучший учебный центр ATC 2006 и 2008 года</a>. За 9 лет  успешного сотрудничества с компанией Autodesk, «Специалист» стал крупнейшим  учебным центром России и вторым учебным центром по объемам обучения в регионе  EMEA. Сегодня каждый третий <a href="http://www.specialist.ru/center/about-center/documents-off#Autodesk">сертификат</a> после <strong>обучения Autodesk</strong> в России  выдается в «Специалисте».
</p>
<p>
На сегодняшний день Центр является также является крупнейшим авторизованным учебным центром SolidWorks, Graphisoft, АСКОН, Adobe и других компаний-вендоров. 
</p>
<p> 
<%= Images.Image("/Vendor/autodesk.jpg").Alt("autodesk")
	.Style("border-width:0px;margin-right:10px;margin-bottom:10px;float:left;") %>

В Центре читаются курсы  <a href="http://www.specialist.ru/product/autocad-courses">AutoCAD</a>, <a href="http://www.specialist.ru/product/inventor-courses">Inventor</a>, <a href="http://www.specialist.ru/product/3ds-max-courses">3ds max</a>, <a href="http://www.specialist.ru/product/autocad-civil-3d-courses">Civil 3D</a>, <a href="http://www.specialist.ru/product/autocad-mep-courses">MEP</a> и другие! Занятия проводят известные  <a href="http://www.specialist.ru/center/sectiontrainers/cad">сертифицированные инструкторы компании Autodesk</a>, обладающие многолетним опытом автоматизации проектирования на крупнейших промышленных предприятиях и в строительных организациях.</p>
<br />
<br />
	<% foreach (var course in Model.Courses){ %>
	<h2> <%= course.V1 %></h2>
	<% Html.RenderPartial(PartialViewNames.DwgCourseTable, course.V2); %>
	<% } %>
<h2>
	Преимущества сертифицированных курсов</h2>
<ul>
	<li>Полностью отвечает западным требованиям к специалистам САПР</li>
	<li>Курсы проводятся сертифицированными инструкторами </li>
	<li>Для проведения курсов используются фирменные учебные пособия </li>
	<li>Международный сертификат специалиста </li>
</ul>
<h2>
	Координаты</h2>
<p>
	Адрес: г.Москва, м. Бауманская, Госпитальный пер. д. 4/6, 2-ой этаж<br>
	Телефон. (095) 232-3216<br/>
	Интернет: <a href="http://www.specialist.ru/" target="_blank">http://www.specialist.ru</a><br/>
	E-mail: <a href="mailto:info@specialist.ru">info@specialist.ru</a><br/>
</p>
