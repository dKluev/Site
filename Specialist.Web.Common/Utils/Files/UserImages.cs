using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Specialist.Entities.Profile.ViewModel;
using System.Linq;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Utils.Logic;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Web.Common.Html
{
    public static class UserImages
    {
        public const string Best2010 = "Best2010";
        public const string Webinar2011 = "Webinar2011";
		  public const string Best2011 = "Best2011";
		  public const string BestGraduate = "BestGraduate";
		  public const string RealSpecialist = "RealSpecialist";

        public const int MaxWidth = 80;

        public const int SuccessStoryImageCount = 3;

        public static readonly KbSize MaxImageSize = 500;

        public static readonly KbSize ForumMaxImageSize = 500;

        public static readonly KbSize MaxBestSize = 800;

        public static readonly KbSize MaxPassportSize = 1500;
        public static readonly KbSize MaxJubileeFileSize = 1500;
        public static readonly KbSize MaxTestFileSize = 1500;


        public static Bitmap Resize(Bitmap photo, int thumbnailSize)
        {
            if (photo.Width < thumbnailSize)
            {
                return photo;
            }

            var width = thumbnailSize;
            var height = photo.Height * thumbnailSize / photo.Width;
            /*
                        }
                        else
                        {
                            width = photo.Width * thumbnailSize / photo.Height;
                            height = thumbnailSize;
                        }
            */

            var target = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(target))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Default;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(photo, 0, 0, width, height);
            }
            return target;
        }

        public static Image PutOver(Image bg, Image icon)
        {
            var resultImage = new Bitmap(bg);
            using(var g = Graphics.FromImage(resultImage))
                g.DrawImage(icon, bg.Width - icon.Width, bg.Height - icon.Height);
            return resultImage;
        }

        public static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }


        public static void SaveUserPhoto(HttpFileCollectionBase files, int userID)
        {
            foreach (string file in files)
            {
                var hpf = files[file];
                if (hpf.ContentLength == 0)
                    return;
                var filename = UserImages.GetUserPhotoFileSys(userID);
                SaveFileWithResize(hpf, filename);
                return;
            }
        }

        public static string SaveMessageImage(HttpFileCollectionBase files, int userID) {
            foreach (string file in files) {
                var hpf = files[file];
                if (hpf.ContentLength == 0)
                    return string.Empty;
                var fileName = Path.GetFileName(hpf.FileName);
                var sysName = GetMessageFileSys(userID, fileName);
                SaveFileWithResize(hpf, sysName, 800);
                return Urls.ContentRoot(Urls.FullMessageImage + userID + "/" + fileName);
            }
            return string.Empty;
        }

        public static UserImageVM GetUserImage(string filePath)
        {
            return new UserImageVM{HasFile = File.Exists(filePath)};
        }

        public static UserImageVM GetUserImage(HttpPostedFile file)
        {
            return new UserImageVM { Name = file.FileName, 
                ContentLength = file.ContentLength};
        }

        public static void SaveStoryFiles(HttpFileCollectionBase fileList, int storyID)
        {
            var files = GetSuccessStoryFilesSys(storyID);
            var i = 0;
            foreach (string file in fileList)
            {
                var hpf = fileList[file];
                if (hpf.ContentLength != 0)
                {
                    var filename = files[i];
                    SaveFileWithResize(hpf, filename);
                }
                i++;
            }
        }

        public static string GetUserPhotoFileSys(int userID)
        {
            return Urls.SysRoot + Urls.FullUserPhoto +userID + Urls.PhotoExt;
        }

        public static string GetPassportFileSysWithoutExt(int userID)
        {
            return Urls.SysRoot + Urls.FullUserPassport +userID;
        }

        public static string GetJubileeFileSys(string fileName)
        {
            return Urls.SysRoot + Urls.FullJubileeFile + fileName;
        }

        public static string GetTestQuestionFileSysWithoutExt(int questionId)
        {
            return CdnFiles.Paths.FileTestQuestion +questionId;
        }


        public static string GetTestQuestionFileSys(int qustionId) {
        	return GetTestFileSys(GetTestQuestionFileSysWithoutExt(qustionId));
        }

        public static string GetTestAnswerFileSys(int qustionId) {
        	return GetTestFileSys(GetTestAnswerFileSysWithoutExt(qustionId));
        }
        public static string GetTestAnswerFileSysWithoutExt(int answerId)
        {
            return CdnFiles.Paths.FileTestAnswer +answerId;
        }
    	private static string GetTestFileSys(string withoutExtension) {
    		foreach (var ext in Urls.TestFileExts) {
    			var fullPath = withoutExtension + ext;
    			if (File.Exists(fullPath))
    				return fullPath;
    		}
    		return null;
    	}

		public static string GetGraduateCertificateFileSys(string name, string fullName) {
			return Urls.SysRoot + Urls.ImageFolder + Urls.User + name + "/Certificate/" + fullName + ".png";
		}

		public static string GetWebinarCertFileSys(decimal sigId) {
			return CdnFiles.Paths.TempUserWebinarCert + "{0}.png".FormatWith(sigId);
		}
		public static string GetGroupCertFileSys(decimal sigId) {
			return CdnFiles.Paths.TempUserGroupCert + "{0}.png".FormatWith(sigId);
		}

		public static string GetBest2016FileSys(decimal studentId) {
			return CdnFiles.Paths.TempUserBest2016 + "{0}.png".FormatWith(studentId);
		}

