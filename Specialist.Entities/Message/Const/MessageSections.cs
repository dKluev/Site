using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Profile.Const
{
    public static class MessageSections
    {

        public const string Forum = "Forum";

	    public const int GraduateClub = 6;

	    public const int Job = 10;
	    public const int Vacancy = 63;

	    public static Dictionary<int, int> Orders = _.Dict(Job, 1000).AddFluent(Vacancy, 1001);

    }
}