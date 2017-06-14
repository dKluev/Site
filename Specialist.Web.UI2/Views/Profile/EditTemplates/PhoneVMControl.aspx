<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EditTemplates/Field.Master" 
Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM<PhoneVM>>" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Label" runat="server">
    <label for="<%=Model.Name%>"><%=Model.Descriptor.DisplayName%></label>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
    <%=Html.TextBox(Model.For(x => x.Phone), Model.Value.Phone, new {@class = "text"})%>

	<p class="explanation">������: (495)1234567 <br/>���� ��������� � ������������� ���������������� ���, ��������� � ������� ������ � ������� � ������� ��� ��� ������. �� �� �������� ������ ������� �����</p>	
    
    <%= Html.RadioButton(Model.For(x => x.ContactType), ContactTypes.Mobile, 
        Model.Value.ContactType == ContactTypes.Mobile) %>
    ��������� <br />
    <%= Html.RadioButton(Model.For(x => x.ContactType), ContactTypes.Phone,
        Model.Value.ContactType == ContactTypes.Phone) %>
    �������� <br />
    <%= Html.RadioButton(Model.For(x => x.ContactType), ContactTypes.WorkPhone,
        Model.Value.ContactType == ContactTypes.WorkPhone) %>
    ������� <br />
</asp:Content>
