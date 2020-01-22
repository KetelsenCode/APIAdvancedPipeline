using FlaqAPI.Handlers;
using FlaqAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FlaqAPI.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET: api/Values
        /*[HttpGet, Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", Request.GetApiKey() };
        }*/

        // GET: api/Values/5
        [HttpGet, Route("{id:int}", Name = "TestForwarded")]
        public int Get(int id)
        {
            return id;
        }

        // GET: api/values/complex/?String1=test1&String2=test2&Int1=1&Int2=7
        [HttpGet, Route("forwarded")]
        public IEnumerable<string> Get()
        {
            var getTestForwardedUrl = Url.Link("TestForwarded", new { id = 16 });

            return new string[] {
                getTestForwardedUrl,
                Request.GetSelfReferenceBaseUrl().ToString(),
                Request.RebaseUrlForClient(new Uri(getTestForwardedUrl)).ToString()
            };
        }

        // GET: api/values/complex/?String1=test1&String2=test2&Int1=1&Int2=7
        [HttpGet, Route("complex", Name = "ComplexRoute")]
        public IHttpActionResult Get([FromUri]ComplexDTO dto)
        {
            
            return Json(dto);
        }

        [HttpGet, Route("segments/{*array:MaxLength(256)}")]
        public string[] Get([ModelBinder(typeof(StringArrayWildcardBinder))] string[] array)
        {

            return array;
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
