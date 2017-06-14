using System.Security.Cryptography;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;
using Specialist.Entities.Context.Const;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Html;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Root {
	public class OpenBadge {
		public class Issuer {
			public string name = "Центр Компьютерного обучения Специалист";
			public string url = "http://www.specialist.ru";
		} 
		public class RSBadgeMeta {
			public string name;
			public string description;
			public string image;
			public string criteria = CommonConst.SiteRoot + SimplePages.FullUrls.RealSpecialist;
			public string issuer = CommonConst.SiteRoot + SimplePages.FullUrls.OpenBadgeIssuer;

			public RSBadgeMeta(string tc) {
				name = "Настоящий {0} Специалист".FormatWith(ClabCardColors.GetName(tc));
				description = "Обладатель статуса Настоящий Специалист";
				image = CdnFiles.FullUrls.ImageBadgeRealSpecialist + tc + ".png";
			}
		}
		public class Recipient {
			public string type = "email";
			public bool hashed = true;
			public string salt = "abc";
			public string identity;
			public Recipient(string identity) {
				this.identity = "sha256$" + StringUtils.GetSha256(identity + salt);
			}
		}
		public class Verify {
			public string type = "hosted";
			public string url;
			public Verify(string url) {
				this.url = CommonConst.SiteRoot + url;
			}
		}
		public class BadgeData {
			public string uid;
			public Recipient recipient;
			public string issuedOn;
			public string badge;
			public Verify verify;
			public BadgeData(string uid, string identity, string issuedOn, string badge, string url) {
				var root = CommonConst.SiteRoot;
				this.uid = uid;
				this.recipient = new Recipient(identity);
				this.issuedOn = issuedOn;
				this.badge = root + badge;
				this.verify = new Verify(url);
			}
		}

	}
}