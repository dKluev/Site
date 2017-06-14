using System.IO;
using System.Web;

namespace Specialist.Services.Common
{
    public class FileService : IFileService
    {
        public string GetTemplate(string name)
        {
            var fileName = HttpContext.Current.Server.
                MapPath("~/Views/Shared/MailTemplate/" + name + ".erb");
            return File.ReadAllText(fileName);

        }
    }
}