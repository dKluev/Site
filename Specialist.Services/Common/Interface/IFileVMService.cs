using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Services.Common
{
    public interface IFileVMService {
        List<UserFile> GetUserFiles(string courseTC);
    }
}