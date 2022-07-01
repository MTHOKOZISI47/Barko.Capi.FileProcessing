using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Infrastructure.Automation
{
   public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }

        public JobSchedule(Type jobType, int dailyHour, int dailyMin)
        {
            JobType = jobType;
            DailyHour = dailyHour;
            DailyMin = dailyMin;
        }

        public Type JobType { get; }
        public string CronExpression { get; }
        public int? DailyHour { get; set; }
        public int? DailyMin { get; set; }
    }
}
