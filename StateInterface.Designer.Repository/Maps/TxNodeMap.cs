using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class TxNodeMap : ClassMap<TxNode>
    {
        public TxNodeMap()
        {
            Id(x => x.Id);
            Map(x => x.Sequence);
        }
    }
}
