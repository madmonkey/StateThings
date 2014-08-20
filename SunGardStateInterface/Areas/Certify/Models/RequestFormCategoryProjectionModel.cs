using StateInterface.Designer.Model;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Areas.Certify.Models
{
    public class RequestFormCategoryProjectionModel
    {
        public int Id { get; set; }
        public RequestFormProjectionModel RequestForm { get; set; }
        public CategoryProjectionModel Category { get; set; }
        public RequestFormCategoryProjectionModel(RequestForm requestForm, Category category)
        {
            Id = category.Id;
            RequestForm = new RequestFormProjectionModel(requestForm);
            Category = new CategoryProjectionModel(category);
        }
    }
}