using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class HomeModel
    {
        public string InitialData { get; set; }
        public string GetFormUrl { get; set; }

        public HomeModel(string getFormUrl)
        {
            GetFormUrl = getFormUrl;
        }
    }
}