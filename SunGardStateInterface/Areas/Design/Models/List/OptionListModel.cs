using System.Threading;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Properties;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Areas.Design.Models
{
    public class OptionListModel
    {
        public int Id { get; set; }
        public int RecordsCenterId { get; set; }            
        public string Created { get; set; }
        public string Updated { get; set; }
        public string ListName { get; set; }
        public List<OptionListTierModel> OptionListTiers { get; set; }
        public List<OptionListItemModel> OptionListItems { get; set; }
        public string InitialData { get; set; }                
        public bool CanDesignManage { get; set; }
        public IEnumerable<FormFieldProjection> FormFieldsUsing { get; set; }

        public OptionListModel(OptionList optionList, IEnumerable<FormFieldProjection> formFieldsUsing)
        {
            Id = optionList.Id;
            RecordsCenterId = optionList.RecordsCenter.Id;

            Created = string.Format(Resources.DateTimeFormat, optionList.Created);
            Updated = string.Format(Resources.DateTimeFormat, optionList.Updated);

            ListName = optionList.ListName;

            this.OptionListTiers = new List<OptionListTierModel>();
            foreach (var optionListTier in optionList.OptionListTiers)
            {
                this.OptionListTiers.Add(new OptionListTierModel(optionListTier));
            }     

            this.OptionListItems = new List<OptionListItemModel>();
            foreach (var optionListItem in optionList.OptionListItems)
            {
                this.OptionListItems.Add(new OptionListItemModel(optionListItem));
            }

            FormFieldsUsing = formFieldsUsing;                            
        }
    }
}