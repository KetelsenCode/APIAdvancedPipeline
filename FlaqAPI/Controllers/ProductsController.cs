using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlaqAPI.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [HttpGet,Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "palue1", "palue2" };
        }

        // GET: api/Products/5
        [HttpGet, Route("{id:int:range(1000,3000)}")]
        public string Get(int id)
        {
            return "palue1";
        }

        // POST: api/Products
        [HttpPost, Route("")]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        [HttpPut, Route("{id:int:range(1000,3000)}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        [HttpDelete, Route("{id:int:range(1000,3000)}")]
        public void Delete(int id)
        {
        }
    }
}
