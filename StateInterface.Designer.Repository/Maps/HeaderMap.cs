using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class HeaderMap : ClassMap<Header>
    {
        public HeaderMap()
        {
            Id(x => x.Id);
            Map(x => x.Created);
            Map(x => x.Updated);
            Map(x => x.HeaderName);
            Map(x => x.Description);
            References(x => x.RecordsCenter);
            HasMany(x => x.HeaderNodes).AsBag().OrderBy("Sequence").Cascade.AllDeleteOrphan();
        }
    }
}
