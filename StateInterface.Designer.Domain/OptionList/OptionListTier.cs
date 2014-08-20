
using StateInterface.Designer.Model.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace StateInterface.Designer.Model
{
    [Serializable]
    public class OptionListTier : EntityBase, IDataErrorInfo
    {
        private OptionList _optionList;
        private int _id = 0;
        private string _name = string.Empty;
        private int _sequence;
        private string _optionListTierNamePropertyName;

        public virtual OptionList OptionList
        {
            get { return _optionList; }
            set { _optionList = value; }
        }

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

        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (OptionList != null)
                {
                    OptionList.Validate();
                }
                RaisePropertyChanged(() => Name);
            }
        }

        public OptionListTier()
        {
            _optionListTierNamePropertyName = PropertyHelper.GetPropertyName((OptionListTier item) => item.Name);
        }


        #region IDataErrorInfo Members
        public virtual string Error { get { return null; } }

        public virtual bool IsValid()
        {
            if (this[_optionListTierNamePropertyName] != null) { return false; }

            return true;
        }

        public virtual string this[string propertyName]
        {
            get
            {
                if (propertyName == _optionListTierNamePropertyName)
                {
                    if (Name.Length > 50)
                    {
                        return "Maximum length of 50 characters.";
                    }
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "Tier Name is required";
                    }

                    if (Name.Contains(" "))
                    {
                        return "May not contain spaces";
                    }

                    if (!Regex.IsMatch(Name, "^[a-zA-Z0-9]+$", RegexOptions.None))
                    {
                        return "Must be alphanumeric";
                    }
                }

                return null;
            }
        }
        #endregion
    }
}
