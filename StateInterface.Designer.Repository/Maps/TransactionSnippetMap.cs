using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Designer.Repository.Mappings
{
    public class TransactionSnippetMap : ClassMap<TransactionSnippet>
    {
        public TransactionSnippetMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.Created).Default("GETUTCDATE()").Nullable();
            Map(x => x.Updated).Default("GETUTCDATE()").Nullable();
            Map(x => x.TokenName).UniqueKey("UK_RecordsCenter_TokenName").Length(50);
            Map(x => x.TransactionDefinition).Length(2000);
            Map(x => x.Criteria).Length(512).Nullable();
            Map(x => x.IncludePrefixAndSuffix).Not.Nullable().CustomSqlType("bit").Default("1");
            Map(x => x.Description).Length(100);
            References(x => x.RecordsCenter).UniqueKey("UK_RecordsCenter_TokenName");
            HasMany(x => x.TransactionSnippetFields).AsBag().Cascade.AllDeleteOrphan();
        }
    }
}
