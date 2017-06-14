


 using SimpleUtils.FluentHtml.Tags;
namespace Specialist.Web.Common.Cdn {
	public static class CdnFiles {
	   public static class Paths {
		public const string ImageMainStudentClabCard = "C:/Inetpub/cdn/Content/Image/Main/StudentClabCard/";
public const string ImageMainCoupon = "C:/Inetpub/cdn/Content/Image/Main/Coupon/";
public const string ImageBadgeRealSpecialist = "C:/Inetpub/cdn/Content/Image/Badge/RealSpecialist/";
public const string ImageBadgeHours = "C:/Inetpub/cdn/Content/Image/Badge/Hours/";
public const string ImageBadgeTop = "C:/Inetpub/cdn/Content/Image/Badge/Top/";
public const string ImageBadgeUnlimit = "C:/Inetpub/cdn/Content/Image/Badge/Unlimit/";
public const string ImageBadgeBest = "C:/Inetpub/cdn/Content/Image/Badge/Best/";
public const string ImageSimplePage = "C:/Inetpub/cdn/Content/Image/SimplePage/";
public const string ImageSpecialist = "C:/Inetpub/cdn/Content/Image/Specialist/";
public const string ImageCertificationEmployee = "C:/Inetpub/cdn/Content/Image/Certification/Employee/";
public const string ImageGuide = "C:/Inetpub/cdn/Content/Image/Guide/";
public const string ImageMarketingActionCoupon = "C:/Inetpub/cdn/Content/Image/MarketingAction/Coupon/";
public const string ImageVacancy = "C:/Inetpub/cdn/Content/Image/Vacancy/";
public const string ImageUser = "C:/Inetpub/cdn/Content/Image/User/";
public const string ImageComplexClassRoom = "C:/Inetpub/cdn/Content/Image/Complex/ClassRoom/";
public const string FileTestQuestion = "C:/Inetpub/cdn/Content/File/Test/Question/";
public const string FileTestAnswer = "C:/Inetpub/cdn/Content/File/Test/Answer/";
public const string FileLectureFile = "C:/Inetpub/cdn/Content/File/LectureFile/";
public const string TempUserWebinarCert = "C:/Inetpub/cdn/Content/Temp/User/WebinarCert/";
public const string TempUserGroupCert = "C:/Inetpub/cdn/Content/Temp/User/GroupCert/";
public const string TempUserGroupCertVendor = "C:/Inetpub/cdn/Content/Temp/User/GroupCertVendor/";
public const string TempUserGroupCertEng = "C:/Inetpub/cdn/Content/Temp/User/GroupCertEng/";
public const string TempUserGroupCertEngHd = "C:/Inetpub/cdn/Content/Temp/User/GroupCertEngHd/";
public const string TempUserGroupEnd = "C:/Inetpub/cdn/Content/Temp/User/GroupEnd/";
public const string TempUserRegCoupon = "C:/Inetpub/cdn/Content/Temp/User/RegCoupon/";
public const string TempUserBest2016 = "C:/Inetpub/cdn/Content/Temp/User/Best2016/";
public const string TempUserCityCoupon = "C:/Inetpub/cdn/Content/Temp/User/CityCoupon/";
public const string TempSocialImage = "C:/Inetpub/cdn/Content/Temp/SocialImage/";

		}  
	   public static class ImageUrls {
		public const string ImageMainStudentClabCard = "Main/StudentClabCard/";
public const string ImageMainCoupon = "Main/Coupon/";
public const string ImageBadgeRealSpecialist = "Badge/RealSpecialist/";
public const string ImageBadgeHours = "Badge/Hours/";
public const string ImageBadgeTop = "Badge/Top/";
public const string ImageBadgeUnlimit = "Badge/Unlimit/";
public const string ImageBadgeBest = "Badge/Best/";
public const string ImageSimplePage = "SimplePage/";
public const string ImageSpecialist = "Specialist/";
public const string ImageCertificationEmployee = "Certification/Employee/";
public const string ImageGuide = "Guide/";
public const string ImageMarketingActionCoupon = "MarketingAction/Coupon/";
public const string ImageVacancy = "Vacancy/";
public const string ImageUser = "User/";
public const string ImageComplexClassRoom = "Complex/ClassRoom/";
public const string FileTestQuestion = "Content/File/Test/Question/";
public const string FileTestAnswer = "Content/File/Test/Answer/";
public const string FileLectureFile = "Content/File/LectureFile/";
public const string TempUserWebinarCert = "Content/Temp/User/WebinarCert/";
public const string TempUserGroupCert = "Content/Temp/User/GroupCert/";
public const string TempUserGroupCertVendor = "Content/Temp/User/GroupCertVendor/";
public const string TempUserGroupCertEng = "Content/Temp/User/GroupCertEng/";
public const string TempUserGroupCertEngHd = "Content/Temp/User/GroupCertEngHd/";
public const string TempUserGroupEnd = "Content/Temp/User/GroupEnd/";
public const string TempUserRegCoupon = "Content/Temp/User/RegCoupon/";
public const string TempUserBest2016 = "Content/Temp/User/Best2016/";
public const string TempUserCityCoupon = "Content/Temp/User/CityCoupon/";
public const string TempSocialImage = "Content/Temp/SocialImage/";

		}  
	   public static class Images {
		public static TagImg ImageMainStudentClabCard(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Main/StudentClabCard/" + file).Alt(""); }
public static TagImg ImageMainCoupon(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Main/Coupon/" + file).Alt(""); }
public static TagImg ImageBadgeRealSpecialist(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Badge/RealSpecialist/" + file).Alt(""); }
public static TagImg ImageBadgeHours(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Badge/Hours/" + file).Alt(""); }
public static TagImg ImageBadgeTop(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Badge/Top/" + file).Alt(""); }
public static TagImg ImageBadgeUnlimit(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Badge/Unlimit/" + file).Alt(""); }
public static TagImg ImageBadgeBest(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Badge/Best/" + file).Alt(""); }
public static TagImg ImageSimplePage(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/SimplePage/" + file).Alt(""); }
public static TagImg ImageSpecialist(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Specialist/" + file).Alt(""); }
public static TagImg ImageCertificationEmployee(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Certification/Employee/" + file).Alt(""); }
public static TagImg ImageGuide(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Guide/" + file).Alt(""); }
public static TagImg ImageMarketingActionCoupon(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/MarketingAction/Coupon/" + file).Alt(""); }
public static TagImg ImageVacancy(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Vacancy/" + file).Alt(""); }
public static TagImg ImageUser(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/User/" + file).Alt(""); }
public static TagImg ImageComplexClassRoom(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Image/Complex/ClassRoom/" + file).Alt(""); }
public static TagImg FileTestQuestion(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/File/Test/Question/" + file).Alt(""); }
public static TagImg FileTestAnswer(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/File/Test/Answer/" + file).Alt(""); }
public static TagImg FileLectureFile(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/File/LectureFile/" + file).Alt(""); }
public static TagImg TempUserWebinarCert(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/WebinarCert/" + file).Alt(""); }
public static TagImg TempUserGroupCert(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/GroupCert/" + file).Alt(""); }
public static TagImg TempUserGroupCertVendor(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/GroupCertVendor/" + file).Alt(""); }
public static TagImg TempUserGroupCertEng(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/GroupCertEng/" + file).Alt(""); }
public static TagImg TempUserGroupCertEngHd(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/GroupCertEngHd/" + file).Alt(""); }
public static TagImg TempUserGroupEnd(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/GroupEnd/" + file).Alt(""); }
public static TagImg TempUserRegCoupon(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/RegCoupon/" + file).Alt(""); }
public static TagImg TempUserBest2016(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/Best2016/" + file).Alt(""); }
public static TagImg TempUserCityCoupon(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/User/CityCoupon/" + file).Alt(""); }
public static TagImg TempSocialImage(string file){ return Htmls.img.Src("//cdn.specialist.ru/Content/Temp/SocialImage/" + file).Alt(""); }

		}  
	   public static class FullUrls {
		public const string ImageMainStudentClabCard = "//cdn.specialist.ru/Content/Image/Main/StudentClabCard/";
public const string ImageMainCoupon = "//cdn.specialist.ru/Content/Image/Main/Coupon/";
public const string ImageBadgeRealSpecialist = "//cdn.specialist.ru/Content/Image/Badge/RealSpecialist/";
public const string ImageBadgeHours = "//cdn.specialist.ru/Content/Image/Badge/Hours/";
public const string ImageBadgeTop = "//cdn.specialist.ru/Content/Image/Badge/Top/";
public const string ImageBadgeUnlimit = "//cdn.specialist.ru/Content/Image/Badge/Unlimit/";
public const string ImageBadgeBest = "//cdn.specialist.ru/Content/Image/Badge/Best/";
public const string ImageSimplePage = "//cdn.specialist.ru/Content/Image/SimplePage/";
public const string ImageSpecialist = "//cdn.specialist.ru/Content/Image/Specialist/";
public const string ImageCertificationEmployee = "//cdn.specialist.ru/Content/Image/Certification/Employee/";
public const string ImageGuide = "//cdn.specialist.ru/Content/Image/Guide/";
public const string ImageMarketingActionCoupon = "//cdn.specialist.ru/Content/Image/MarketingAction/Coupon/";
public const string ImageVacancy = "//cdn.specialist.ru/Content/Image/Vacancy/";
public const string ImageUser = "//cdn.specialist.ru/Content/Image/User/";
public const string ImageComplexClassRoom = "//cdn.specialist.ru/Content/Image/Complex/ClassRoom/";
public const string FileTestQuestion = "//cdn.specialist.ru/Content/File/Test/Question/";
public const string FileTestAnswer = "//cdn.specialist.ru/Content/File/Test/Answer/";
public const string FileLectureFile = "//cdn.specialist.ru/Content/File/LectureFile/";
public const string TempUserWebinarCert = "//cdn.specialist.ru/Content/Temp/User/WebinarCert/";
public const string TempUserGroupCert = "//cdn.specialist.ru/Content/Temp/User/GroupCert/";
public const string TempUserGroupCertVendor = "//cdn.specialist.ru/Content/Temp/User/GroupCertVendor/";
public const string TempUserGroupCertEng = "//cdn.specialist.ru/Content/Temp/User/GroupCertEng/";
public const string TempUserGroupCertEngHd = "//cdn.specialist.ru/Content/Temp/User/GroupCertEngHd/";
public const string TempUserGroupEnd = "//cdn.specialist.ru/Content/Temp/User/GroupEnd/";
public const string TempUserRegCoupon = "//cdn.specialist.ru/Content/Temp/User/RegCoupon/";
public const string TempUserBest2016 = "//cdn.specialist.ru/Content/Temp/User/Best2016/";
public const string TempUserCityCoupon = "//cdn.specialist.ru/Content/Temp/User/CityCoupon/";
public const string TempSocialImage = "//cdn.specialist.ru/Content/Temp/SocialImage/";

		}  
	}

}

