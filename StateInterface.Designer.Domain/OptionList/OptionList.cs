using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using StateInterface.Designer.Model.Helper;
using System.Text.RegularExpressions;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class OptionList : EntityBase, IDataErrorInfo
    {
        private int _id;
        private string _listName = string.Empty;
        RecordsCenter _recordsCenter;
        private DateTime _created;
        private DateTime? _updated;
        private IList<OptionListTier> _optionListTiers;
        private IList<OptionListItem> _optionListItems;
        private string _errorMessage;
        private string _listNamePropertyName;

        public virtual IList<OptionListTier> OptionListTiers
        {
            get { return _optionListTiers; }
            set
            {
                _optionListTiers = value;
                RaisePropertyChanged(() => OptionListTiers);
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

        public OptionList()
        {
            _optionListTiers = new List<OptionListTier>();
            _optionListItems = new List<OptionListItem>();
            Created = DateTime.UtcNow;

            // Grab string property names for use with IDataErrorInfo
            _listNamePropertyName = PropertyHelper.GetPropertyName((OptionList item) => item.ListName);
        }
        public OptionList(RecordsCenter recordsCenter)
            : this()
        {
            RecordsCenter = recordsCenter;
            _optionListTiers.Add(new OptionListTier() { OptionList = this });
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
        public virtual string ListName
        {
            get { return _listName; }
            set
            {
                _listName = value;
                RaisePropertyChanged(() => ListName);
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
        public virtual bool HasErrors
        {
            get { return ErrorMessage != null; }
        }
        public virtual void RemoveNonPrintableAndTrim()
        {
            // Removes not printable characters and white space from codes and descriptions
            foreach (var item in OptionListItems)
            {
                item.RemoveNonPrintableAndTrim();
            }

            List<OptionListItem> emptyRows = OptionListItems.Where(x => string.IsNullOrWhiteSpace(x.Code) == true
                && string.IsNullOrWhiteSpace(x.Description) == true).ToList();

            // Removes empty rows from the list
            foreach (var item in emptyRows)
            {
                OptionListItems.Remove(item);
            }
        }
        public static OptionList CopyOptionList(OptionList sourceOptionList)
        {
            OptionList newOptionList = new OptionList()
            {
                ListName = sourceOptionList.ListName,
                RecordsCenter = sourceOptionList.RecordsCenter,
            };

            foreach (var listItem in sourceOptionList.OptionListItems)
            {
                OptionListItem newOptionListItem = new OptionListItem()
                {
                    OptionListTier = listItem.OptionListTier,
                    Code = listItem.Code,
                    Description = listItem.Description
                };

                newOptionList.OptionListItems.Add(newOptionListItem);
            }

            foreach (var listItem in sourceOptionList.OptionListTiers)
            {
                OptionListTier newOptionListTier = new OptionListTier()
                {
                    Name = listItem.Name,
                    OptionList = newOptionList
                };

                newOptionList.OptionListTiers.Add(newOptionListTier);
            }

            return newOptionList;
        }

        public override string ToString()
        {
            return ListName;
        }


        #region IDataErrorInfo Members

        public virtual bool IsValid()
        {
            if (this[_listNamePropertyName] != null || !AreTiersNamesValid() || !AreTiersNamesUnique()) { return false; }
            foreach (var tier in OptionListTiers)
            {
                if (!tier.IsValid())
                {
                    return false;
                }
            }
            return true;
        }

        public virtual string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
                RaisePropertyChanged(() => HasErrors);
            }
        }

        public virtual string Error { get { return null; } }

        public virtual string this[string propertyName]
        {
            get
            {
                if (propertyName == _listNamePropertyName)
                {
                    if (ListName.Length > 50)
                    {
                        return "The maximum length of a List Name is 50 characters.";
                    }

                    if (string.IsNullOrEmpty(ListName))
                    {
                        return "A List Name is required";
                    }

                    if (ListName.Contains(" "))
                    {
                        return "List Name may not contain spaces";
                    }

                    if (!Regex.IsMatch(ListName, "^[a-zA-Z0-9]+$", RegexOptions.None))
                    {
                        return "List Name can contain letters and numbers only";
                    }
                }

                return null;
            }
        }
        public virtual void Validate()
        {
            if (!AreTiersNamesUnique())
            {
                ErrorMessage = "Tier Names must be Unique";
            }

            else
            {
                ErrorMessage = null;
            }
        }
        public virtual bool AreTiersNamesValid()
        {
            foreach (var tier in OptionListTiers)
            {
                if (!tier.IsValid())
                {
                    return false;
                }
            }
            return true;
        }
        public virtual bool AreTiersNamesUnique()
        {
            foreach (var tier in OptionListTiers)
            {
                foreach (var otherTier in OptionListTiers.Where(x => x != tier))
                {
                    if (tier.Name == otherTier.Name)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public virtual IList<OptionListItem> ValidateItems(IList<OptionListItem> items)
        {
            var invalidItems = new List<OptionListItem>();

            //TODO: Add threading capabilities (Parallel.ForEach()?)
            foreach (var item in items)
            {
                if ((!String.IsNullOrWhiteSpace(item.Description) && String.IsNullOrWhiteSpace(item.Code)) || (String.IsNullOrWhiteSpace(item.Description) && !String.IsNullOrWhiteSpace(item.Code)))
                {
                    invalidItems.Add(item);
                }
                else if (String.IsNullOrWhiteSpace(item.Description) && String.IsNullOrWhiteSpace(item.Code) && item.OptionListItems.Any())
                {
                    invalidItems.Add(item);
                }

                invalidItems.AddRange(ValidateItems(item.OptionListItems));
            }

            return invalidItems;
        }
        public virtual List<OptionListItem> ValidateHierarchy(IList<OptionListItem> items, List<OptionListItem> childlessItems)
        {
            foreach (var item in items)
            {
                if (!item.OptionListItems.Any() && item.OptionListTier != OptionListTiers.Last())
                {
                    childlessItems.Add(item);
                }

                ValidateHierarchy(item.OptionListItems, childlessItems);
            }

            return childlessItems;
        }

        #endregion
    }
}
