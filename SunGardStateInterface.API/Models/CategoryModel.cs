using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Model;

namespace SunGardStateInterface.API.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MessageModel> Messages { get; set; }
        public CategoryModel()
        {
            Messages = new List<MessageModel>();
        }
        public CategoryModel(int parentId, Category category): this()
        {
            Id = category.Id;
            ParentId = parentId;
            Code = category.Code;
            Name = category.Name;
            Description = category.Description;
        }
        public Category ToDomain()
        {
            return new Category()
            {
                Id = Id,
                Code = Code,
                Name = Name,
                Description = Description,
            };
        }
    }
}