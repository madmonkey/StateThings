using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class RequestFormMap : ClassMap<RequestForm>
    {
        public RequestFormMap()
        {
            Id(x => x.Id);
            Map(x => x.Created);
            Map(x => x.Updated);
            Map(x => x.FormId);
            Map(x => x.Description);
            Map(x => x.Title);
            Map(x => x.Version);
            Map(x => x.TableRowCount);
            Map(x => x.TableColumnCount);
            Map(x => x.IncludeFieldPrefixAndSuffix);
            References(x => x.RecordsCenter);
            HasMany(x => x.LabelElements).AsBag().OrderBy("Sequence").Cascade.AllDeleteOrphan();
            HasMany(x => x.FieldElements).AsBag().OrderBy("Sequence").Cascade.AllDeleteOrphan();
            HasMany(x => x.FormFields).AsBag().OrderBy("Sequence").Cascade.AllDeleteOrphan();
            HasMany(x => x.Transactions).AsBag().OrderBy("Sequence").Cascade.AllDeleteOrphan();
            HasMany(x => x.RequestFormCategories).AsBag().Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.Applications).AsBag().Cascade.All().Table("RequestFormApplication");

            Map(x => x.SubmissionMode, "SubmissionMode_Id").CustomType<SubmissionMode>();
        }
    }
}
