
using System;
using System.Collections.Generic;
using System.Text;
namespace StateInterface.Designer.Model
{
    [Serializable]
    public class OptionListItem : EntityBase
    {
        private int _id = 0;
        private int _sequence = 0;
        private string _code = string.Empty;
        private string _description = string.Empty;
        private IList<OptionListItem> _optionListItems;
        private OptionListTier _optionListTier;

        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }
        public virtual int Sequence
        {
            get { return _sequence; }
            set
            {
                _sequence = value;
                RaisePropertyChanged(() => Sequence);
            }
        }
        public virtual string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged(() => Code);
            }
        }
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public virtual OptionListTier OptionListTier
        {
            get { return _optionListTier; }
            set
            {
                _optionListTier = value;
                RaisePropertyChanged(() => OptionListTier);
            }
        }

        public virtual IList<OptionListItem> OptionListItems
        {
            get { return _optionListItems; }
            set
            {
                _optionListItems = value;
                RaisePropertyChanged(() => OptionListItems);
            }
        }

        public OptionListItem()
        {
            OptionListItems = new List<OptionListItem>();
        }

        public virtual void RemoveNonPrintableAndTrim()
        {
            Code = Code.RemoveNonPrintable().Trim();
            Description = Description.RemoveNonPrintable().Trim();
        }
    }
}
