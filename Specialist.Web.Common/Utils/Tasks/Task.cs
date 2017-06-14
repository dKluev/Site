using System;
using System.IO;
using System.Timers;
using System.Web.Hosting;
using NLog;
using Logger = Specialist.Services.Utils.Logger;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Mvc.Binders;

namespace Specialist.Web.Common.Utils.Tasks
{
    public abstract class TaskWithTimer
    {
        DateTime? _nextCheck;
        readonly static Timer _timer = new Timer(10*60*1000);

		static TaskWithTimer() {
            _timer.Start();
			
		}

        public abstract double Minutes { get;}

	    string dateFileName {
		    get {
				return HostingEnvironment.MapPath("~/temp/{0}-date.txt".FormatWith(GetType().Name));
		    }
	    } 

	    public void WriteDate() {
			File.WriteAllText(dateFileName, DateTime.Now.ToString());
	    }

	    public DateTime? GetDate() {
            var date = File.Exists(dateFileName)
	            ? (DateTime?) DateTime.Parse(File.ReadAllText(dateFileName))
	            : null;
		    return date;
	    }


        public bool IsTodayDone
        {
            get {
		        if (DateTime.Now.Hour < 9)
			        return true;
	            var date = GetDate();
				WriteDate();
	            return date.HasValue && date.Value >= DateTime.Today;
            }
        }


        public void Start()
        {
            _timer.Elapsed += _timer_Elapsed;

        }

        public void Stop()
        {
            _timer.Stop();
        }

        public abstract void TimerTick();

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try {
	            if (!_nextCheck.HasValue || _nextCheck.Value < DateTime.Now) {
		            _nextCheck = DateTime.Now.AddMinutes(Minutes);
		            TimerTick();
	            }
            }
            catch (AggregateException ex) {
				Logger.Exception(Logger.GetFromAggregate(ex), "error in " +  this.GetType().Name);
            }
            catch (Exception ex)
            {
				Logger.Exception(ex, "error in " +  this.GetType().Name);
            }
        }
    }
}