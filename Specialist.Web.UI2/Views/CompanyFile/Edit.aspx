<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<CompanyFileVM>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Core.Logic" %>
<%@ Import Namespace="Specialist.Web.Util" %>

<table class="table_form">
    <%= Html.ValidationSummary() %>

    <% using (Html.DefaultForm<CompanyFileController>(c => c.Edit(null), 
           new { enctype = "multipart/form-data" }))
       { %>
        <%= Html.HiddenFor(x => x.IsNew) %>
       
        <%= Html.HiddenFor(x => x.CompanyFile.Id) %>
    <p>
      <b>Название</b> <br/>
        <%= H.InputText(Model.For(x => x.CompanyFile.Name), Model.CompanyFile.Name).Style("width:600px;") %>
        
    </p>
    <% if (Model.User.IsCompany) { %>
    <%= H.Hidden(Model.For(x => x.CompanyFile.CompanyID), Model.User.CompanyID) %>
    <% }else{ %>
    <p>
      <b>Компания</b> <br/>
	<%= Html.DropDownList(Model.For(x => x.CompanyFile.CompanyID), 
		SelectListUtil.GetSelectItemList(Model.Companies, x=> x.CompanyName, x=> x.CompanyID)) %>
        
    </p>
    <% } %>
    <p>
      <b>Файл</b> <br/>
    <%= Html.File(Model.For(x => x.File)) %>  
        
    </p>
    <p class="help-block">Примечание: файл размером не более 10 mb</p>

     <%= H.Submit("Сохранить") %>
        
    
    </table>
        
    <% } %>		
