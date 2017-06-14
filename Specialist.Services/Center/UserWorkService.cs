using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Center {
    public class UserWorkService: Repository<UserWork>, IUserWorkService {

        public UserWorkService(IContextProvider contextProvider) : base(contextProvider) {}

        public IQueryable<UserWork> GetAllRandomForBlock() {
            var context = new SpecialistWebDataContext();
            return context.UserWorks.IsActive()
                .Where(x => x.SmallImage != null)
                .OrderBy(x => context.GetNewID());
        }

		public IQueryable<UserWork> GetAllRandomForSection(int sectionId) {
			return GetAllRandomForBlock()
				.Where(uw => uw.Section_ID == sectionId);
		}
    }
}