using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using System.Linq;
using SimpleUtils.Reflection;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.Common.ViewModel
{
    public class PollVM
    {
        public Poll Poll { get; set; }

		public int NextPollId { get; set; }

        private Dictionary<int, double> _percents;


    	private Dictionary<int, double> _consts = new Dictionary<int, double> { };

        public Dictionary<int, double> Percents
        {
            get
            {
                if(_percents == null)
                {
                    _percents = new Dictionary<int, double>();
                    var total = GetTotal();
                    var totalPercents = 0.0;
                    foreach (var option in Poll.PollOptions)
                    {
                        var result = 0.0;
                        if(total != 0) {
                        	var constPercent = _consts.GetValueOrDefault(option.PollOptionID);
							if(constPercent > 0) {
								result = constPercent;
							}else {
	                            result = ((option.VoteCount * 1000) / total)/10.0;
							}
                        }
                        _percents.Add(option.PollOptionID, result);
                        totalPercents += result;
                            
                    }
                    _percents[_percents.Keys.Last()] += 100 - totalPercents;
                }
                return _percents;
            }
        }

	    public Dictionary<int, string> Messages {
		    get {
			    return Poll.PollOptions.Where(x => !x.Message.IsEmpty()).ToDictionary(x => x.PollOptionID, x => x.Message);
		    }
	    } 

        public int GetTotal() {
            return Poll.PollOptions.Sum(po => po.VoteCount);
        }

        public double GetPercent(PollOption option) 
        {
            return Percents[option.PollOptionID];
        }
    }
}