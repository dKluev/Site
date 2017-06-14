using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Utils;

namespace Specialist.Web.Common.Html
{
    public static class CompanyFiles
    {
        public static int MaxFileSize = MaxFileSizeMB * 1024 * 1024;

        public const int CompetitionMaxFileSize = 6 * 1024 * 1024;

	    public static int MaxFileSizeMB {
		    get {
			    return 10;
		    }
	    }

        public static List<string> UserFileExt = _.List(".zip",
			".doc",
			".pdf",
".docx",
".ppt",
".pptx",
".rar",
".jpg",
".jpeg",
".png",
".gif"
);

        public static string GetFileUrl(int fileID)
        {
            var directoryInfo = GetDirectoryInfo(fileID);
            if (!directoryInfo.Exists)
                return null;
            var fileInfos = directoryInfo.GetFiles();
            if (fileInfos.Length == 0)
                return null;
            return Urls.ContentRoot(GetInnerUserFilePath(fileID) + fileInfos[0].Name);
        }

        private static DirectoryInfo GetDirectoryInfo(int fileID) {
            var innerPath = GetInnerUserFilePath(fileID);
            return new DirectoryInfo(Urls.SysRoot + innerPath);
        }

        public static UploadFile GetUploadFile(HttpPostedFileBase file)
        {
            return new UploadFile()
            {
                Name = Path.GetFileName(file.FileName),
                Stream = file.InputStream,
                ContentLength = file.ContentLength
            };
        }

        private static string GetInnerUserFilePath(int fileID)
        {
            return Urls.File + Urls.CompanyFile + fileID + "/";
        }

        public static void DeleteFile(int fileID)
        {
            var directoryInfo = GetDirectoryInfo(fileID);
            if(directoryInfo.Exists)
                directoryInfo.Delete(true);
        }

        public static string GetUserFileSys(int fileID, string sysFileName)
        {
            var path = Urls.SysRoot + GetInnerUserFilePath(fileID);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            return path + sysFileName;
        }
    }
}