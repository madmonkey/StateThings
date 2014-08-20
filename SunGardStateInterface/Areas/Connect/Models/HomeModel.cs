using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Connect.Models
{
    public class HomeModel
    {
        public string SpecificationsUrl { get; set; }
        public string InitialData { get; set; }

        public HomeModel(string specificationsUrl)
        {
            SpecificationsUrl = specificationsUrl;
        }
    }
}