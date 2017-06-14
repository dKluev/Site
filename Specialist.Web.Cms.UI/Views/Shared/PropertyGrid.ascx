<%@ Import Namespace="SimpleUtils.Linq.Data.LInq" %>
<%@ Import Namespace="SimpleUtils.LinqToSql" %>
<%@ Import Namespace="SimpleUtils.Reflection.Extensions" %>
<%@ Import Namespace="Specialist.Web.Cms.Util" %>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Reflection"%>
<%@ Import Namespace="Specialist.Web.Cms.Core"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EditVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Entities.Catalog"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData"%>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core.Interfaces"%>
<%@ Import Namespace="Specialist.Web.Cms"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Const"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<script type="text/javascript">
    $(function() {
        <% foreach (var filterValue in Model.FilterValues) { %>
       //     $("#<%= filterValue.Key %>").attr("disabled", "disabled");
        $("#<%= filterValue.Key %> :not(option:selected)").remove();

        <% } %>

    });
    tinyMCE.init({
    mode: "exact",
    convert_urls: false,
    forced_root_block: "",
    force_p_newlines: false,
    elements : "<%= Model.HtmlProperties %>",
    theme: "advanced",
    plugins: "safari,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",
    theme_advanced_buttons2 : "objectlink,cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,cleanup,|,insertdate,inserttime,preview,image,backcolor,code",
    theme_advanced_buttons3 : "tablecontrols",
    theme_advanced_toolbar_location : "top",
    theme_advanced_toolbar_align: "left",
    valid_elements : '*[*]',
    theme_advanced_statusbar_location : "bottom",
    theme_advanced_resizing : true
    });
</script>

    <%= Html.ValidationSummary() %>

<% var helps = Helps.GetHelp(Model.MetaData.EntityType); %>
    
    <% foreach (var property in Model.MetaData.GetProperties()
           .Where(p => p.Control() 
               != SimpleUtils.FluentAttributes.Const.Controls.Hidden)) {
           var help = helps.GetValueOrDefault(property.Name);
           var @class = Model.EntityTypeName + property.Name;
           var value = Model.Entity.GetValue(property.Name);
           var propertyNameWithPrefix = Model.PrefixPropertyName + property.Name; %>
        <p>
            
            <% if(property.Control() !=  SimpleUtils.FluentAttributes.Const.Controls.PropertyGrid) { %>
            <label for="<%= propertyNameWithPrefix %>">
                <%= Html.ValidationMessage(propertyNameWithPrefix, "*") %>
                <%= property.DisplayName() %> 
                <%= help == null ? null : H.b["?"].Title(help) %>:
            </label>
            <% } %>
            <% if (property.IsReadOnly() || Model.MetaData.IsReadOnly()) { %>
                <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.None){ %>
                   <%= value %>
            <div style="clear:both;"></div>
                <% }else{ %>
                    <%= SelectListUtil.GetValueForProperty(property, Model.Entity, Url) %>
                <% } %> 
                </p>
                <% continue; %>
            <% } %>
            <% if (property.Control() == SimpleUtils.FluentAttributes.Const.Controls.Text)
               { %>
                <%= HtmlControls.Text(propertyNameWithPrefix, 
                    Model.Entity.GetValue(property.Name), "text") %>
            <% } %>
            <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.TextArea){ %>
                <%= HtmlControls.TextArea(propertyNameWithPrefix, 
                    Model.Entity.GetValue(property.Name), "textarea ") %>
            <% } %>
            <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.Number){ %>
                <%= HtmlControls.Text(propertyNameWithPrefix, 
                    Model.Entity.GetValue(property.Name), "number") %>
            <% } %>
            <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.DatePicker){ %>
                <script type="text/javascript">
                    $(function() {
                    $("#<%= propertyNameWithPrefix %>")
                        .datepicker({
                            dateFormat: 'dd.mm.yy', 
                            showButtonPanel: true,
                            showOn: 'both', 
                            buttonImage: '/Content/Image/Calendar.png',
                            buttonImageOnly: true
                        });
                    });
    	        </script>

                <%= HtmlControls.Text(propertyNameWithPrefix, 
                    ((DateTime?)Model.Entity.GetValue(property.Name))
                                    .NotNullToString("dd.MM.yyyy"), @class) %>
            <% } %>
            <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.CheckBox){ %>
             <%= HtmlControls.CheckBox(propertyNameWithPrefix, 
                 (bool)Model.Entity.GetValue(property.Name)) %>
            <% } %>
            <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.TriCheckBox) { %>
                <%= Html.TriCheckBox(property.Name, (bool?) value)%>
            <% } %>   
           
            <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.Select){ %>
              <% Html.RenderAction(SimpleUtils.FluentAttributes.Const.Controls.Select , Model.MetaData.EntityType.Name + Common.ControlPosfix, 
                       new {propertyName = propertyNameWithPrefix, 
                           currentValue = Model.Entity.GetValue(property.Name) });%>
               
            <% }else if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.CheckBoxList){ %>
            
              <% Html.RenderAction(SimpleUtils.FluentAttributes.Const.Controls.CheckBoxList, 
                     Model.MetaData.EntityType.Name + Common.ControlPosfix, 
                       new {propertyName = propertyNameWithPrefix, 
                           id = LinqToSqlUtils.GetPK(Model.Entity) });%>
               
            <% } %>
            
            <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.Html){ %>
                 <%= HtmlControls.TextArea(propertyNameWithPrefix, HttpUtility.HtmlEncode(
                         Model.Entity.GetValue(property.Name)), propertyNameWithPrefix,
                                          propertyNameWithPrefix) %>

<%--                 <%= H.Anchor("#", "Убрать html редактор").Onclick("tinymce.EditorManager.execCommand('mceRemoveControl',true, '{0}');return false;".FormatWith(propertyNameWithPrefix)) %>--%>
            <% } %>
            
             <% if(property.Control() == SimpleUtils.FluentAttributes.Const.Controls.PropertyGrid){ %>
                 <% var propertyTypeMetaData = MvcApplication.MetaDataProvider
                             .Get(Model.Entity.GetValue(property.Name).GetType()); %>
                 <% if(propertyTypeMetaData != null) { %>
                     <% Html.RenderPartial(property.Control(),
                         new EditVM(Model.Entity.GetValue(property.Name), propertyTypeMetaData,
                             propertyNameWithPrefix));%>
                 <% } %>
            <% } %>


              <% if(property.Control() == CommonConst.CourseTCList){ %>
                 <% Html.RenderPartial(property.Control(), Model.Entity);  %>
            <% } %>
           
            
        </p>
    <% } %>
   