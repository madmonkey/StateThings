using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class ApplicationFormProjectionMap : ClassMap<ApplicationFormProjection>
    {
        public ApplicationFormProjectionMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.FormId);
            Map(x => x.Description);
            Map(x => x.Title);
            References(x => x.RecordsCenter).Column("RecordsCenter_Id").ReadOnly();
            HasMany(x => x.RequestFormCategories).AsBag().KeyColumn("RequestForm_Id").ReadOnly();
            HasManyToMany(x => x.Applications).AsBag().Cascade.All().Table("RequestFormApplication").ParentKeyColumn("RequestForm_Id");
            Table("RequestForm");
        }
    }
}


