using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleUtils.Collections;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Context;
using Specialist.Web.Common.Html;
using Specialist.Web.Core.Logic;
using Specialist.Web.Helpers;
using Specialist.Web.Root.Center.ViewModels;
using SubSonic.Extensions;

namespace Specialist.Web.Root.Center.Views {
	public class VideoCategoryView:BaseView<VideoCategoryVM> {
		public override object Get() {
			return l(Model.Category.Description, Videos(Url, Model.Videos));
		}



		public static object Videos(UrlHelper Url, List<Video> videos, bool showDescription = false) {
			if(!videos.Any())
				return string.Empty;

            Func<Video, TagTd> videoBlock = x => H.td.Class("new-video")[
				Htmls2.YouTube(x,200), 
                x.AvailableEveryOne != null ? 
                            Convert.ToBoolean(x.AvailableEveryOne) == true ? H.p[Url.VideoLink(x)] : 
                            H.p[x.Name + " ", "<a href=\"/SimpleReg/Registration\")>Доступно после регистрации</a>"]
                    : H.p[x.Name + " ", H.a.Href(Htmls2.BaseUrl() + "SimpleReg/Registration")["Доступно после регистрации"]],
					
            showDescription && x.ShortDescription.IsEmpty() ? p[x.ShortDescription] : null ];


			return H.table[ListUtils.GetRows(videos, 3).Select(y => H.tr[y.Select(videoBlock)])];
		}
	}
}