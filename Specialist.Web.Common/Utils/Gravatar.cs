using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using SimpleUtils;
using SimpleUtils.Common.Extensions;

namespace SimpleUtils.Util
{
  /*  public static class Gravatar
    {
        const string GravatarUrlFormat =
            "http://www.gravatar.com/avatar/{0}?r=g&amp;s={1}&amp;d=identicon";
        private static string GetUrl(string email, int size)
        {
            var result = new StringBuilder();
            if (!string.IsNullOrEmpty(email))
            {
                byte[] hash;
                using (MD5 md5 = MD5.Create())
                {
                    byte[] data = Encoding.Default.GetBytes(email.ToLowerInvariant());
                    hash = md5.ComputeHash(data);
                }
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
                }
            }
            return string.Format(GravatarUrlFormat, result, size);
        }

        public static string GetImg(string email, int sizi) {
            return GetImg(email, sizi, null);
        }

        public static string GetImg(string email, int sizi, string @class) {
            return "<img src='" + GetUrl(email, sizi) + "' " + 
                (@class.IsEmpty() ? string.Empty 
                : " class='" + @class + "' ") + " alt='gravatar' />";
        }
    }*/
}