using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class OptionElementMap : SubclassMap<OptionElement>
    {
        public OptionElementMap()
        {
            References(x => x.Field);
        }
    }
}
