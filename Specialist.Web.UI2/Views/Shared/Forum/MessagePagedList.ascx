<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SimpleUtils.Collections.Paging.PagedList<Specialist.Entities.Context.UserMessage>>" %>
<%@ Import Namespace="Specialist.Web.Const"%>

    <%= Html.GetNumericPagerPretty(Model) %>
    
    <% Html.RenderPartial(PartialViewNames.MessageList, Model); %>

    <%= Html.GetNumericPagerPretty(Model) %>