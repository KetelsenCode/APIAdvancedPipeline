using FlaqAPI.Handlers;
using FlaqAPI.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace FlaqAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //register delegating handlers
            config.MessageHandlers.Add(new FullPipelineTimerHandler());
            config.MessageHandlers.Add(new ApiKeyHeaderHandler());
            config.MessageHandlers.Add(new ForwardedHeadersHandler());
            // Web API routes

            // Register constraints
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("enum", typeof(EnumerationConstraint));
            constraintResolver.ConstraintMap.Add("onlynumber", typeof(OnlyNumbersConstraint));

            //Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);
            /*config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/
        }
    }
}
