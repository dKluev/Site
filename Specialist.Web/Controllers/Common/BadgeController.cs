using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Services.Core.Interface;
using Specialist.Web.Root;
using Specialist.Web.Const;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Controllers.Common {
	public class BadgeController: Controller {

        [Dependency]
        public IRepository2<User> UserService { get; set; }

        [Dependency]
        public IRepository2<StudentCalc> StudentCalcService { get; set; }

		private ActionResult GetJson(object obj) {
			return Json(obj, JsonRequestBehavior.AllowGet);
		}
		public ActionResult Issuer() {
			return GetJson(new OpenBadge.Issuer());
		}


		public ActionResult RealSpecialist(string tc) {
			return GetJson(new OpenBadge.RSBadgeMeta(tc.ToUpper()));
		}

		public ActionResult UserRealSpecialist(int userId) {
			var user = UserService.GetValues(userId, x => new {x.Student_ID, x.Email});
			var card = StudentCalcService.GetValues(user.Student_ID,
				x => new {
					x.StudentClabCard.IssuedDate,
					x.StudentClabCard.ClabCardColor_TC
				});
			var tc = card.ClabCardColor_TC;
			var uid = "rs" + userId;
			return GetJson(new OpenBadge.BadgeData(uid, user.Email,
				card.IssuedDate.ToString("yyyy-MM-dd"),
				Url.Badge().Urls.RealSpecialist(tc), 
				Url.Badge().Urls.UserRealSpecialist(userId)));
		}
		 
	}
}