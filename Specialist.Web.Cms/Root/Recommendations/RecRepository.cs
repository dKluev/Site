using System.Web.Hosting;
using SubSonic.DataProviders;
using SubSonic.Repository;

namespace Specialist.Web.Cms.Root.Recommendations {
	public class RecRepository {
		 
		SimpleRepository context;
		public SimpleRepository GetContext() {

            var fileName = HostingEnvironment.MapPath("~/temp/rec.db")
				?? "rec.db";
			
			if(context == null) {
				var p = ProviderFactory.GetProvider(@"Data Source=" + fileName,
					"System.Data.SQLite");
				context = new SimpleRepository(p, SimpleRepositoryOptions.RunMigrations);
			}
			return context;
		}
	}
}