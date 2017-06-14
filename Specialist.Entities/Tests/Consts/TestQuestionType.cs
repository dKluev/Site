using System;
using System.Collections.Generic;
using SimpleUtils.Common.Enum;
using System.Linq;

namespace Specialist.Entities.Tests.Consts {
	public class TestQuestionType:BaseNamedId<TestQuestionType> {
	
		[EnumDisplayName("���� �����")]
		public const byte OneAnswer = 1; 
		[EnumDisplayName("��������� �������")]
		public const byte ManyAnswers = 2; 
		[EnumDisplayName("�������������")]
		public const byte Comparison = 3;
		[EnumDisplayName("����������")] 
		public const byte Sort = 4;

	
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