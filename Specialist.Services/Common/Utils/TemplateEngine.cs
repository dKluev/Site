using System.Collections.Generic;
using System.Reflection;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using SimpleUtils;

namespace Specialist.Services.Common.Utils
{
    public class TemplateEngine
    {
        public static string GetText(string template, object templateData)
        {
			if(template.IsEmpty())
				return template;
            var dictionary = ObjectUtils.ToDictionary(templateData);
            foreach (var o in dictionary)
            {
                var dataName = string.Format("[{0}]", 
                    o.Key[0].ToString().ToUpper() + o.Key.Substring(1));
                template = template.Replace(dataName, o.Value.NotNullString() ); 
            }
            return template;
        }
    }

    
}