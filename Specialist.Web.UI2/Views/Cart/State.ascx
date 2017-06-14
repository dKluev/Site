<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Tuple<string>>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<% var isEmpty = Model.Item1 == ShoppingCartStateVM.ZeroText; %>
<% var url = isEmpty ? SimplePages.FullUrls.Payment : Url.Cart().Urls.Details(); %>
 <%= H.Anchor(url, "Записаться на курсы").Class("discount_color").Id("link2") %>
 <p>
    <span>Корзина:</span>
    <%= Url.Link<CartController>(oc => oc.Details(), Model.Item1).Class("link").Style("white-space:nowrap;") %>
 </p>


