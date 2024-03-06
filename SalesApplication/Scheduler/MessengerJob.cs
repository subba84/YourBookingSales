using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace SalesApplication.Scheduler
{
    public class MessengerJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            new Thread(() => { Messenger.SendMessage(); }).Start();
        }
    }
}