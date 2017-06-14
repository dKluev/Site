using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using DynamicForm.Mvc;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Education.ViewModel;
using Specialist.Entities.Profile;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Mvc;
using SimpleUtils;
using System.Linq;
using Specialist.Web.Common.Site;
using SimpleUtils.Extension;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Utils;
using Specialist.Web.Common.Utils.Files;

namespace Specialist.Web.Common.Html
{
    public static class Images
    {
		public static TagImg Indicator() {
			return H.Img(Urls.Common("indicator.gif")).Class("ajax-indicator");
		} 

        

        public static TagBuilder Common(string name)
        {
            var image = HtmlControls.Image(Urls.Common(name), name);
            var size = Urls.GetSize(Urls.CommonSys(name));
            if (size.HasValue)
                image.Size(size.Value.Width, size.Value.Height);
            return image; 
        }

      /*  public static TagBuilder Common(string name, string alt)
        {
            var image = HtmlControls.Image(Urls.Common(name), name);
            image.Attributes.Add("alt", alt);
            var size = Urls.GetSize(Urls.CommonSys(name));
            if (size.HasValue)
                image.Size(size.Value.Width, size.Value.Height);
            return image;
        }*/

        public static TagBuilder ContactType(object id) {
            return Common("ContactType/" + id + ".gif")
                        .Class("ico_blog");
        }

        public static TagImg StudentClabCard(string tc) {
        	return Images.Main("StudentClabCard/{0}.png".FormatWith(tc))
				.Title("Настоящий {0} Специалист".FormatWith(ClabCardColors.GetName(tc)));
        }

        public static TagImg StudentClabCardNew(string tc) {
        	return Images.Main("StudentClabCard/New/{0}.jpg".FormatWith(tc))
				.Title("{0} карта".FormatWith(ClabCardColors.GetName2(tc)));
        }

        public static TagImg Main(string name)
        {
			var size = Urls.GetSize(Urls.MainSys(name));
			var tag = new TagImg().Src(Urls.Main(name)).Alt("");
			if (size.HasValue)
				tag.Size(size.Value.Width, size.Value.Height);
        	return tag;
        }
		public static class Mobile {
	        public static TagImg Main(string name) {
	        	return Image("Mobile/Main/" + name);
	        }
			
		}

//		public static TagImg GroupCertEng(decimal sigId) {
//			return H.Img(Urls.SysToWeb(UserImages.GetGroupCertFileSys(sigId)));
//		}

		public static TagImg Best2016(decimal studentId) {
			return H.Img(Urls.SysToWeb(UserImages.GetBest2016FileSys(studentId)));
		}
//		public static TagImg GroupCertVendor(decimal sigId) {
//			return H.Img(Urls.SysToWeb(UserImages.GetGroupCertVendorFileSys(sigId)));
//		}
		public static TagImg GroupCertEng(decimal sigId, bool vendor, bool ru) {
			return H.Img(Urls.SysToWeb(UserImages.GetGroupCertEngFileSys(sigId, false, vendor, ru)));
		}
		public static TagImg SeminarCert(decimal sigId) {
			return H.Img(Urls.SysToWeb(UserImages.GetWebinarCertFileSys(sigId)));
		}

		public static TagImg Forum(string name) {
			return Main("Forum/{0}.gif".FormatWith(name));
		}

        public static TagImg Attendance(Attendance name)
        {
			if(name == Specialist.Entities.Const.Attendance.None)
				return null;
            return Image("Common/Group/" + name + ".gif");
        }

        public static TagP Attendances() {
	        return H.p[
		        Images.Attendance(Specialist.Entities.Const.Attendance.Truancy),
		        " - Не явился на занятие",
		        Images.Attendance(Specialist.Entities.Const.Attendance.Lateness),
		        " - Опоздал на занятие",
		        Images.Attendance(Specialist.Entities.Const.Attendance.Departure),
		        " - Ушел раньше с занятия"].Class("signs2");
        }


        public static TagBuilder Submit(string name)
        {
            return HtmlControls.ImgSubmit(Urls.Button(name), name)
                .Class("button");
        }

        public static TagBuilder Button(string name)
        {
            return HtmlControls.Image(Urls.Button(name), name);
        }

        public static TagImg Section(string name)
        {
            return H.img.Src(Urls.Image("Section/" + name + ".jpg")).Alt(name);
        }

  

