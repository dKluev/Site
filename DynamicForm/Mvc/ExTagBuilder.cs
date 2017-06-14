using System;
using System.Web.Mvc;

namespace DynamicForm.Mvc
{
    public class ExTagBuilder: TagBuilder
    {
        public ExTagBuilder(string tagName) : base(tagName) {}


        public override string ToString()
        {
            return ToString(RenderMode);
        }

        public TagRenderMode RenderMode { get; set; }
    }

    public class NullTagBuilder: TagBuilder
    {
        public NullTagBuilder() : base("none") {}

        public override string ToString()
        {
            return string.Empty;
        }
    }
}