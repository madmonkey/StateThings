using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Id(x => x.Id);
            Map(x => x.Sequence);
            Map(x => x.TransactionName);
            Map(x => x.Description);
            References(x => x.Header);
            References(x => x.RequestForm);
            HasMany(x => x.TxNodes).AsBag().Not.LazyLoad().OrderBy("Sequence").Cascade.AllDeleteOrphan();
            HasMany(x => x.Criterion).AsBag().Not.LazyLoad().OrderBy("Sequence").Cascade.AllDeleteOrphan();
        }
    }
}
