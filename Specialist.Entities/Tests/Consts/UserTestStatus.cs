using System;
using System.Collections.Generic;
using SimpleUtils.Common.Enum;
using System.Linq;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Tests.Consts {
	public class UserTestStatus:BaseNamedId<UserTestStatus> {
	
		[EnumDisplayName("Не сдан")]
		public const byte Fail = 1; 
		[EnumDisplayName("Сдан")]
		public const byte Pass = 2; 
		[EnumDisplayName("Средний уровень")]
		public const byte Average = 3;
		[EnumDisplayName("Эксперт")] 
		public const byte Expert = 4;
		[EnumDisplayName("Назначен")] 
		public const byte Appoint = 5;
		[EnumDisplayName("Запланирован")] 
		public const byte InPlan = 6;

		public static readonly List<byte> PassStatuses = _.List(Pass, Average, Expert);

		public static readonly Dictionary<int, string> TrackNames = new Dictionary<int, string> {
			{Fail, "Неудовлетворительно"},
			{Pass, "Удовлетворительно"},
			{Average, "Хорошо"},
			{Expert, "Отлично"},
		};
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