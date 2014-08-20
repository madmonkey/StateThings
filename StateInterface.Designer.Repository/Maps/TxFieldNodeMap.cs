using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class TxFieldNodeMap : SubclassMap<TxFieldNode>
    {
        public TxFieldNodeMap()
        {
            Map(x => x.Prefix);
            Map(x => x.Suffix);
            Map(x => x.TransformFormat);
            Map(x => x.TrimInputToLength);
            Map(x => x.PadCharacter);
            References(x => x.FormField).Not.LazyLoad();
        }
    }
}
