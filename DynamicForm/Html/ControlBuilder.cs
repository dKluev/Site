using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DynamicForm
{
    public class ControlBuilder
    {
        public PropertyVM Model { get; set; }

        public HtmlHelper HtmlHelper { get; set; }

        public override string ToString()
        {
            HtmlHelper.RenderPartial(Model.PartialName, Model);
            return "";
        }
    }
}