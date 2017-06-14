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
    <button id="openbadge-button" style="margin-bottom: 10px;">�������� ������ OpenBadge!</button>
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
�� ��������� ������ �������� ����� <%= SimpleLinks.RealSpecialist("���������� ���������� � ����� ���������� ����������") %>.
��� ������ �� ������ ��������������� ��������������� ��������������, ������� ������������� ��� �������� � ����� ���������� ���������� � ��������� ������ �����:
<% } else { %>
����������� ��� � ����������� ������� ������� � ������� ����� ���������� ���������� � ���������� ����� ��������������� ����������! ���� ���������� � ����������� �����������������, ��������� ������������, ��������� ����� ������ � ������� ����������� ������������ ��������! �� ��������, ��� ����� ����������, ��� �� � ��������� ������ ������!
������ ��� ��� �������� ��� �������������� ���������� � ������ �������� ����� ���������� ����������: 
<% } %>

<h3><%= H.Anchor(SimplePages.FullUrls.RealSpecialist, "�������� ���� ������������� ������ � �������") %></h3>

<% if (color != ClabCardColors.Diamond) { %>
<p>�� ����� ������ ���������� ����� ������� � ������� ��������������� 7% ������� �� ����� �����! �� ������� ������ ������������� ��������� ������ �� �������� �������������� 0, 5 ����� � ��� ������������� ���� ��� ���������� ������ ������ � ��������� ����� �����.
</p>
<p>
���������� ��������� ������������ � ��� ���������� ���������� ��� ���, ��� ��-���������� ����� ������, ��� ������� �� ���������������� ����! ������� ��������� � <%= H.Anchor(MainMenu.Urls.SpecialOffers,"������� � �������") %>, ������� ������������� ������ ����� ���������� ���������� ���� ��������!
</p>
<% } %>
<p>
�� ����� ������ ����������� <%= Url.Link<GraduateController>(c => c.RealSpecialist(), "���������� ����������� �����������") %> , ���������� ����� �������, ���� �������������� �  �������� ����� ������.  
</p>
<p>
������� ���������� �������� ����� ������������, ��� �������� ������������ � ����������� �������.  ����� ����������� ��� ����� ������ �������� ���.  ��  ��������  ������� ��� � ����� ����� ���������� ���������� � � ����������, � �����  ����� ���������� �� ����� ������ � ������������ �� ��������� �������� �������� � ������������ � ����� ������.
</p>
<% }else{ %>
<p>
�� ���������������� �� �����������! ���������� �����, ����� ����� ������ � �������, ������� �������� ��� � ����� �������� ������, �� �������� ����� �������� ����� ���������� ���������� � ������ � <%= SimpleLinks.RealSpecialist("�������������� ������� � �����������") %>!
</p>
<% } %>



</asp:Content>
