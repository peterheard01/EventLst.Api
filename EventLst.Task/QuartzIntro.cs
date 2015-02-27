using System;
using Quartz;

namespace QuartzIntro
{
    public class SampleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //Console.WriteLine(DateTimeOffset.Now);




        }
    }
}