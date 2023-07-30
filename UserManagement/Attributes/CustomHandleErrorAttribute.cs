using System.Web.Mvc;
using NLog;

namespace UserManagement.Attributes;

public class CustomHandleErrorAttribute : HandleErrorAttribute
{
    private readonly Logger _logger;

    public CustomHandleErrorAttribute()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }

    public override void OnException(ExceptionContext filterContext)
    {
        var ex = filterContext.Exception;
        _logger.Error(ex,
            $"{filterContext.RouteData.Values["controller"]}.{filterContext.RouteData.Values["action"]}: {ex.Message}");
        filterContext.ExceptionHandled = true;
        filterContext.Result = new ViewResult
        {
            ViewName = "Error"
        };
    }
}