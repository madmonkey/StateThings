using System.Threading;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class OptionListTierModel
    {
        public int Id { get; set; }
        public int Sequence { get; set; }            
        public string Name { get; set; }

        public OptionListTierModel(OptionListTier optionListTier)
        {
            Id = optionListTier.Id;
            Sequence = optionListTier.Sequence;
            Name = optionListTier.Name;                                                                              
        }
    }
}