using System.Web;
using System.Web.Hosting;
using Specialist.Web.Root.Profile.Logic;
using SubSonic.DataProviders;
using SubSonic.Repository;

namespace Specialist.Web.Root.Profile.Services {
	public class MyBusinessService {
		SimpleRepository context;
		public MyBusinessService() {
		}

		private SimpleRepository GetContext() {
			if(context == null) {
				var path = HostingEnvironment.MapPath("~/temp/MyBusiness.db");
				var p = ProviderFactory.GetProvider(@"Data Source=" + path,
					"System.Data.SQLite");
				context = new SimpleRepository(p, SimpleRepositoryOptions.RunMigrations);
			}
			return context;
		}

		public bool Save(MyBusinessUser newUser) {
			
			var user = GetContext().Single<MyBusinessUser>(x => x.UserId == newUser.UserId);
			if(user == null) {
				GetContext().Add(newUser);
				return true;
			}
			return false;
		}
	}
}