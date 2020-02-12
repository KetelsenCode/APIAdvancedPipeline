using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FlaqAPI.Actionfilters
{
    public enum ClientCacheControl
    {
        Public, // can be cached by intermediate devices even if auth is used
        Private, // browser-only, no intermediate caching, typicalle per-user data
        NoCache // no caching by browser or intermediate caching
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ClientCacheControlActionFilterAttribute : ActionFilterAttribute
    {
        // TODO: If you need constructor arguments, create properties to hold them
        //       and public constructors that accept them.
        public ClientCacheControl CacheType;
        public double CacheSeconds;
        public ClientCacheControlActionFilterAttribute(double seconds = 60.0)
        {
            CacheType = ClientCacheControl.Private;
            CacheSeconds = seconds;
        }

        public ClientCacheControlActionFilterAttribute(ClientCacheControl cacheType, double seconds = 60.0)
        {
            CacheType = cacheType;
            CacheSeconds = seconds;

            if (CacheType == ClientCacheControl.NoCache)
                CacheSeconds = -1;
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            // STEP 2: Call the rest of the action filter chain
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
            // STEP 3: Any logic you want to do AFTER the other action filters, and AFTER
            //         the action method itself is called.
            if (CacheType == ClientCacheControl.NoCache)
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true
                };

                //for older browsers
                actionExecutedContext.Response.Headers.Pragma.TryParseAdd("no-cache");

                // create a date if non present, so we can have Expires to match it
                if (!actionExecutedContext.Response.Headers.Date.HasValue)
                    actionExecutedContext.Response.Headers.Date = DateTimeOffset.Now;

                actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date;
            }
            else
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    Public = (CacheType == ClientCacheControl.Public),
                    Private = (CacheType == ClientCacheControl.Private),
                    NoCache = false,
                    MaxAge = TimeSpan.FromSeconds(CacheSeconds)
                };

                // create a date if non present, so we can have Expires to match it
                if (!actionExecutedContext.Response.Headers.Date.HasValue)
                    actionExecutedContext.Response.Headers.Date = DateTimeOffset.Now;

                actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date.Value.AddSeconds(CacheSeconds);

            }

        } 
            
            
        }
        
    }
