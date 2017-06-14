<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MainPageVM>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
    <%= Htmls2.ChamBegin(true) %>
    <%= Htmls.HtmlBlock(HtmlBlocks.HomeLeft) %>
<%----%>
<%----%>
<%--	<%= Htmls2.Menu2(HtmlControls.Anchor(MainMenu.Urls.MainCourses ,--%>
<%--                                       "����������� ��������")) %>--%>
<%--    <div class="block_chamfered_in v_orientation_teaching">--%>
<%--        --%>
<%--	      <% var sections = Model.Sections.ToList(); %>--%>
<%--	      <% var index = sections.FindIndex(x => x.Section_ID == Sections.Soul); %>--%>
<%--          <% if (index > 0) sections.Insert(index + 1, new Section{ --%>
<%--                 UrlName = "soul",--%>
<%--                 Name = "���������� �����"}); { %>--%>
<%--          <% } %>--%>
<%----%>
<%--        <% Func<Section, string> getLink = s => {--%>
<%--               var link = Html.SectionLink(s);--%>
<%--               if (s.UrlName == "soul") {--%>
<%--                   var id = s.Section_ID > 0 ? "soul-course-test-link" : "creative-course-test-link";--%>
<%--                   link = StringUtils.AddAttr(link, "id", id);--%>
<%--               }--%>
<%--               return link;--%>
<%--           }; %>--%>
<%--        <% var links = sections.Select(x => getLink(x) + (x.IsSeparator ? "<hr style='color:#999999;'/>" : "")).ToList(); %>--%>
<%--        <% links.Insert(1, "<a href='/vendor/microsoft'>����� � ������������ Microsoft</a>"); %>--%>
<%----%>
<%--        <%= Htmls.HtmlBlock(HtmlBlocks.HomeLeft) %>--%>
<%--        <p>--%>
<%--            <%= HtmlControls.Anchor(MainMenu.Urls.MainCourses ,--%>
<%--                                       "��� �����������").Class("all").Style("color:red;") %></p>--%>
<%--    </div>--%>
<%----%>
<%--<br />--%>
<%----%>
<%--    <% Func<IEnumerable<object>, string, object> list = (x,url) => --%>
<%--           Htmls2.MarkArrow(x.ToList().AddFluent(H.Anchor(url, "...")));%>--%>
<%--	<%= Htmls2.Menu2(HtmlControls.Anchor(MainMenu.Urls.MainCourses,--%>
<%--             "�������������� ����� � ������������").ToString().LinkAddFragmentCyrillic("�������")) %>--%>
<%--    <div class="block_chamfered_in v_authorized_courses">--%>
<%--            <% var allVendord = HtmlControls.Anchor(MainMenu.Urls.MainCourses,--%>
<%--                   "��� �������").Class("all").Style("color:red;").ToString().LinkAddFragmentCyrillic("�������"); %>--%>
<%--		<%= Htmls2.MarkArrow(Model.Vendors.Select(x => Html.GetLinkWithoutCoursesPrefixFor(x))) %>--%>
<%--        <p>--%>
<%--            <%= allVendord %>--%>
<%--        </p>--%>
<%--    </div>--%>
<%--	<%= Htmls2.Menu2(HtmlControls.Anchor(MainMenu.Urls.MainCourses,--%>
<%--             "���� ���������").ToString()--%>
<%--                         .LinkAddFragmentCyrillic("���������")) %>--%>
<%--    <div class="block_chamfered_in v_specialization">--%>
<%--            <% var allProfessions = HtmlControls.Anchor(MainMenu.Urls.MainCourses,--%>
<%--                   "��� ���������").Class("all").Style("color:red;").ToString()--%>
<%--                   .LinkAddFragmentCyrillic("���������"); %>--%>
<%--		<%= list(Model.Professions.Select(x =>--%>
<%--			Html.GetLinkWithoutCoursesPrefixFor(x)), StringUtils.GetHref(allProfessions)) %>--%>
<%--        <p>--%>
<%--            <%= allProfessions %>--%>
<%--        </p>--%>
<%----%>
<%--    </div>--%>
<%--	<%= Htmls2.Menu2(HtmlControls.Anchor(MainMenu.Urls.MainCourses,--%>
<%--             "����� �� ���������").ToString()--%>
<%--                         .LinkAddFragmentCyrillic("��������")) %>--%>
<%--    <div class="block_chamfered_in v_specialization">--%>
<%--            <% var allProducts = HtmlControls.Anchor(MainMenu.Urls.MainCourses,--%>
<%--                   "��� ��������").Class("all").Style("color:red;").ToString()--%>
<%--                   .LinkAddFragmentCyrillic("��������"); %>--%>
<%--		<%= list(Model.Products.Select(x =>--%>
<%--			Html.GetLinkWithoutCoursesPrefixFor(x)), StringUtils.GetHref(allProducts)) %>--%>
<%--        <p>--%>
<%--            <%= allProducts %>--%>
<%--        </p>--%>
<%--    </div>--%>
<%--	<%= Htmls2.Menu2(HtmlControls.Anchor(MainMenu.Urls.MainCourses,--%>
<%--             "����� �� �����������").ToString()--%>
<%--                         .LinkAddFragmentCyrillic("����������")) %>--%>
<%--    <div class="block_chamfered_in v_specialization">--%>
<%--            <% var allTerms = HtmlControls.Anchor(MainMenu.Urls.MainCourses,--%>
<%--                   "��� ����������").Class("all").Style("color:red;").ToString()--%>
<%--                   .LinkAddFragmentCyrillic("������� � ����������");%>--%>
<%--		<%= list(Model.SiteTerms.Select(x =>--%>
<%--			Html.GetLinkWithoutCoursesPrefixFor(x)), StringUtils.GetHref(allTerms)) %>--%>
<%--        <p>--%>
<%--            <%= allTerms %>--%>
<%--        </p>--%>
<%--    </div>--%>


    <%= Htmls2.BlockEnd() %>
