using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class OptionListItemMap : ClassMap<OptionListItem>
    {
        public OptionListItemMap()
        {
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.Description);
            Map(x => x.Sequence);
            References(x => x.OptionListTier);
            HasMany(x => x.OptionListItems).AsBag().OrderBy("Sequence").Cascade.AllDeleteOrphan().BatchSize(1000);
        }
    }
}
