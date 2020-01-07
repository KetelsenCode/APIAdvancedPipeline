using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace FlaqAPI.Routing
{
    public class OnlyNumbersConstraint : IHttpRouteConstraint
    {
        //lille testcase for mig selv
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object routeValue;
            if (values.TryGetValue(parameterName, out routeValue))
            {
                if (Convert.ToInt32(routeValue) == 1)
                return true;
            }
            return false;
        }
    }
}