using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateInterface
{
    public class Serialize
    {
        public static ContentResult Json(object model)
        {
            var result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(model),
                ContentType = "application/json"
            };
            return result;
        }
    }
}