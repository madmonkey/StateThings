using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class CriteriaNodeMap : ClassMap<CriteriaNode>
    {
        public CriteriaNodeMap()
        {
            Id(x => x.Id);
			Map(x => x.CheckValue);
			Map(x => x.Condition, "FieldCriteriaCondition_Id").CustomType<FieldCriteriaCondition>();
            References(x => x.FormField).Not.LazyLoad();
            
        }
    }
}
