using FluentNHibernate.Mapping;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Designer.Repository.Mappings
{
    public class CategoryProjectionMap : ClassMap<CategoryProjection>
    {
        public CategoryProjectionMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.Name);
            Table("Category");
        }
    }
}
