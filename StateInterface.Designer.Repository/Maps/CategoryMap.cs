using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id);
            Map(x => x.IsSystemCategory);
            Map(x => x.Name);
        }
    }
}
