using System.Globalization;
using System.Web.Mvc;
using log4net;
using log4net.Core;

namespace Northwind.Mvc
{
public class LogFilter : IActionFilter
{
    private readonly ILog log;
    private readonly Level logLevel;

    public LogFilter(ILog log, string logLevel)
    {
        this.log = log;
        this.logLevel = log.Logger.Repository.LevelMap[logLevel];
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var message = string.Format(
            CultureInfo.InvariantCulture,
            "Executing action {0}.{1}",
            filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            filterContext.ActionDescriptor.ActionName);
        this.log.Logger.Log(typeof(LogFilter), this.logLevel, message, null);
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
        var message = string.Format(
            CultureInfo.InvariantCulture,
            "Executed action {0}.{1}",
            filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            filterContext.ActionDescriptor.ActionName);
        this.log.Logger.Log(typeof(LogFilter), this.logLevel, message, null);
    }
}
}