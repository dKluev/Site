<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ListVM>" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Web.Cms.Const" %>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Cms.Util"%>
<%@ Import Namespace="SimpleUtils.Extension"%>
<%@ Import Namespace="SimpleUtils.Extension"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>

<% if(Model.Filters.Count() > 0){ %>
  <script type="text/javascript">
    $(function() {
    <% if(Model.FilterValues.All(p => p.Value == null)){ %>
      $(".msg_body").hide();
    <% } %>
      $(".msg_head").click(function()
      {
        $(this).next(".msg_body").slideToggle(600);
      });
    });
</script>

     <div class="msg_head ui-state-default ui-corner-all">
        <%= CmsHtmls.ContentWithIcon(CmsHtmls.Icon().Class("ui-icon-search").ToString(),
            "Фильтр".Tag("strong")) %>
     </div>
     <div class="msg_body ui-widget-content ui-corner-all">
    <% using (Html.BeginForm()) {%>

           
            <% foreach (var property in Model.Filters) { %>
                <p>
                    <label for="<%= property.Name %>"><%= property.DisplayName() %>:</label>
                   <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.Select) { %>
                   <% Html.RenderAction("FilterComboBox", Model.MetaData.EntityType.Name + Common.ControlPosfix, 
                       new {propertyName = property.Name, 
                           currentValue = Model.FilterValues.GetValueOrDefault(property.Name)});%>
                   <% }else if(property.Control() ==
                          SimpleUtils.FluentAttributes.Const.Controls.CheckBox){ %>
                            
                        <%= HtmlControls.CheckBox(property.Name, 
                           ((bool?)  Model.FilterValues.GetValueOrDefault(property.Name))
                                                   .GetValueOrDefault()) %>
                   <% }else if(property.Control() ==
                          SimpleUtils.FluentAttributes.Const.Controls.DatePicker){ %>
                            
                <script type="text/javascript">
                    $(function() {
                    $("#<%= property.Name %>")
                        .datepicker({
                            dateFormat: 'dd.mm.yy', 
                            showButtonPanel: true,
                            showOn: 'both', 
                            buttonImage: '/Content/Image/Calendar.png',
                            buttonImageOnly: true
                        });
                    });
    	        </script>

                <%= HtmlControls.Text(property.Name, 
                    ((DateTime?)Model.FilterValues.GetValueOrDefault(property.Name))
                                    .NotNullToString("dd.MM.yyyy"), "") %>
                   <% }else{ %>
                        <%= HtmlControls.Text(property.Name, 
                            Model.FilterValues.GetValueOrDefault(property.Name), "text" ) %>
                   <% } %>
                </p>
            <% } %>
      
            <p>
                <%= HtmlControls.Submit("Фильтровать")%>
            </p>
   <%--     </fieldset>--%>
    </div>
    <% } %>
<% } %>

<br />