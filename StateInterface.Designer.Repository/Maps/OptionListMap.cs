using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class OptionListMap : ClassMap<OptionList>
    {
        public OptionListMap()
        {
            Id(x => x.Id);
            Map(x => x.Created);
            Map(x => x.Updated);
            Map(x => x.ListName);
            References(x => x.RecordsCenter);
            HasMany(x => x.OptionListTiers).AsBag().OrderBy("Sequence").Cascade.AllDeleteOrphan().BatchSize(10);
            HasMany(x => x.OptionListItems).AsBag().OrderBy("Sequence").Cascade.AllDeleteOrphan().BatchSize(1000);
        }
    }
}
