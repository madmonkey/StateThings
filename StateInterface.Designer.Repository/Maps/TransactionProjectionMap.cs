using FluentNHibernate.Mapping;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Designer.Repository.Mappings
{
    public class TransactionProjectionMap : ClassMap<TransactionProjection>
    {
        public TransactionProjectionMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.Sequence);
            Map(x => x.TransactionName);
            Map(x => x.Description);
            References(x => x.RequestForm).Column("RequestForm_Id");
            HasMany(x => x.Criterion).AsBag().ReadOnly().OrderBy("Sequence").KeyColumn("Transaction_Id");
            Table("[Transaction]");
        }
    }
}
