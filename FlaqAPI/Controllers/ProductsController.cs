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
        public enum Widgets
        {
            Bolt,
            Screw,
            Nut,
            Motor
        }
        /*[HttpGet, Route("widget/{number:onlynumber}")]
          public string OnlyNumbers(string number)
          {
              return number.ToString();
          }*/

        [HttpGet, Route("widget/{widget:enum(FlaqAPI.Controllers.ProductsController+Widgets)}")]
        public string GetProductsWithWidget(string widget)
        {
            return "widget-" + widget;
        }
        // GET: api/Products
        [HttpGet, Route("widget/{testint:onlynumber}")]
        public IEnumerable<string> Get()
        {
            return new string[] { "palue1", "palue2" };
        }

        // GET: api/Products/5
        [HttpGet, Route("{id:int:range(1000,3000)}", Name = "GetById")]
        //Tilde overrides the routeprefix
        [Route("~/prods/{id:int:range(1000,3000)}")]
        public string Get(int id)
        {
            return "palue1";
        }

        // POST: api/Products
        [HttpPost, Route("")]
        public void CreateProduct([FromBody]string value)
        {
        }

        [HttpPost, Route("{status:alpha=pending}/{prodId:int:range(1000,3000)?}")]
        public HttpResponseMessage CreateProduct([FromUri]string status, int prodId = 5)
        {
            //logic first

            var response = Request.CreateResponse(HttpStatusCode.Created);

            //self-refering link
            string uri = Url.Link("GetById", new { id = prodId });
            response.Headers.Location = new Uri(uri);
            return response; 
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
