using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Designer.Repository.Mappings
{
    public class TransactionSnippetFieldMap : ClassMap<TransactionSnippetField>
    {
        public TransactionSnippetFieldMap()
        {
            Id(x => x.Id).Column("Id").UniqueKey("UK_SnippetId_TagName");
            Map(x => x.TagName).Not.Nullable().Length(50).UniqueKey("UK_SnippetId_TagName");
            Map(x => x.Prefix).Nullable().Length(50);
            Map(x => x.Suffix).Nullable().Length(50);
            Map(x => x.ToolTip).Nullable().Length(50);
            Map(x => x.MakeUpperCase).CustomSqlType("bit").Default("0").Nullable();
            Map(x => x.AcceptCarriageReturns).CustomSqlType("bit").Default("0").Nullable();
            Map(x => x.Length).Not.Nullable();
            Map(x => x.PadCharacterDec).Nullable();
            Map(x => x.TrimInputToLength).Nullable();
            Map(x => x.DefaultValue).Nullable().Length(10000); //anything over 4000 is varchar(max)
            Map(x => x.TransformFormat).Nullable().Length(50);
            Map(x => x.FormatMask, "FormatMask_Id").CustomType<FormatMaskType>();
            Map(x => x.Frequency).Nullable();
            Map(x => x.Separator).Nullable().Length(5);
        }
    }
}
