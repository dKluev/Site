<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Profile.ViewModel.Common.StudyTypeStatsVM>" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% var data = Model.GetData(); %>
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
      google.charts.load("current", {packages:["corechart"]});
      google.charts.setOnLoadCallback(drawChart);
      function drawChart() {
        var data = google.visualization.arrayToDataTable(<%= JsonConvert.SerializeObject(data) %>, true);
        var options = {
            pieHole: 0.4,
            legend: 'none',
            'chartArea': {'width': '90%', 'height': '70%'},
            colors: <%= JsonConvert.SerializeObject(data.Select(x => x[2])) %>
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
      }
    </script>
<table>
    <tr>
        <td>
<div id="piechart" style="width: 400px; height: 400px;"></div>
            
        </td>
        <td style="vertical-align: top;">
            <%= H.div[data.Select(x => H.p[H.span[""]
    .Style("display: inline-block; width: 10px; height: 10px; margin-right: 5px;background-color: " + x[2]), 
    H.Anchor(x[3].ToString(), x[0]  + " обучение"), H.b[" " + x[1] + " а.ч."]])].Style("margin-top:60px;") %>

        </td>
    </tr>
</table>

</asp:Content>
