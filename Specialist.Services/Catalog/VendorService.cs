using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;

namespace Specialist.Services
{
    public class VendorService: Repository<Vendor>, IVendorService
    {
        public VendorService(IContextProvider contextProvider) : base(contextProvider) {}

    }
}