using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class OptionListItemModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public List<OptionListItemModel> OptionListItems { get; set; }

        public OptionListItemModel(OptionListItem optionListItem)
        {
            Id = optionListItem.Id;
            Code = optionListItem.Code;
            Description = optionListItem.Description;
            Sequence = optionListItem.Sequence;

            this.OptionListItems = new List<OptionListItemModel>();
            foreach (var item in optionListItem.OptionListItems)
            {
                this.OptionListItems.Add(new OptionListItemModel(item));
            }                                                      
        }
    }
}