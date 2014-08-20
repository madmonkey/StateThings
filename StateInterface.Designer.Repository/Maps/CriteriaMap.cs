using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class CriteriaMap : ClassMap<Criteria>
    {
        public CriteriaMap()
        {
            Id(x => x.Id);
            Map(x => x.Sequence);
            Map(x => x.CriteriaName);
            Map(x => x.Description);
            References(x => x.Transaction);
            HasMany(x => x.QAActions).AsBag().Cascade.AllDeleteOrphan().OrderBy("OccurredAt");
            HasMany(x => x.CriteriaNodes).AsBag().Not.LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}
