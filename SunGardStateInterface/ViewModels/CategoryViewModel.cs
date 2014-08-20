using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Model;

namespace SunGardStateInterface.Models.StateInterface
{
    public class ManageCategoryViewModel
    {
        public string AddUrl { get; set; }
        public string EditUrl { get; set; }
        public string DeleteUrl { get; set; }
        public string ManageUrl { get; set; }
        public string InitialData { get; set; }
        public CategoryModel Category { get; set; }
        public CategoryModel CategoryForEdit { get; set; }

        public ManageCategoryViewModel(CategoryModel model)
        {
            Category = model;
            CategoryForEdit = new CategoryModel();
        }

        public ManageCategoryViewModel()
        {
        }
    }
}