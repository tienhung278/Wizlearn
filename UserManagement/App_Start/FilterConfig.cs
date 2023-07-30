using System.Web.Mvc;
using UserManagement.Attributes;

namespace UserManagement;

public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new CustomHandleErrorAttribute());
    }
}