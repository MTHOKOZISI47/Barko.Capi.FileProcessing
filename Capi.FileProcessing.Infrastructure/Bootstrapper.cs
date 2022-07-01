using Capi.FileProcessing.Application;
using Capi.FileProcessing.Core.Collection.Application;
using Capi.FileProcessing.Infrastructure.Automation;
using Capi.FileProcessing.Infrastructure.Automation.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Infrastructure
{
    public static class Bootstrapper
    {
        public static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IOutgoingServices, OutgoingServices>();
            services.AddHostedService<QuartzHostedServices>();
            services.AddSingleton<IJobFactory, QuartzJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<OutgoingJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(OutgoingJob),
            cronExpression: "0/2 * * ? * * *"         
            // cronExpression: "0 0 * ? * *"
            //cronExpression: "0 0 */12 ? * *"
             //cronExpression: "0 30 07 ? * *"
            // cronExpression: "0 30 07/1 * * * *"
            //cronExpression: "* 0 8,20 ? * *"
           ));
        }
    }
}
