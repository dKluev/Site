using System;
using System.Reflection;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SimpleUtils.FluentAttributes.Core;
using Config=DynamicForm.Core.Config;

namespace DynamicForm
{
    public class FormBuilder
    {
        public BaseMetaData MetaData { get; set; }

        public HtmlHelper HtmlHelper { get; set; }

        public string Action { get; set; }

        public FormMethod FormMethod { get; set; }


        public FormBuilder(HtmlHelper htmlHelper, BaseMetaData metaData)
        {
            HtmlHelper = htmlHelper;
            MetaData = metaData;
        }

        public override string ToString()
        {
            using (HtmlHelper.BeginForm("","", null, FormMethod,
                new {action = Action}))
            {
                foreach (var propertyInfo in MetaData.EntityType.GetProperties())
                {
                    var model = new PropertyVMCreator(HtmlHelper)
                        .Create<object>(propertyInfo, MetaData.Instance);

                    HtmlHelper.RenderPartial(model.PartialName, model);
                }
                HtmlHelper.RenderPartial(Config.ControlFolder + "/SubmitButton");
            }
            return string.Empty;
        }
    }
}