using System.ComponentModel;
using System.Collections.Generic;
using System;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class Category : EntityBase, IDataErrorInfo
    {
        public virtual int Id { get; set; }
        public virtual bool IsSystemCategory { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<RequestFormCategory> RequestFormCategories { get; set; }

        public Category()
        {
            RequestFormCategories = new List<RequestFormCategory>();
        }

        #region IDataErrorInfo Members

        public virtual bool IsValid()
        {
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
                return null;
            }
        }

        #endregion
    }
}
