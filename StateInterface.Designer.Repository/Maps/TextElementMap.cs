using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class TextElementMap : SubclassMap<TextElement>
    {
        public TextElementMap()
        {
            References(x => x.Field);
        }
    }
}
