using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class LabelElementMap : SubclassMap<LabelElement>
    {
        public LabelElementMap()
        {
            Map(x => x.Text);
        }
    }
}
