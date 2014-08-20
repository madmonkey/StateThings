using System.Xml.Serialization;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using StateInterface.Designer.Model.Helper;
using System.Text.RegularExpressions;

namespace StateInterface.Designer.Model
{
    public enum FormatMaskType
    {
        Text = 0,
        Numeric = 1,
        Date = 2,
        SSN = 3,
        Counter = 4,
        Name = 5,
        Convert = 6,
        SystemDate = 7,
        StateProvinceRegion = 8
    }

    [Serializable]
    public class Field : EntityBase, IDataErrorInfo
    {
        private int _id = 0;
        private string _tagName = string.Empty;
        private string _description = string.Empty;
        private string _toolTip = string.Empty;
        private bool _makeUpperCase = true;
        private int _length;
        private string _prefix = string.Empty;
        private string _suffix = string.Empty;
        private string _defaultValue = string.Empty;
        private int _frequency;
        private string _separator = string.Empty;
        private string _transformFormat;
        private RecordsCenter _recordsCenter;
        private DateTime _created;
        private DateTime? _updated;
        private string _tagNamePropertyName;
        private string _maxLengthPropertyName;
        private string _frequencyPropertyName;
        private string _separatorPropertyName;
        private string _transformFormatPropertyName;
        private bool _isHiddenField;
        private FormatMaskType _formatMask;
        private string _defaultValuePropertyName;
        private bool _acceptReturn;

        public virtual FormatMaskType FormatMask
        {
            get { return _formatMask; }
            set
            {
                _formatMask = value;
                RaisePropertyChanged(() => FormatMask);
            }
        }
        public static IEnumerable<string> DateFormatMasks
        {
            get
            {
                var masks = new List<string>()
                {
                    "MMDDYYYY",
                    "MMDDYY",
                    "YYYYMMDD",
                    "YYMMDD",
                    "MM-DD-YYYY",
                    "MM-DD-YY",
                    "YYYY-MM-DD",
                    "YY-MM-DD",
                };

                return masks;
            }
        }
        public static IEnumerable<string> NameFormatMasks
        {
            get
            {
                var masks = new List<string>()
                {
                    "Last, First Middle",
                    "Last,First Middle",
                    "First Middle Last",
                    "Last, First Middle Suffix",
                    "Last,First Middle Suffix",
                    "First Middle Last Suffix",
                    "Last,First",
                    "First Last",
                    "Last",
                    "Middle",
                    "First"
                };

                return masks;
            }
        }
        public virtual bool AcceptReturn
        {
            get { return _acceptReturn; }
            set
            {
                _acceptReturn = value;
                RaisePropertyChanged(() => AcceptReturn);
            }
        }

