using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Common.Utils.Files {
	public static class LectureFiles {
		public const string ext = "zip";
		public const int sizeMb = 50;
		public const int size = sizeMb*1024*1024;
		
        public static CdnFile GetFile(int lectureFileId)
        {
            return Cdns.FileLectureFile(lectureFileId + "." + ext);
        }


	}
}