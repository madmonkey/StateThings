using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StateInterface.API.Controllers
{
    public class HeadersController : ApiController
    {
        // GET: api/Headers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Headers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Headers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Headers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Headers/5
        public void Delete(int id)
        {
        }
    }
}
