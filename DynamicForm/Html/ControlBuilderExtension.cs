using System.ComponentModel;

namespace DynamicForm
{
    public static class ControlBuilderExtension
    {
        public static ControlBuilder UIHint(this ControlBuilder controlBuilder,
            string uiHint)
        {
            controlBuilder.Model.PartialName = uiHint;
            return controlBuilder;
        }

    }
}