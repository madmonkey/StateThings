using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class HeaderNodeMap : ClassMap<HeaderNode>
    {
        public HeaderNodeMap()
        {
            Id(x => x.Id);
            Map(x => x.Sequence);
        }
    }
}
