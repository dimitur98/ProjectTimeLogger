using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace ProjectTimeLogger.Helpers.Mvc.Routing
{
    public static class IUrlHelpersExtensions
    {
        public static string Action(this IUrlHelper helper, string actionName, string controllerName, NameValueCollection queryString)
        {
            return helper.Action(actionName, controllerName) + queryString.ToQueryString();
        }
    }
}
