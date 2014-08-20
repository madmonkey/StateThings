using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class TxTextNodeMap : SubclassMap<TxTextNode>
    {
        public TxTextNodeMap()
        {
            Map(x => x.Text);
            Map(x => x.TextNodeType, "TextNodeType_Id").CustomType<TextNodeType>();
        }
    }
}
