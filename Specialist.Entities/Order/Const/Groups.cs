using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Order.Const
{
    public static class Groups {

    	public static readonly List<decimal> Ubiley = _.List<decimal>(109004, 108912, 108971); 

        public const decimal NotChoiceGroupID = 43284;

    	public const decimal TestCert = 105037;

        public const decimal WithoutPerspective = 55464;

	    public const decimal MegaShev = 211347;

        public static bool IsNotVisible(decimal groupID)
        {
            return groupID.In(NotChoiceGroupID, WithoutPerspective, TestCert);
        }
    }
}