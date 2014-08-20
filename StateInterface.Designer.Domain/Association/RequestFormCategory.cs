using System;
using System.ComponentModel;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class RequestFormCategory : EntityBase
    {
        public virtual int Id { get; set; }
        public virtual RequestForm RequestForm { get; set; }
        public virtual Category Category { get; set; }

        public RequestFormCategory()
        {}
        public RequestFormCategory(RequestForm requestForm, Category category)
        {
            RequestForm = requestForm;
            Category = category;
            Category.RequestFormCategories.Add(this);
        }
    }
}
