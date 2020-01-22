using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FlaqAPI.Handlers
{
    /// <summary>
    /// Generic template for a DelegatingHandler
    /// </summary>
    /// <remarks>
    /// If you don't have any response processing to do for step 3, you can replace the entire
    /// block of steps 2 through 4 with a single <code>return base.SendAsync(request, cancellationToken);</code>
    /// and remove the async keyword from the method definition (since you don't need the 
    /// continuation behavior of await in that case).
    /// </remarks>
    public class ApiKeyHeaderHandler : DelegatingHandler
    {
        public const string _apiKeyHeader = "X-API-Key";

        public const string _apiQueryString = "api_key";
        protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // STEP 1: Global message-level logic that must be executed BEFORE the request
            //          is sent on to the action method
            string apiKey = null;
            //check if the header contains our apiKeyHeader
            if (request.Headers.Contains(_apiKeyHeader))
            {
                apiKey = request.Headers.GetValues(_apiKeyHeader).FirstOrDefault();
            }
            //if not then check the url query string
            else
            {
                var queryString = request.GetQueryNameValuePairs();
                var kvp = queryString.FirstOrDefault(a => a.Key.ToLowerInvariant().Equals(_apiQueryString));
                if (!string.IsNullOrEmpty(kvp.Value))
                {
                    apiKey = kvp.Value;
                }
            }

            //if no key, aport request - should be in auth filter
            /*
            if (string.IsNullOrEmpty(apiKey))
            {
                //Create response
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Missing API Key")
                };

                return Task.FromResult(response);
            }*/

            request.Properties.Add(_apiKeyHeader, apiKey);

            // STEP 2: Call the rest of the pipeline, all the way to a response message

            // STEP 3: Any global message-level logic that must be executed AFTER the request
            //          has executed, before the final HTTP response message

            // STEP 4:  Return the final HTTP response
            return base.SendAsync(request, cancellationToken);

        }
    }

    //Extension method to retrieve key
    public static class HttpRequestMessageApiKeyExtension
    {
        public static string GetApiKey(this HttpRequestMessage request)
        {
            if (request == null)
                return null;
            if (request.Properties.TryGetValue(ApiKeyHeaderHandler._apiKeyHeader, out object apiKey))
            {
                
                return (string)apiKey;
            }

            return null;
        }
    }
       
}