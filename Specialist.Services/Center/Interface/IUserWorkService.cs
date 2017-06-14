using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Center {
    public interface IUserWorkService: IRepository<UserWork> {
        IQueryable<UserWork> GetAllRandomForBlock();
    	IQueryable<UserWork> GetAllRandomForSection(int sectionId);
    }
}