using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using YammerDailyPosterBLL;

[assembly: WebActivator.PreApplicationStartMethod(typeof(YammerDailyPosterWeb.SampleAspNetTimer), "Start")]
namespace YammerDailyPosterWeb
{
    public static class SampleAspNetTimer
    {
        private static readonly Timer _timer = new Timer(OnTimerElapsed);
        private static readonly JobHost _jobHost = new JobHost();

        public static void Start()
        {
            _timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private static void OnTimerElapsed(object sender)
        {
            _jobHost.DoWork(() => { 
                /* What is it that you do around here */
                YammerDailyPoster ydp = new YammerDailyPoster();
                ydp.DoDailyPost();
            });
        }
    }

    public class JobHost : IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;

        public JobHost()
        {
            HostingEnvironment.RegisterObject(this);
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }

        public void DoWork(Action work)
        {
            lock (_lock)
            {
                if (_shuttingDown)
                {
                    return;
                }
                work();
            }
        }
    }
}