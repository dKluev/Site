<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>>" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Core" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<% if (!Model.Any())
	   return; %>


<div class="tab_content">
    <% var entities = Model.Select(x => x).ToList(); %>
    <% if(Htmls.IsThird) {
            var entity =
               entities.FirstOrDefault(x => x.Entity.Name == "Курсы для опытных пользователей");
            
           var office = 
               entities.FirstOrDefault(x => x.Entity.Name == "Microsoft Office");
           entities = entities.Where(x =>
               !Htmls.NamesForHide.Contains(x.Entity.Name)).ToList();
          
           if (entity != null && office != null) {
               entities.Remove(entity);
               entity = EntityWithList.New(entity.Entity, entity.List.Select(x => x).ToList());
               entity.List.AddRange(office.List.Where(x => Htmls.NamesForShow.Contains(x.Name)));
               entities.Add(entity);
           }
       } %>
	<% foreach (var entityWithLists in entities.Cast<IGrouping<IEntityCommonInfo,IEntityCommonInfo>>().ToList().GetColumns(2,2)) { %>
	<div class="tab_2column">
		<% foreach (var entityWithList in 
            entityWithLists.Cast<EntityWithList<IEntityCommonInfo,IEntityCommonInfo>>()) { %>
		<% if (entityWithList == null) { %>
		<% break; %>
		<% } %>
		<% Html.RenderPartial(PartialViewNames.RootEntity, entityWithList); %>
		<% } %>
	</div>
	<% } %>
</div>
