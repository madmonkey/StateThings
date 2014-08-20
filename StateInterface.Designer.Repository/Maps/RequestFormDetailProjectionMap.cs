using FluentNHibernate.Mapping;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Designer.Repository.Mappings
{
    public class RequestFormDetailProjectionMap : ClassMap<RequestFormDetailProjection>
    {
        public RequestFormDetailProjectionMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.FormId);
            Map(x => x.Description);
            Map(x => x.Title);
            References(x => x.RecordsCenter).Column("RecordsCenter_Id").ReadOnly();
            HasMany(x => x.Transactions).AsBag().KeyColumn("RequestForm_Id").ReadOnly();
            HasMany(x => x.RequestFormCategories).AsBag().KeyColumn("RequestForm_Id").ReadOnly();
            HasManyToMany(x => x.Applications).AsBag().Cascade.All().Table("RequestFormApplication").ParentKeyColumn("RequestForm_id");
            Table("RequestForm");
        }
    }
}
