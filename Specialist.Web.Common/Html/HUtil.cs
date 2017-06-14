using System;
using SimpleUtils.Contracts;

namespace Specialist.Web.Common.Html {
    public static class HUtil {
        public static void NotNull<T>(T obj, Action<T> action)
            where T:class{
            if (obj != null)
                action(obj);
        }
    }
}