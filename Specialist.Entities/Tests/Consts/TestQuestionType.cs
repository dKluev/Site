using System;
using System.Collections.Generic;
using SimpleUtils.Common.Enum;
using System.Linq;

namespace Specialist.Entities.Tests.Consts {
	public class TestQuestionType:BaseNamedId<TestQuestionType> {
	
		[EnumDisplayName("Один ответ")]
		public const byte OneAnswer = 1; 
		[EnumDisplayName("Несколько ответов")]
		public const byte ManyAnswers = 2; 
		[EnumDisplayName("Сопоставление")]
		public const byte Comparison = 3;
		[EnumDisplayName("Сортировка")] 
		public const byte Sort = 4;

	
	}

	
/*
		public string GetName(TestQuestionType type) {
			switch (type) {
				case TestQuestionType.OneAnswer:
					return "Один ответ" ;
				case TestQuestionType.ManyAnswers:
					return "Несколько ответов" ;
				case TestQuestionType.Comparison:
					return "Сопоставление" ;
				case TestQuestionType.Sort:
					return "Сортировка" ;
			}
		}
*/
		
}