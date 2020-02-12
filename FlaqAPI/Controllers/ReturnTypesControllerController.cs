using FlaqAPI.Actionfilters;
using FlaqAPI.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlaqAPI.Controllers
{
    [RoutePrefix("returntypes")]
    [ClientCacheControlActionFilterAttribute(ClientCacheControl.NoCache)]
    public class ReturnTypesControllerController : ApiController
    {
        // GET: api/ReturnTypesController
        [HttpGet, Route("void")]
        public void ReturnVoid()
        {
            
        }
        [HttpGet, Route("object")]
        // GET: api/ReturnTypesController/5
        public ComplexDTO ReturnComplexType()
        {
            return new ComplexDTO
            {
                String1 = "String 1",
                String2 = "String 2",
                Int1 = 1,
                Int2 = 2,
                Date1 = DateTime.Now
            }; 
        }
          
        
        // POST: api/ReturnTypesController
        [HttpPost, Route("dto")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(string))]
        [ValidateModelState(BodyRequired = true)]
        public IHttpActionResult ReturnModelAsIHttpActionResult([FromBody]ComplexDTOConstraints dto)
        {
            return Ok("seems to work");
        }
        
        // PUT: api/ReturnTypesController/5
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ReturnTypesController/5
        public void Delete(int id)
        {
        }
    }
}