//		public static string GetGroupCertVendorFileSys(decimal sigId) {
//			return CdnFiles.Paths.TempUserGroupCertVendor + "{0}.png".FormatWith(sigId);
//		}
		public static string GetGroupCertEngFileSys(decimal sigId, bool hd, bool vendor, bool ru) {
			var vendorPostfix = vendor ? "v" : "";
			var ruPostFix = ru ? "r" : "";
			return hd
			  ? CdnFiles.Paths.TempUserGroupCertEngHd + "{0}{1}{2}.gif".FormatWith(sigId, vendorPostfix, ruPostFix)
			  : CdnFiles.Paths.TempUserGroupCertEng + "{0}{1}{2}.png".FormatWith(sigId, vendorPostfix, ruPostFix);
		}
		public static string GetGroupEndFileSys(string coureTC) {
			return CdnFiles.Paths.TempUserGroupEnd + "{0}.png".FormatWith(coureTC);
		}

		public static string GetRegCouponFileSys(int userId) {
			return CdnFiles.Paths.TempUserRegCoupon + CouponUtils.PromoCode(userId) + ".jpg";
		}
		public static string GetCityCouponFileSys(int userId) {
			return CdnFiles.Paths.TempUserCityCoupon + userId + ".jpg";
		}

		public static string GetTestCertFileSys(int userTestId) {
			return Urls.SysRoot + Urls.FullTestCert + userTestId + ".png";
		}
		public static string GetPassportFileSys(int userID) {
			var withoutExtension = Urls.SysRoot + Urls.FullUserPassport + userID;
			foreach (var ext in Urls.PassportExts) {
				var fullPath = withoutExtension + ext;
				if (File.Exists(fullPath))
					return fullPath;
			}
			return null;
		}

		public static string GetBestFileSys(string bestName, Guid id) {
			return Urls.SysRoot + GetBestNameFull(bestName) + id + Urls.PhotoExt;
		}

		private static string GetBestNameFull(string bestName) {
			return Urls.ImageFolder + Urls.User + bestName + "/";
		}


		public static TagBuilder Best(string bestName, Guid id) {
			var image = Urls.ContentRoot(
				GetBestNameFull(bestName) + id + Urls.PhotoExt);
			return HtmlControls.Image(image);
		}

		public static List<string> GetSuccessStoryFilesSys(int storyID) {
			var result = new List<string>();
			for (int i = 0; i < SuccessStoryImageCount; i++) {
				result.Add(Urls.SysRoot + Urls.FullSeccessStory +
					storyID + "/" + i + Urls.PhotoExt);
			}
			return result;
		}

		public static string GetMessageFileSys(int userID, string name) {
			return Urls.SysRoot + Urls.FullMessageImage +
					userID + "/" + name;
		}

		public static List<UserImageVM> GetSuccessStoryFilesForResponse(int storyID) {

			return GetSuccessStoryFilesSys(storyID).Select(file =>
				GetUserImage(file)).ToList();
		}

		public static List<UserImageVM> GetSuccessStoryFilesForRequest() {
			var files = HttpContext.Current.Request.Files;
			return files.AllKeys.
				Select(x => files[x]).Select(file =>
				GetUserImage(file)).ToList();
		}

		public static void SaveFileWithResize(HttpPostedFileBase hpf, string filename,
			int maxWidth = MaxWidth) {
			var stream = hpf.InputStream;
			var photo = new Bitmap(stream);
			var result = Resize(photo, maxWidth);
			CreateDirectoryInNotExists(filename);
			result.Save(filename, ImageFormat.Jpeg);
		}

		private static void CreateDirectoryInNotExists(string filename) {
			var directoryName = Path.GetDirectoryName(filename);
			if (!Directory.Exists(directoryName))
				Directory.CreateDirectory(directoryName);
		}

        public const int MaxCertSigId = 100;
		public static void DeleteAllCerts(decimal sigId) {
			if (sigId <= MaxCertSigId)
				return;
			File.Delete(GetGroupCertFileSys(sigId));
//			File.Delete(GetGroupCertVendorFileSys(sigId));
			var types = _.List(false, true);
			foreach (var hd in types) {
			foreach (var vendor in types) {
			foreach (var ru in types) {
			File.Delete(GetGroupCertEngFileSys(sigId, hd, vendor,ru));
				
			}
				
			}
				
			}
		}
	}
}