<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Passport.User>" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Наклейка на конверт</title>
</head>

<% var address = Model.GetAddress().GetOrDefault(x => "{0}, г. {1}, {2}"
   	.FormatWith(x.Index, x.City, x.Address)); %>
<body>
	<div style="width:140mm; height:90mm; border:1px solid black; position:absolute; top:0mm; left:0mm;">
	    <span style="font-family:Arial, Helvetica, sans-serif; color:#000081; position:absolute; top:5mm; left:100mm; font-size:1.2em;">Не сгибать</span>
		<span style="font-family:Arial, Helvetica, sans-serif; color:#000081; position:absolute; top:35mm; left:10mm; width:120mm;">
			<span style="font-size:1.2em;">Куда:</span>&nbsp;<u><span contentEditable="true" style="font-size:1.1em;"><%= address %></span></u>
			<br /><br />
			<span style="font-size:1.2em;">Кому:</span>&nbsp;<u><span contentEditable="true" style="font-size:1.1em;"><%= Model.FullName %></span></u>
		</span>
	</div>
</body>
</html>