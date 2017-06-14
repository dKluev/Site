<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<EditSuccessStoryVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="DynamicForm.Mvc.Extensions"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="all">
        <% using (Html.DefaultForm<ProfileController>(c => c.SuccessStory(null),
            new { enctype = "multipart/form-data" })){ %>
            
            <%= Html.ValidationSummary() %>
            <% Htmls.FormSection(" ", () => {%> 
        
            <%= Html.SelectFor(x => x.SuccessStory.Profession_ID, Model.Professions, "Нет")%>
            <%= Html.ControlFor(x => x.SuccessStory.Description) %>
            <tr> <td class="name">Картинки</td>
            <td class="field">
            <% for(var i = 0; i < Model.Images.Count; i++){ %>
            <% var image = Model.Images[i]; %>
            <% var name = Model.For(x => x.Images) + "[" + i + "].File"; %>
<%--            <% var idName = Model.For(x => x.Images)+"[" + i +"].ContactTypeID"; %>--%>
                    <input type="file" class="file" size="50" name="<%= name %>" />    
                 <% if(image.HasFile) { %>
                    <%= Images.UserStoryImage(Model.SuccessStory.SuccessStoryID,
                        i).Size(100, null) %>
                    <br />
                    
                    <%= Html.ActionLink<ProfileController>(c => c.DeleteStoryImage(i), 
                        "Удалить картинку") %>
                <% } %>
                    <br />
            <% } %>
            <p class="explanation">Примечание: все картинки в формате .jpg, 
            размером не более <%= UserImages.MaxImageSize.Value %> kb</p> 
            </td>    

            </tr>
            
            <% }); %>
        	<%= Htmls.Submit("ok") %>
        <% } %>
</div>
</asp:Content>
