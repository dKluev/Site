using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Specialist.Services.Utils;
using Specialist.Web.Cms.Root.YandexDirect.Logic;
using Specialist.Web.Common.Utils.Tasks;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.WinService.Tasks {
	public class AbsPositionUpdateTask : TaskWithTimer {
		public override double Minutes {
			get { return 5; }
		}

		List<int> _updateHours = new List<int>{11,15,19};

		int GetNextHour(int hour) {
			var result = _updateHours.FirstOrDefault(x => x > hour);
			if(result > 0)
				return result;
			return _updateHours.First();
		}

		private int _nextHour;

		public int NextHour {
			get {
				if(_nextHour == 0) {
					var lastUpdateDate = YandexParser.GetSavedPositions().Max(x => (DateTime?) x.Date);
					if (lastUpdateDate == null) {
						var hour = GetNowHour();
						_nextHour = _updateHours.Contains(hour) 
							? hour 
							: GetNextHour(hour);
					}
					else {
						_nextHour = GetNextHour(lastUpdateDate.Value.Hour);
					}
				}
				return _nextHour;
			}
		}

		public override void TimerTick() {
			Task.Factory.StartNew(() => {
			var nextHour = NextHour;
			var nowHour = GetNowHour();
			if (nowHour.BetweenInclude(_updateHours.First(), _updateHours.Last()) && nowHour == nextHour ) {
					YandexParser.UpdatePositions();
					_nextHour = GetNextHour(nowHour);

			}
			});
		}

		private int GetNowHour() {
			return DateTime.Now.Hour;
		}
	}
}