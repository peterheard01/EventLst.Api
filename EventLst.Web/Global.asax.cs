using EventLst.Web;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EventLst
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // construct a scheduler
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();

            var job = JobBuilder.Create<SampleJob>().Build();

            var trigger = TriggerBuilder.Create()
                            .WithSimpleSchedule(x => x.WithIntervalInSeconds(15).RepeatForever())
                            .Build();

            scheduler.ScheduleJob(job, trigger);

        }
    }
}
