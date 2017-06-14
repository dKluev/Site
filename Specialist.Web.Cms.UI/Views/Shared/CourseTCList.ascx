<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<% var isSection = Model is Section; %>
<%= HtmlControls.Text(isSection ? "NotAnnounce" : "CourseTCList", 
isSection ? Model.NotAnnounce : Model.CourseTCList, "text") %>
<script type="text/javascript">
    $(function () {


        $("#CourseTCList").autocomplete("/courseentity/coursenames", {
            minChars: 3,
            matchSubset: false,
            maxItemsToShow: 50,
            dataType: 'json',
            width: 600,
            multiple: true,
            formatItem: function (item) {
                return item.name;
            },
            parse: function (data) {
                var rows = new Array();
                for (var i = 0; i < data.length; i++) {
                    rows[i] = { data: data[i], value: data[i].name, result: data[i].id };
                }
                return rows;
            }

        }).result(function (event, item) {
               });


           });
    </script>