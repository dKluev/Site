<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<SimpleUtils.Common.Tuple<string,string,Specialist.Entities.Tests.UserTest>>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<% if(Model.V2.Contains("ESET")) { %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Сертификат</title>
</head>

<body>
<div style="width:297mm; height:200mm; position:absolute; top:0px; left:0px;">
<div style="position:absolute; top:58mm; left:18mm;"><p style="font-size:55px;"><i><%= Model.V1 %></i></p>
</div>
<div style="position:absolute; top:98mm; left:18mm"><p style="font-size:46px;"><i><%= Model.V2 %></i></p>
</div>
<div style="position:absolute; top:130mm; left:175mm"><p style="font-size:10px;"><%=Model.V3.RunDate.DefaultString()%></p>
</div>
<div style="position:absolute; top:170mm; left:18mm"><p style="font-size:10px;">№ <%= Model.V3.Id %> </p>
</div>
</div>






</body>
</html>

<% }else { %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Сертификат</title>
</head>

<body>
<div style="width:297mm; height:200mm; position:absolute; top:0px; left:0px;">
<div style="position:absolute; top:65mm; left:18mm"><p style="font-size:55px;"><i><%=Model.V1%></i></p>
</div>
<div style="position:absolute; top:124mm; left:18mm; width:50%"><p style="font-size:22px;font-weight: bold;"><i><%=Model.V2%></i></p>
</div>
<div style="position:absolute; top:150mm; left:18mm"><p style="font-size:27px;"><i>
<%=Model.V3.RunDate.DefaultString() %></i></p>
</div>
<div style="position:absolute; top:160mm; left:18mm"><p><img src="//cdn2.specialist.ru/Content/Image/SimplePage/facsimile1.jpg" width="268px"/></p>
</div>
<div style="position:absolute; top:192mm; left:18mm"><p style="font-size:10px;">№ <%= Model.V3.Id %> </p>
</div>
</div>






</body>
</html>
<% } %>