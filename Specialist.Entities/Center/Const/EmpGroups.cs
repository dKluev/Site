using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const
{
    public static class EmpGroups
    {
        public const string Trainer = "�������";

        public static string Team = "���";

        public const string Dismiss = "������";

	    public static List<string> Ofl = _.List( 
			"����",
		    "����",
		    "�����",
		    "����",
		    "�� ���",
		    "��",
		    "��");

//	    public const string Svid = "����";

	    public const string Sont = "����";




	    /*  public const string Manager = "��";

        public const string Vip = "���";

        public static List<string> GetAllEmployees()
        {
            return new List<string>{Manager, Vip};
        }*/
    }
}