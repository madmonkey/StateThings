using FluentNHibernate.Mapping;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Designer.Repository.Mappings
{
    public class RequestFormProjectionMap : ClassMap<RequestFormProjection>
    {
        public RequestFormProjectionMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.FormId);
            Map(x => x.Title);
            References(x => x.RecordsCenter);
            HasMany(x => x.Categories).AsBag().KeyColumn("RequestForm_Id").ReadOnly();
            Table("[RequestForm]");
        }
    }
}
