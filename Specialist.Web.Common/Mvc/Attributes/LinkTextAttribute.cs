using System;

namespace Specialist.Web.Common.Mvc.Attributes
{
    public class LinkTextAttribute: Attribute
    {
        public string Text { get; set; }

        public LinkTextAttribute(string text)
        {
            Text = text;
        }
    }
}