        public static TagImg NewsSmall(News news) {
            var newsSmall = "News/Small/" + news.SmallImage;
            var image = Image(newsSmall).Alt(news.Title);
            var size = Urls.GetSize(Urls.SysRoot + Urls.ImageFolder + newsSmall);
            if (size.HasValue)
                image.Size(size.Value.Width, size.Value.Height);
            return image;
        }

        public static TagImg Course(string name) {
        	var src = Urls.Image("Course/" + name + ".jpg");
        	return H.Img(src);
        }

    	public static TagBuilder UserPhoto(int userID)
        {
            var image = Urls.Image("User/Photo/" + userID + Urls.PhotoExt);
            if (image.IsEmpty())
                image = Urls.Image("User/Photo/default.jpg");
            return HtmlControls.Image(
                image + "?" + DateTime.Now.Millisecond, userID.ToString()).Size(80, null);
        }

      

        public static TagBuilder UserStoryImage(int storyID, int index)
        {
            var image = Urls.Image(Urls.User + Urls.SuccessStory + 
                storyID + "/" + index + Urls.PhotoExt);
            return HtmlControls.Image(
                image + "?" + DateTime.Now.Millisecond, index.ToString());
        }

        public static TagBuilder Banner(string name, string alt)
        {
            return HtmlControls.Image(Urls.Image("UsefulImages/" + name), alt);
        }

        public static TagBuilder Organization(Organization organization) {
            return HtmlControls.Image(
                Urls.Image("Organization/Logo/" + organization.LogoImg), organization.Name);
        }

        public static TagBuilder MarketingAction(MarketingAction marketingAction) {
            return HtmlControls.Image(
                Urls.Image("MarketingAction/" + marketingAction.UrlName + ".jpg"), 
                    marketingAction.Name);
        }
        public static TagBuilder MarketingActionSmall(MarketingAction marketingAction) {
            return HtmlControls.Image(
                Urls.Image("MarketingAction/Small/" + marketingAction.UrlName + ".jpg"), 
                    marketingAction.Name);
        }

		public static TagImg Coupon(string urlName) {
            return H.Img(
                Urls.Image("MarketingAction/Coupon/" + urlName + ".jpg"));
        }


        public static TagBuilder Competitions(Competition competition)
        {
            return HtmlControls.Image(
                Urls.Image("Competitions/" + competition.UrlName + ".jpg"),
                    competition.Name);
        }

        public static TagBuilder UsefulInfomation(UsefulInformation usefulInformation)
        {
            return HtmlControls.Image(
                Urls.Image("UsefulInformation/" + usefulInformation.UrlName + ".jpg"),
                    usefulInformation.Name);
        }

        public static TagBuilder OrgResponse(OrgResponse response) {
            if (response.OriginalImg.IsEmpty())
                return new NullTagBuilder();
            return HtmlControls.Image(
                Urls.Image("Organization/ResponseOriginal/" + response.OriginalImg),
                    "Отзыв компании: " + response.Organization.Name);
        }

        public static TagBuilder AuthorizationType(string name)
        {

            return HtmlControls.Image(
                Urls.Image("AuthorizationType/" + name + ".gif"), name);
        }

        public static TagImg Employee(string employeeTC)
        {
            var image = Urls.Employee(employeeTC);
			if(image == null)
				image = Urls.Employee("default");
            return H.Img(image,employeeTC);
        }

        public static TagImg Entity(IEntityCommonInfo entity)
        {
            if (entity is Employee)
                return Employee(entity.UrlName).Alt(entity.Name);
            return Image(entity.GetType().Name + "/" + GetTrueUrlName(entity.UrlName) + ".jpg")
				.Alt(entity.Name);
        }
		
		public static string GetTrueUrlName(string urlName) {
			if(!urlName.StartsWith("/"))
				return urlName;
			var urlSegments = urlName.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
			return urlSegments.Last();
		}

        public static TagBuilder EntitySmall(IEntityCommonInfo entity)
        {
            return Root(entity.GetType().Name + "/Small/" + GetTrueUrlName(entity.UrlName) + ".gif")
                .Size(70, 70).Class("ico");
        }

		public static List<TagImg> MainSlider() {
			return MethodBase.GetCurrentMethod().Cache(() => {
				var gallaryFolder = Urls.MainSys("MainPageSlider/");
				var files = Directory.GetFiles(gallaryFolder, "*.jpg").OrderBy(x => x);
				return files.Select(x => Main("MainPageSlider/" + Path.GetFileName(x)))
					.ToList();
			});

		} 