        public Field()
        {
            _length = 1;
            _frequency = 1;
            _tagName = string.Empty;
            _description = string.Empty;
            _toolTip = string.Empty;
            _makeUpperCase = true;
            _prefix = string.Empty;
            _suffix = string.Empty;
            _separator = string.Empty;
            Created = DateTime.UtcNow;
            FormatMask = FormatMaskType.Text;

            // Grab string property names for use with IDataErrorInfo
            _tagNamePropertyName = PropertyHelper.GetPropertyName((Field item) => item.TagName);
            _maxLengthPropertyName = PropertyHelper.GetPropertyName((Field item) => item.Length);
            _frequencyPropertyName = PropertyHelper.GetPropertyName((Field item) => item.Frequency);
            _separatorPropertyName = PropertyHelper.GetPropertyName((Field item) => item.Separator);
            _defaultValuePropertyName = PropertyHelper.GetPropertyName((Field item) => item.DefaultValue);
            _transformFormatPropertyName = PropertyHelper.GetPropertyName((Field item) => item.TransformFormat);
        }
        public Field(RecordsCenter recordsCenter): this()
        {
            RecordsCenter = recordsCenter;
        }
        public virtual DateTime Created
        {
            get { return _created; }
            set
            {
                _created = value;
                RaisePropertyChanged(() => Created);
            }
        }
        public virtual DateTime? Updated
        {
            get { return _updated; }
            set
            {
                _updated = value;
                RaisePropertyChanged(() => Updated);
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
        public virtual bool IsHiddenField
        {
            get { return _isHiddenField; }
            set
            {
                _isHiddenField = value;
                RaisePropertyChanged(() => IsHiddenField);
            }
        }
        public virtual string TransformFormat
        {
            get { return _transformFormat; }
            set
            {
                _transformFormat = value;
                RaisePropertyChanged(() => TransformFormat);
            }

        }
        public virtual string TagName
        {
            get { return _tagName; }
            set
            {
                _tagName = value;
                RaisePropertyChanged(() => TagName);
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
        public virtual string ToolTip
        {
            get { return _toolTip; }
            set
            {
                _toolTip = value;
                RaisePropertyChanged(() => ToolTip);
            }
        }
        public virtual bool MakeUpperCase
        {
            get { return _makeUpperCase; }
            set
            {
                _makeUpperCase = value;
                RaisePropertyChanged(() => MakeUpperCase);
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
        public virtual int Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                // Firing both Freq and sep change messages to handle dependant validation between the fields. When Freq > 1 have to have a separator.
                RaisePropertyChanged(() => Frequency);
                RaisePropertyChanged(() => Separator);
            }
        }
        public virtual string Separator
        {
            get { return _separator; }
            set
            {
                _separator = value;
                // Firing both Freq and sep change messages to handle dependant validation between the fields. When Freq > 1 have to have a separator.
                RaisePropertyChanged(() => Separator);
                RaisePropertyChanged(() => Frequency);
            }
        }
        public virtual string Prefix
        {
            get { return _prefix; }
            set
            {
                _prefix = value;
                RaisePropertyChanged(() => Prefix);
            }
        }
        public virtual string Suffix
        {
            get { return _suffix; }
            set
            {
                _suffix = value;
                RaisePropertyChanged(() => Suffix);
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
        public virtual RecordsCenter RecordsCenter
        {
            get { return _recordsCenter; }
            set
            {
                _recordsCenter = value;
                RaisePropertyChanged(() => RecordsCenter);
            }
        }
        public override string ToString()
        {
            return string.Format("{0} ({1})", TagName, Description);
        }
        public static Field CopyField(Field sourceField)
        {
            Field newField = new Field()
            {
                AcceptReturn = sourceField.AcceptReturn,
                DefaultValue = sourceField.DefaultValue,
                Description = sourceField.Description,
                FormatMask = sourceField.FormatMask,
                Frequency = sourceField.Frequency,
                IsHiddenField = sourceField.IsHiddenField,
                Length = sourceField.Length,
                MakeUpperCase = sourceField.MakeUpperCase,
                Prefix = sourceField.Prefix,
                RecordsCenter = sourceField.RecordsCenter,
                Separator = sourceField.Separator,
                Suffix = sourceField.Suffix,
                TagName = sourceField.TagName,
                ToolTip = sourceField.ToolTip,
                TransformFormat = sourceField.TransformFormat,
            };

            return newField;
        }

        #region IDataErrorInfo Members

        public virtual bool IsValid()
        {
            if (this[_tagNamePropertyName] != null) { return false; }
            if (this[_maxLengthPropertyName] != null) { return false; }
            if (this[_frequencyPropertyName] != null) { return false; }
            if (this[_separatorPropertyName] != null) { return false; }
            if (this[_defaultValuePropertyName] != null) { return false; }
            if (this[_transformFormatPropertyName] != null) { return false; }
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
                if (columnName == _tagNamePropertyName)
                {
                    if (TagName.Length > 50)
                    {
                        return "The maximum length of a Tag Name is 50 characters.";
                    }

                    if (string.IsNullOrEmpty(TagName))
                    {
                        return "Tag name is required";
                    }

                    if (TagName.Contains(" "))
                    {
                        return "Tag name may not contain spaces";
                    }

                    if (!Regex.IsMatch(TagName, "^[a-zA-Z0-9]+$", RegexOptions.None))
                    {
                        return "Tag Name can contain letters and numbers only";
                    }
                }
                else if (columnName == _maxLengthPropertyName)
                {
                    if (Length < 1)
                    {
                        return "Max length must be greater than zero";
                    }
                }
                else if (columnName == _frequencyPropertyName)
                {
                    if (Frequency < 1)
                    {
                        return "Frequency must be 1 or greater";
                    }
                }
                else if (columnName == _separatorPropertyName)
                {
                    if (Frequency > 1 && string.IsNullOrWhiteSpace(Separator) == true)
                    {
                        return "A Separator is required when frequency is greater than 1";
                    }
                    else if (Frequency == 1 && string.IsNullOrWhiteSpace(Separator) == false)
                    {
                        return "A Separator is not required when frequency equals 1";
                    }
                }
                else if (columnName == _defaultValuePropertyName)
                {
                    // If this is a numeric field, verify that the default is empty string or numeric
                    if (_formatMask == FormatMaskType.Numeric)
                    {
                        if (string.IsNullOrEmpty(DefaultValue))
                        {
                            return null;
                        }

                        int temp = -1;
                        if (!Int32.TryParse(DefaultValue, out temp))
                        {
                            return "Must be a number";
                        }
                    }
                }
                else if (columnName == _transformFormatPropertyName)
                {
                    if (_formatMask == FormatMaskType.Date || _formatMask == FormatMaskType.Name)
                    {
                        if (String.IsNullOrWhiteSpace(_transformFormat))
                        {
                            return "Requires a format";
                        }
                    }
                }

                return null;
            }
        }

        #endregion
    }
}
