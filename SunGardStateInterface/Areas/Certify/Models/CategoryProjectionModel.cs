using StateInterface.Designer.Model;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Areas.Certify.Models
{
    public class CategoryProjectionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryProjectionModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}