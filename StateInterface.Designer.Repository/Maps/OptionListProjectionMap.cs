using FluentNHibernate.Mapping;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Designer.Repository.Mappings
{
    public class OptionListProjectionMap : ClassMap<ListProjection>
    {
        public OptionListProjectionMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.ListName);
            References(x => x.RecordsCenter);
            Table("[OptionList]");
        }
    }
}
