<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Employee>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

	<%= Html.Site().ThreeColumns(
                   Model.GroupBy(x => x.Department)
					   .OrderBy(x => {
					       var index = Departments.Order.IndexOf(x.Key.Department_TC);
					       return index < 0 ? 100 : index;
					   }).ThenBy(x => x.Key.DepartmentCaption).Select(x => 
					   Grouping.New(x.Key.DepartmentCaption, x.ToList()))) %>

  



