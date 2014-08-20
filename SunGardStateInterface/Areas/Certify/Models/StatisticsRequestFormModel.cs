using System.Collections.Generic;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Certify.Models
{
    public sealed class StatisticsRequestFormModel 
    {
        public StatisticsDetailsModel Statistics { get; set; }
        public int Id { get; set; }
        public string FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public RecordsCenterModel RecordsCenter { get; set; }
        public List<RequestFormCategoryProjectionModel> RequestFormCategories { get; set; }

        public StatisticsRequestFormModel(RequestForm form)
        {
            Id = form.Id;
            FormId = form.FormId;
            Title = form.Title;
            Description = form.Description;
            RecordsCenter = new RecordsCenterModel(form.RecordsCenter);
            RequestFormCategories = new List<RequestFormCategoryProjectionModel>();

            foreach (var item in form.RequestFormCategories)
            {
                RequestFormCategories.Add(new RequestFormCategoryProjectionModel(form, item.Category));
            }
            Statistics = new StatisticsDetailsModel(form.GetQaStatisticsDetails());
        }
    }
}