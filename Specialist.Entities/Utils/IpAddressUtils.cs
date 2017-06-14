using SimpleUtils;
using System.Linq;
using SimpleUtils.Common.Extensions;

namespace SimpleUtils.Util {
    public static class IpAddressUtils {
        public static long ConvertToNumber(string ipAddress) {
            if (ipAddress.IsEmpty())
                return 0;
            var parts = ipAddress.Split('.');
            if (parts.Length != 4)
                return 0;
            long factor = 1;
            long result = 0;
            foreach (var part in parts.Reverse()) {
                result += factor*long.Parse(part);
                factor *= 256;
            }
            return result;
        }
    }
}