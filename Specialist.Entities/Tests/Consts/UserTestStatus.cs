using System;
using System.Collections.Generic;
using SimpleUtils.Common.Enum;
using System.Linq;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Tests.Consts {
	public class UserTestStatus:BaseNamedId<UserTestStatus> {
	
		[EnumDisplayName("�� ����")]
		public const byte Fail = 1; 
		[EnumDisplayName("����")]
		public const byte Pass = 2; 
		[EnumDisplayName("������� �������")]
		public const byte Average = 3;
		[EnumDisplayName("�������")] 
		public const byte Expert = 4;
		[EnumDisplayName("��������")] 
		public const byte Appoint = 5;
		[EnumDisplayName("������������")] 
		public const byte InPlan = 6;

		public static readonly List<byte> PassStatuses = _.List(Pass, Average, Expert);

		public static readonly Dictionary<int, string> TrackNames = new Dictionary<int, string> {
			{Fail, "�������������������"},
			{Pass, "�����������������"},
			{Average, "������"},
			{Expert, "�������"},
		};
	}

	
/*
		public string GetName(TestQuestionType type) {
			switch (type) {
				case TestQuestionType.OneAnswer:
					return "���� �����" ;
				case TestQuestionType.ManyAnswers:
					return "��������� �������" ;
				case TestQuestionType.Comparison:
					return "�������������" ;
				case TestQuestionType.Sort:
					return "����������" ;
			}
		}
*/
		
}