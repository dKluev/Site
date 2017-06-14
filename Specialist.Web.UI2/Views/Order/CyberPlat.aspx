<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NameValueCollection>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1251"/>
	<title>CyberPlat</title>
</head>
<body onload="document.forms[0].submit()">
  <form action="http://cyberplat.specialist.ru/Cyberplat/cybercrd.cgi" method="post">
            <% foreach (var key in Model.AllKeys) { %>
                <%= HtmlControls.Hidden(key, Model[key]) %>
            <% } %>
            <p><%= Images.Submit("pay") %></p>
        </form>
</body>
</html>