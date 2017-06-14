using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Context
{
    public partial class News: IEntityCommonInfo
    {
	    public NewsType NewsType {
		    get {
			    var type = Type;
			    if (type == Context.NewsType.TrainerPublish) {
				    type = NewsType.Publish;
			    }
				return NewsType.AllById.GetValueOrDefault(type);
		    }
	    }

	    public bool IsFreeWebinar {
		    get {
			    var t = Title.ToLower();
			    return t.Contains("бесплатн") && (t.Contains("вебинар") || t.Contains("семинар"));
		    }
	    }

        partial void OnCreated() {
            this.PublishDate = DateTime.Today;
        }

        public string YouTubeId {
            get {
                if (!IsVideo)
                    return null;
                var match = Regex.Match(Description, @"youtube.com/v/([^&?]+)");
                if(match.Success)
                    return match.Groups[1].Value;
                return null;
            }
        }

        public bool IsVideo {
            get { return Type == NewsType.Video; }
        }

        public List<string> CourseTCSplitList {
            get { return StringUtils.SafeSplit(CourseTCList); }
        }

    	public string Name {
			get { return Title; }
    	}

    	public string UrlName {
			get { return NewsID.ToString(); }
    	}

    	public int WebSortOrder {
			get { return 0; }
    	}
    }
}