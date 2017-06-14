using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using System.Linq;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const
{
    public static class DayShifts
    {
        public const string Morning = "�";//����
        public const string Day = "�";//����
        public const string MorningDay = "��";//����-����
        public const string Evening = "�";//�����
        public const string DayEvening = "��";//����-�����
        public const string MorningDayEvening = "���";//����-����-�����
        public const string NotStandard = "���";//�������������

     /*   private static readonly List<string> _fullDay1 =
            new List<string> {Morning, Day, Evening};

        private static readonly List<string> _fullDay2 =
            new List<string> {MorningDay, Evening};
*/
        public static List<string> GetOpposite(string dayShiftTC)
        {
            var result = MorningDayEvening.Replace(dayShiftTC, string.Empty);
            return GetCurrent.Where(ds => result.Contains(ds)).ToList();

        }

	    public static List<string> AllMornings = _.List(Morning, Day, MorningDay);
        public static bool IsAllMorningDay(string dayShiftTC)
        {
            return AllMornings.Contains(dayShiftTC);
        }

        public static readonly List<string> GetCurrent =
            new[] { Morning, Day, MorningDay, Evening }.ToList();
        

       
    }
}