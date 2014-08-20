using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class RecordsCenterMap : ClassMap<RecordsCenter>
    {
        public RecordsCenterMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            HasMany(x => x.RequestForms).AsBag().Cascade.AllDeleteOrphan();
            HasMany(x => x.OptionLists).AsBag().Cascade.AllDeleteOrphan();
            HasMany(x => x.Fields).AsBag().Cascade.AllDeleteOrphan();
            HasMany(x => x.Headers).AsBag().Cascade.AllDeleteOrphan();
        }
    }
}