		public static Dictionary<decimal, Tuple<string, DateTime>> GetGroupsPhoto() {
			return MethodBase.GetCurrentMethod().Cache(() => {
				var gallaryFolder = Urls.SysRoot + Urls.ImageFolder + "/Employee/Gallery/";
				var directory = new DirectoryInfo(gallaryFolder);
				var groups = directory.GetDirectories("*", SearchOption.AllDirectories)
					.Where(x => x.Parent.Name != directory.Name);
				return groups.DistinctToDictionary(x => (decimal) StringUtils.ParseInt(x.Name)
					.GetValueOrDefault(),
					x => Tuple.Create(x.Parent.Name, x.CreationTime));
			});
		} 



		public static List<string> GetGallaryFiles(IEntityCommonInfo entity, string folder) {
            var gallaryFolder = Urls.SysRoot + Urls.ImageFolder + 
                entity.GetType().Name + "/" + folder + "/" + entity.UrlName;
            if (!Directory.Exists(gallaryFolder))
                return new List<string>();
	        var files = Directory.GetFiles(gallaryFolder, "*.jpg",SearchOption.AllDirectories);
			return files.ToList();

		} 
        public static string Gallary(IEntityCommonInfo entity, bool withSlider = false, string folder = "Gallery",
			string filter = "") {
	        var files = GetGallaryFiles(entity, folder);
	        if (!files.Any())
		        return null;
            var result = new List<string>();
	        if (!filter.IsEmpty())
		        files = files.Where(x => x.Contains(filter)).ToList();
	        var imageDescs = new ImageMetas().Descs();
            foreach (var imageFile in files.Select(x => x.Replace('\\', '/'))
				.Where(x => !x.ToLowerInvariant().EndsWith("-s.jpg"))) {
                var imageUrl = Urls.SysToWeb(imageFile).ToLowerInvariant();
                var smallImageUrl = GetSmallImageUrl(imageUrl);
	            var name = Path.GetFileName(Path.GetDirectoryName(imageFile)) + "/" + Path.GetFileName(imageFile);
	            var alt = imageDescs.GetValueOrDefault(name.ToLower());
	            var tagA = H.Anchor(imageUrl, 
					H.Img(smallImageUrl).Alt(alt).ToString())
					.Class("fancy-box").Rel("entity-fancy-box").Style("padding:5px");
	            result.Add(tagA.ToString());
            }

			if(!withSlider || result.Count <= 3)
	            return result.JoinWith("").Tag("div");
        	return  CommonSiteHtmls.Carousel(result.CutInPartCount(3)
				.Select(x => H.span[x.JoinWith("")].Style("margin:0 20px").Class("fit-width-item")), true).ToString();

        }

    	public static string GetSmallImageUrl(string imageUrl) {
    		return imageUrl.Replace(".jpg", "-s.jpg");
    	}

    	public static TagBuilder Root(string name)
        {
            return HtmlControls.Image(Urls.Image(name), 
                Path.GetFileNameWithoutExtension(name));
        }

		public static TagImg ClassRoom(string classRoomTC) {
			var image = H.Img(Urls.ClassRoom(classRoomTC));
            var size = Urls.GetSize(Urls.ClassRoomSys(classRoomTC));
            if (size.HasValue)
                image.Size(size.Value.Width, size.Value.Height);
			return image;
		}
		public static TagImg Guide(string img) {
			var image = Image(CdnFiles.ImageUrls.ImageGuide + img);
			return image;
		}

		public static TagImg Image(string name) {
			var path = Urls.Image(name);
			if(path == null)
				return new NullTagImg();
			return H.img.Src(path).Alt(
				Path.GetFileNameWithoutExtension(name));
		}

/*
		public static string CertEmployee(string certUrl, string employeeTC) {
			var tagImg =
				Image(CdnFiles.ImageUrls.ImageCertificateionEmployee + certUrl + "/" + employeeTC + ".jpg")
				.Class("");

			return tagImg.ToString();
		}
*/


	    public static Dictionary<SubscribeType, string> CatalogImages = new Dictionary<SubscribeType, string>{
			{SubscribeType.Newspaper, "gazeta3b"},
			{SubscribeType.It, "programming2012"},
			{SubscribeType.Buh, "buhgalter2013"},
			{SubscribeType.Design, "webdesign_2013-s"},
			{SubscribeType.School, "school"},
		
		}; 
    }
}