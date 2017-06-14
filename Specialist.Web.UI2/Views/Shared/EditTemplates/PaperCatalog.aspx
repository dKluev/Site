<%@ Page Title="" Language="C#" MasterPageFile="Field.Master"  Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM>" %>
<%@ Import Namespace="SimpleUtils.Common.Enum" %>
<%@ Import Namespace="Specialist.Entities.Profile" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
<% var enumValue = (Enum)((dynamic)Model).Value; %>
<% var enumType = enumValue.GetType(); %>
      <% foreach (var value in Enum.GetValues(enumType).Cast<SubscribeType>().Where(x => Convert.ToInt32(x) > 1)) { %>
	  
    <%= HtmlControls.CheckBox(Model.Name, enumValue.HasFlag(value) , 
        value) %> <%= EnumUtils.GetDisplayName(value) %> <br />
    <div style="margin: 5px 15px;">
    <%= Images.Image(CdnFiles.ImageUrls.ImageSpecialist + Images.CatalogImages.GetValueOrDefault(value) + ".jpg") %>
        
    </div>
  
	<% }  %>
</asp:Content>