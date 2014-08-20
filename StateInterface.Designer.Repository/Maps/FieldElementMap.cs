using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class FieldElementMap : SubclassMap<FieldElement>
    {
        public FieldElementMap()
        {
            References(x => x.FormField);
        }
    }
}
