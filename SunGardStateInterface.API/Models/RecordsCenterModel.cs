using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using StateInterface.Model;

namespace SunGardStateInterface.API.Models
{
    public class RecordsCenterModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public RecordsCenterModel()
        {
            Categories = new List<CategoryModel>();
        }
        public RecordsCenterModel(RecordsCenter recordsCenter, bool includeChildren)
            : this()
        {
            Id = recordsCenter.Id;
            Code = recordsCenter.Code;
            Name = recordsCenter.Name;
            Description = recordsCenter.Description;

            if (includeChildren == true)
            {
                foreach (var item in recordsCenter.Categories)
                {
                    Categories.Add(new CategoryModel(recordsCenter.Id, item));
                }
            }
        }
        public RecordsCenter ToDomain()
        {
            return new RecordsCenter()
            {
                Id = this.Id,
                Code = this.Code,
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}