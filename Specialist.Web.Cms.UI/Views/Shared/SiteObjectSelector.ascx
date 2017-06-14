<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ControlVM>" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SimpleUtils.Reflection"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core"%>

<script type="text/javascript">
    function setAutocomplete(controlName, hiddenControlName1, 
        hiddenControlName2, serviceUrl)
    {
        $(document).ready(function () {

            var names = <%= JsonConvert.SerializeObject(SiteObjectType.SmallNamesBySysName) %>;

            $(controlName).autocomplete(serviceUrl, {

                minChars: 3,
                matchSubset: false,
                selectOnly: true,
                maxItemsToShow: 50,
                dataType: 'json',
                width: 600,
                formatItem: function (item) {
                    return names[item.type] + " " + item.name;
                },
                parse: function (data) {
                    var rows = new Array();
                    for (var i = 0; i < data.length; i++) {
                        rows[i] = { data: data[i], value: data[i].name, result: data[i].name };
                    }
                    return rows;
                }

            })
                .result(function (event, item) {
                    $(hiddenControlName1).val(item.id);
                    $(hiddenControlName2).val(item.type);
                });


        });
    }
    setAutocomplete('#SiteObjectName', '#RelationObject_ID', '#RelationObjectType',
        '<%=Url.Action<SiteObjectRelationEntityController>(c => c.Objects(null, null)) %>');
</script>

<%= HtmlControls.Text("SiteObjectName", "", "text") %>
<%= HtmlControls.Hidden("RelationObject_ID", "") %>
<%= HtmlControls.Hidden("RelationObjectType", "") %>


