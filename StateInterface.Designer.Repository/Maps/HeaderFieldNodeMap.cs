using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class HeaderFieldNodeMap : SubclassMap<HeaderFieldNode>
    {
        public HeaderFieldNodeMap()
        {
            Map(x => x.Prefix);
            Map(x => x.Suffix);
            Map(x => x.PadCharacter);
            Map(x => x.Length);
            References(x => x.Field);
        }
    }
}
