using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure.CustomRoute
{
    public class LegacyRoute:RouteBase
    {
        private string[] urls;

        public LegacyRoute(params string[] targetUrls)
        {
            this.urls = targetUrls;
        }

        /// <summary>
        /// Customize Routing for Input Address
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            string requestedURL = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
            if (this.urls.Contains(requestedURL, StringComparer.OrdinalIgnoreCase))
            {
                result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyURL");
                result.Values.Add("legacyURL", requestedURL);
            }
            return result;
        }

        /// <summary>
        /// Customize Routing for Output Address
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;
            if (values.ContainsKey("legacyURL") && this.urls.Contains((string)values["legacyURL"], StringComparer.OrdinalIgnoreCase))
                result = new VirtualPathData(this, new UrlHelper(requestContext).Content((string)values["legacyURL"]).Substring(1));
            return result;
        }
    }
}