using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Design.Models
{
    public class ApplicationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
        public ApplicationModel(Application application)
        {
            this.Id = application.Id;
            this.Name = application.Name;
            this.Description = application.Description;
            this.IsSelected = true;
        }

        public ApplicationModel()
        {
            
        }
    }
}