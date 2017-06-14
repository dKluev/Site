using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Interface;
using SimpleUtils.Extension;
using Specialist.Entities.Context.Const;

namespace Specialist.Entities.Context
{
    public partial class SimplePage : IEntityCommonInfo
    {
        public SimplePage MainParent
        {
            get
            {
                var parents = SimplePageRelations.Select(spr => spr.ParentPage);
                if(parents.Count() < 2)
                {
                    return parents.FirstOrDefault();
                }

                return SimplePageRelations
                    .FirstOrDefault(sp => sp.IsMainParent).GetOrDefault(x => x.ParentPage);
            }
        }

        public string LinkTitle {
            get {
                return LinkText ?? Title;
            }
        }

        public SimplePage RootMainParent
        {
            get
            {
                if (MainParent != null)
                {
                    var root = MainParent.RootMainParent;
                    if(root != null)
                        return root;
                    return MainParent;
                }
                return MainParent;
            }
        }

        public IEnumerable<SimplePage> Children
        {
            get
            {
                return ParentPageRelations.Select(spr => spr.SimplePage);
            }
        }

        public string Url
        {
            get {
            	return UrlName;
		/*		if(UrlName.StartsWith("/"))
					return UrlName;
                var url = "/" + UrlName;
                if (MainParent != null)
                    url = MainParent.Url + url;
                return url.ToLowerInvariant();*/
            }
        }

        public string Name
        {
            get { return Title; }
            set { Title = value; }
        }

    }
}