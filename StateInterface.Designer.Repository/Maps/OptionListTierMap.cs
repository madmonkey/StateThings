using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class OptionListTierMap : ClassMap<OptionListTier>
    {
        public OptionListTierMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Sequence);
            References(x => x.OptionList);
        }
    }
}
