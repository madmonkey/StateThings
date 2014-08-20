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
        public string RecordsCenterName { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string ListName { get; set; }
        public List<OptionListTierModel> OptionListTiers { get; set; }
        public List<OptionListItemModel> OptionListItems { get; set; }
        public List<UsesList> FormFieldsUsing { get; set; }

        public string InitialData { get; set; }
        public string DesignHomeUrl { get; set; }
        public string ListsHomeUrl { get; set; }
        public bool CanDesignManage { get; set; }

        public OptionListModel(OptionList optionList, IEnumerable<FormFieldProjection> formFieldsUsing, string formDetailsUrl)
        {
            Id = optionList.Id;
            RecordsCenterName = optionList.RecordsCenter.Name;

            //Potentially optionList.Created.ToString(Resources.DateTimeFormat);
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

            FormFieldsUsing = new List<UsesList>();
            foreach (var uses in formFieldsUsing)
            {
                this.FormFieldsUsing.Add(new UsesList(uses, string.Format("{0}/{1}", formDetailsUrl, RecordsCenterName)));
            }
        }
    }
}