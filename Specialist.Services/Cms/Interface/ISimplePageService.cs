using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Cms.Interface
{
    public interface ISimplePageService : IRepository<SimplePage>
    {
        SimplePage GetByUrl(string url);
    }
}