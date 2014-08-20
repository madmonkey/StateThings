using FluentNHibernate.Mapping;
using ICA.Domain;
using FluentNHibernate.Conventions;

namespace ICA.Repository.Mappings
{
    public class FieldCriteriaMap : ClassMap<FieldCriteria>
    {
        public FieldCriteriaMap()
        {
            Id(x => x.Id);
            Map(x => x.CheckValue);
            Map(x => x.Condition, "FieldCriteriaCondition_Id").CustomType<FieldCriteriaCondition>();
        }
    }
}
