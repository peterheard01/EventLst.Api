using System;
using Quartz;
using EventLst.Core;

namespace EventLst.Web
{
    public class SampleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var j = new Job();
            j.DoJob();

        }
    }
}