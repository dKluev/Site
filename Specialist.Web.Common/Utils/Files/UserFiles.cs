using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Utils;

namespace Specialist.Web.Common.Html
{
    public static class UserFiles
    {
        public static int MaxFileSize = MaxFileSizeMB * 1024 * 1024;

        public const int CompetitionMaxFileSize = 6 * 1024 * 1024;

	    public static int MaxFileSizeMB {
		    get {
			    return 50;
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
".gif",
".txt"
);

        public static string GetUserFileUrl(UserFile file)
        {
            return fileRoot + file.UserFileID + "/" + file.SysFileName;
        }


        private static DirectoryInfo GetDirectoryInfo(int fileID) {
            var innerPath = GetInnerUserFilePath(fileID);
            return new DirectoryInfo(Disk + innerPath);
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

	    public const string innerPath = "teacherfiles/";
	    public const string Disk = "e:/";
	    public const string fileRoot = "http://files.specialist.ru/user/";

        private static string GetInnerUserFilePath(int fileID)
        {
            return innerPath + fileID + "/";
        }

        public static void DeleteFile(int fileID)
        {
            var directoryInfo = GetDirectoryInfo(fileID);
            if(directoryInfo.Exists)
                directoryInfo.Delete(true);
        }

        public static string GetUserFileSys(int fileID, string sysFileName)
        {
            var path = Disk + GetInnerUserFilePath(fileID);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            return path + sysFileName;
        }

		public static List<string> Validate(HttpPostedFileBase file, int size, string extention) {
			var result = new List<String>();
			if (file.ContentLength > size) {
				result.Add("Слишком большой файл");
			}
			if (Path.GetExtension(file.FileName) != "." + extention) {
				result.Add("Неверный формат файла");
			}
			return result;
		} 

    }
}