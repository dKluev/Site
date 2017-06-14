<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ReserveVM>" %>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<table border="0">
    <tbody>
        <tr>
            <td width="30%">
                <p>
                    „исло полных мес€цев до начала курса</p>
            </td>
            <% foreach(var days in Model.GetDayList()){ %>
            <td>
                <p align="center"> <%= days/30 %> </p>
            </td>
            <% } %>
        </tr>
        <tr>
            <td width="30%">
                <p>
                    —кидка дл€ частных лиц</p>
            </td>
            <% foreach(var days in Model.GetDayList()){ %>
            <td>
                <p align="center">
                    <%= Model.GetForType(PriceTypes.PPPrefix, days) %>%
                </p>
            </td>
            <% } %>
        </tr>
        <tr>
            <td width="30%">
                <p>
                    —кидка дл€ организаций</p>
            </td>
            <% foreach(var days in Model.GetDayList()){ %>
            <td>
                <p align="center">
                    <%= Model.GetForType(PriceTypes.Corporate, days) %>%
                </p>
            </td>
            <% } %>
        </tr>
    </tbody>
</table>
