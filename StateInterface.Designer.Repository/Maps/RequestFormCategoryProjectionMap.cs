using FluentNHibernate.Mapping;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Designer.Repository.Mappings
{
    public class RequestFormCategoryProjectionMap : ClassMap<RequestFormCategoryProjection>
    {
        public RequestFormCategoryProjectionMap()
        {
            Id(x => x.Id).Column("Id");
            References(x => x.RequestForm).Column("RequestForm_Id").ReadOnly();
            References(x => x.Category).Column("Category_Id").ReadOnly();
            Table("[RequestFormCategory]");
        }
    }
}
