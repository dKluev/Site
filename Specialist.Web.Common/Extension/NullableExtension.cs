using System;

namespace SimpleUtils
{
    public static class NullableExtension
    {
        public static string NotNullToString(this Nullable<DateTime> obj, string format)
        {
            if (obj.HasValue)
                return obj.Value.ToString(format);
            return null;
        }

        
    }
}