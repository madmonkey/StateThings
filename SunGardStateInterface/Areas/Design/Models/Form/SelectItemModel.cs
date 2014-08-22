using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Design.Models
{
    public class SelectItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
        public SelectItemModel()
        {
            
        }
    }
}