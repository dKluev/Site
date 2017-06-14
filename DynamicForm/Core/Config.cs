namespace DynamicForm.Core
{
    public static class Config
    {
        public static string ControlFolder {get;set;}

        static Config()
        {
            ControlFolder = "EditTemplates";
        }
    }
}