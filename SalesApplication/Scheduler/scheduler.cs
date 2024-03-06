using Quartz;
using Quartz.Impl;
using SalesApplication.Scheduler;
using System;
using System.Configuration;

namespace SalesApplication
{
    public class scheduler
    {
        public static void Start()
        {
           #region Scheduler to Run File Generations(Job7)

                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();
                IJobDetail job = JobBuilder.Create<MessengerJob>().Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                      (s =>
                         s.WithIntervalInMinutes(1)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(1, 1))
                      )
                    .Build();

                scheduler.ScheduleJob(job, trigger);
                #endregion
        }
    }
}