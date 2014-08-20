using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StateInterface.API.Controllers
{
    public class FormsController : ApiController
    {
        // GET: api/Forms
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Forms/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Forms
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Forms/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Forms/5
        public void Delete(int id)
        {
        }
    }
}
