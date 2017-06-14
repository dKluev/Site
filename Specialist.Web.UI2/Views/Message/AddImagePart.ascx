<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Entities.Message.ViewModel"%>
<tr>
    <td class="name">
        ƒобавить изображение
    </td>
    <td class="field">
        <input type="file" name="Image" class="file" size="50" />
        <p class="explanation">
            ѕримечание: изображение в формате .jpg, размером не более
            <%= UserImages.ForumMaxImageSize.Value %>
            kb</p>
        <%= HtmlControls.Submit(CreateMessageVM.LoadImage)
                            .Attr(new {name = "isLoad"}) %>
    </td>
</tr>
