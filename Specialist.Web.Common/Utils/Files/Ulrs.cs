using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Context;
using SimpleUtils.Extension;
using SimpleUtils;
using Specialist.Entities.Utils;
using Specialist.Services.Utils;
using SimpleUtils.Common.Extensions;
using System.Linq;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Utils;

namespace Specialist.Web.Common.Html
{
    public static class Urls
    {
        public static string ContentRoot(string url)
        {
			if(url.IsEmpty())
				return FirstContentRoot;
        	return ContentRoots [ Math.Abs(url.GetHashCode()) % 3] + url;
        }

	    public const string CdnRoot = "//cdn.specialist.ru/";
        public const string FirstContentRoot = CdnRoot + "Content/";

    	public static readonly string[] ContentRoots = new[] {
    		FirstContentRoot,
    		"//cdn1.specialist.ru/Content/",
    		"//cdn2.specialist.ru/Content/"
    	};

        public const string SysRoot = "C:/Inetpub/cdn/Content/";

        public const string File = "File/";

        public const string ForRegisterFolder = "ForRegister/";

        public const string User = "User/";
        public const string CommonFolder = "Common/";

        public const string Photo = "Photo/";
        public const string Passport = "Passport/";

        public const string SuccessStory = "SuccessStory/";

        public const string Message = "Message/";

        public const string FullUserPhoto = ImageFolder + User + Photo;
        public const string FullUserPassport = ImageFolder + User + Passport;
        public const string FullJubileeFile = File + "Jubilee/";
        public const string FullWebinar2011Certificate = ImageFolder + User + "Webinar2011/Certificate/";
        public const string FullTestCert = ImageFolder + User + "UserTestCert/";


        public const string FullSeccessStory = ImageFolder + User + SuccessStory;

        public const string FullMessageImage= ImageFolder + User + Message;

        public const string ImageFolder = "Image/";

        public const string JsFolder = "JavaScript/";

        public const string UserFile = "UserFile/";
        public const string CompanyFile = "CompanyFile/";

        public const string PhotoExt = ".jpg";
        public static List<string> PassportExts = _.List(".jpg", ".png", ".gif", ".pdf");
		public const string Swf = ".swf";
        public static List<string> TestFileExts = _.List(".jpg", ".png", ".gif", Swf);

        public static readonly Dictionary<string, Size> 
            ImageSizes = new Dictionary<string, Size>();

        static Urls() {
            foreach (var pattern in new [] {"*.jpg", "*.png", "*.gif"}) {
                var commonFolder = SysRoot + ImageFolder + CommonFolder;
                var newsFolder = SysRoot + ImageFolder + "News/Small/";
            	var mainFolder = SysRoot + ImageFolder + "Main/";
	            var classRoomFolder = CdnFiles.Paths.ImageComplexClassRoom;
                foreach (var folder in new [] {commonFolder, 
					newsFolder, mainFolder,classRoomFolder}) {
                    if (Directory.Exists(folder))
                        foreach (var file in Directory.GetFiles(folder,
                           pattern, SearchOption.AllDirectories)) {
                            try {
                                using (var image = System.Drawing.Image.FromFile(file)) {
                                    ImageSizes.Add(file.Replace('\\', '/').ToLower(),
										image.Size);
                                }
                            }
                            catch { }
                        }
                }
              
            }
        }

        public static string SysToWeb(string sysPath) {
            return sysPath.Replace(SysRoot, FirstContentRoot);
        }

        public static string WebToSys(string url) {
            return Regex.Replace(url,"//cdn.?\\.specialist\\.ru/Content/", SysRoot);
        }

        public static Size? GetSize(string filePath) {
        	filePath = filePath.ToLower();
            if (ImageSizes.ContainsKey(filePath))
                return ImageSizes[filePath];
            return null;
        }

        public static string Common(string name)
        {
            var innerPath = ImageFolder + CommonFolder + name;
            return ContentRoot(innerPath); 
        }

		public static List<string> GetCoupons() {
			return Directory.GetFiles(CdnFiles.Paths.ImageMarketingActionCoupon)
				.Select(Path.GetFileNameWithoutExtension).ToList();
		} 

		public static string Main(string name)
		{
			return Image("Main/" + name);
		}

        public static string CommonSys(string name) {
            var innerPath = ImageFolder + CommonFolder + name;
            return SysRoot + innerPath;
        }

		public static string MainSys(string name)
		{
			var innerPath = ImageFolder + "Main/" + name;
			return SysRoot + innerPath;
		}


        public static string Button(string name)
        {
			if(Htmls.IsNewSite)
				return Main("Button/btn_" + name + ".gif");
            return Common("Button/" + name + ".gif");
        }

        public static string Image(string name) {
#if(!DEBUG)
        if (!IsFileExist(ImageFolder + name)){
			return null;
		}
#endif
            return ContentRoot(ImageFolder + name);
        }

        public static string JavaScript(string file) {
            return FirstContentRoot + JsFolder + file;
        }

        public static string ForRegister(string file) {
            return ContentRoot(file + ForRegisterFolder + file);
        }

        public static bool IsFileExist(string path)
        {
            return System.IO.File.Exists(SysRoot + path);
        }

        public static string GetComplexMap(string urlName)
        {
            return FirstContentRoot + File + "Complex/Map/" + urlName + ".html";
        }

        public static string Employee(string employeeTC) {
            return Image("Employee/" + employeeTC + ".jpg");
        }

		public static string Temp(string file) {
			return SysRoot + "Temp/" + file;
		}

		public static string ClassRoom(string classRoomTC) {
			return Image(CdnFiles.ImageUrls.ImageComplexClassRoom + classRoomTC + ".png");
		}
		public static string ClassRoomSys(string classRoomTC) {
			return CdnFiles.Paths.ImageComplexClassRoom + classRoomTC + ".png";
		}

		public static Dictionary<string, HashSet<string>> GetCertEmployee() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var gallaryFolder = Urls.SysRoot + Urls.ImageFolder + CdnFiles.ImageUrls.ImageCertificationEmployee;
				var directory = new DirectoryInfo(gallaryFolder);
				var groups = directory.GetDirectories("*", SearchOption.AllDirectories)
					.Where(x => x.Parent.Name == directory.Name);
				return groups.DistinctToDictionary(x => x.Name.ToLower(),
					x => new HashSet<string>(x.GetFiles("*.jpg")
						.Select(z => Path.GetFileNameWithoutExtension(z.Name).ToLower())));
			});
		} 
		public static bool IsCertEmployeeExists(string certUrl, string employeeTC) {
			return GetCertEmployee().GetValueOrDefault(certUrl.ToLower())
				.GetOrDefault(x => x.Contains(employeeTC.ToLower()));
		}

		public static string CertEmployee(string prefix, string certUrl, string employeeTC) {
			return prefix + ImageFolder + CdnFiles.ImageUrls.ImageCertificationEmployee +
				certUrl + "/" + employeeTC + ".jpg";
		}
    }
}