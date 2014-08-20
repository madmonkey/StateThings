using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using StateInterface.Designer.Model.Helper;
using System.Text.RegularExpressions;

namespace StateInterface.Designer.Model.Projections
{
    public class RequestFormProjection
    {
        public virtual int Id { get; set; }
        public virtual string FormId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual RecordsCenter RecordsCenter { get; set; }
        public virtual IList<RequestFormCategory> RequestFormCategories { get; set; }
    }
}
