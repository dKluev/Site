using System;
using System.Timers;
using NLog;

namespace Specialist.Web.WinService.Tasks
{
    public abstract class Task
    {
        DateTime? _nextCheck;
        readonly Timer _timer = new Timer(10000);

        public abstract double Minutes { get;}

        public void Start()
        {
            _timer.Elapsed += _timer_Elapsed;

            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public abstract void TimerTick();

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!_nextCheck.HasValue || _nextCheck.Value < DateTime.Now)
                {
                    _nextCheck = DateTime.Now.AddMinutes(Minutes);
                    TimerTick();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().ErrorException("error in " +
                    this.GetType().Name, ex);
            }
        }
    }
}