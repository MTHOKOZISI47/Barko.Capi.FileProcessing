using Capi.FileProcessing.Core.Collection.Application;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Infrastructure.Automation.Jobs
{
    [DisallowConcurrentExecution]
    public class OutgoingJob : IJob
    {
        private readonly IOutgoingServices _servces;

        public OutgoingJob(IOutgoingServices outgoingServices)
        {
            _servces = outgoingServices;
        }
        public async Task Execute(IJobExecutionContext context)
        {
           await  _servces.Outgoing();
        }
    }
}
