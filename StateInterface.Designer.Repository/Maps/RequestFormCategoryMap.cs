using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class RequestFormCategoryMap : ClassMap<RequestFormCategory>
    {
        public RequestFormCategoryMap()
        {
            Id(x => x.Id);
            References(x => x.Category);
            References(x => x.RequestForm);
        }
    }
}
