using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Extension;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Tests {
	public partial class TestPassRule {

		public bool IsCorrect() {
			var hasExpert = ExpertMark.HasValue || AverageMark.HasValue;
			var increase = !hasExpert || (PassMark < AverageMark && AverageMark < ExpertMark);
			var maxMark = Math.Max(PassMark, ExpertMark.GetValueOrDefault());
			return _.List(Time, QuestionCount, PassMark).All(x => x > 0) && increase && QuestionCount >= maxMark;
		}
	
			public void UpdateBy(TestPassRule newTestPassRule) {
			this.Update(newTestPassRule,
				x => x.Time,
				x => x.QuestionCount,
				x => x.PassMark,
				x => x.AverageMark,
				x => x.ExpertMark
				);
		}
	}
}