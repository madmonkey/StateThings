using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class ApplicationMap: ClassMap<Application>
    {
        public ApplicationMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}
