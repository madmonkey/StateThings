using System.Xml.Serialization;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using StateInterface.Designer.Model.Helper;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class FormField : EntityBase, IDataErrorInfo
    {
        private int _id = 0;
        private int _frequency = 1;
        private string _separator = string.Empty;
        private int _sequence;
        private Field _field;
        private RequestForm _requestform;
        private int _length;
        private bool _isHiddenField;
        private string _defaultValue = string.Empty;
        private string _frequencyPropertyName;
        private string _lengthPropertyName;
        private OptionList _optionList;
        private OptionListTier _OptionListTier;
        private FormField _parentFormField;

        public virtual FormField ParentFormField
        {
            get { return _parentFormField; }
            set
            {
                _parentFormField = value;
                RaisePropertyChanged(() => ParentFormField);
            }
        }

        public virtual OptionList OptionList
        {
            get { return _optionList; }
            set
            {
                _optionList = value;
                RaisePropertyChanged(() => OptionList);
                RaisePropertyChanged(() => Frequency);
            }
        }

        public virtual OptionListTier OptionListTier
        {
            get { return _OptionListTier; }
            set
            {
                _OptionListTier = value;
                RaisePropertyChanged(() => OptionListTier);
            }
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
        public virtual int Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                RaisePropertyChanged(() => Frequency);
            }
        }
        public virtual bool IsHiddenField
        {
            get { return _isHiddenField; }
            set
            {
                _isHiddenField = value;
                RaisePropertyChanged(() => IsHiddenField);
            }
        }
        public virtual int Length
        {
            get { return _length; }
            set
            {
                _length = value;
                RaisePropertyChanged(() => Length);
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
        public virtual RequestForm Requestform
        {
            get { return _requestform; }
            set { _requestform = value; }
        }
        public virtual Field Field
        {
            get { return _field; }
            set
            {
                _field = value;
                RaisePropertyChanged(() => Field);
            }
        }
        public virtual string Separator
        {
            get { return _separator; }
            set
            {
                _separator = value;
                RaisePropertyChanged(() => Separator);
            }
        }
        public virtual string DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                _defaultValue = value;
                RaisePropertyChanged(() => DefaultValue);
            }
        }
        public FormField()
        {
            // Grab string property names for use with IDataErrorInfo
            _frequencyPropertyName = PropertyHelper.GetPropertyName((FormField item) => item.Frequency);
            _lengthPropertyName = PropertyHelper.GetPropertyName((FormField item) => item.Length);
        }
        public FormField(FormField formField, RequestForm requestForm) : this()
        {
            Field = formField.Field;
            Frequency = formField.Frequency;
            Requestform = requestForm;
            Separator = formField.Separator;
            Sequence = formField.Sequence;
            DefaultValue = formField.DefaultValue;
            Length = formField.Length;
            IsHiddenField = formField.IsHiddenField;
            OptionList = formField.OptionList;
            OptionListTier = formField.OptionListTier;
            ParentFormField = formField.ParentFormField;
        }
        public FormField(Field field, RequestForm requestForm)
            : this()
        {
            _field = field;
            _requestform = requestForm;
            _frequency = field.Frequency;
            _separator = field.Separator;
            _defaultValue = field.DefaultValue;
            _length = field.Length;
            _isHiddenField = field.IsHiddenField;
        }

        #region IDataErrorInfo Members

        public virtual bool IsValid()
        {
            if (this[_frequencyPropertyName] != null) { return false; }
            if (this[_lengthPropertyName] != null) { return false; }
            return true;
        }

        public virtual string Error
        {
            get { return null; }
        }

        public virtual string this[string columnName]
        {
            get
            {
                if (columnName == _frequencyPropertyName)
                {
                    if (Frequency < 1)
                    {
                        return "Frequency must be 1 or greater";
                    }
                    if (OptionList != null && (OptionList.OptionListTiers.Count > 1 && Frequency > 1))
                    {
                        return "Frequency must be 1 if associated to a hierarchical list.";
                    }
                }

                return null;
            }
        }

        #endregion
    }
}
