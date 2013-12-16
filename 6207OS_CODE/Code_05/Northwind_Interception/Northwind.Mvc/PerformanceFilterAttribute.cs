using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace Northwind.Mvc
{
    public class PerformanceFilterAttribute : ActionFilterAttribute
    {
        [Inject]
        public IPerformanceMonitoringService PerformanceMonitor { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            PerformanceMonitor.BeginMonitor(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            PerformanceMonitor.EndMonitor(filterContext);
        }
    }

    public interface IPerformanceMonitoringService
    {
        void BeginMonitor(ActionExecutingContext ctx);

        void EndMonitor(ActionExecutedContext ctx);
    }
}