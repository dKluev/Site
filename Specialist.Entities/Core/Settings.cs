using System.IO;
using SimpleUtils.Util;

namespace Specialist.Entities.Core
{
    public class Settings
    {
        private  string settingFile = typeof(Settings).AssemblyQualifiedName + 
            ".settings.xml";

        public string WebManagerTC { get; set; }

        public Settings() { WebManagerTC = "аюдшкэ"; }

        public static Settings Get()
        {
            return null;
        }
    }
}