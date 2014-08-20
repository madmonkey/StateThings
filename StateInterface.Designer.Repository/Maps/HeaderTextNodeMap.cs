using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Repository.Mappings
{
    public class HeaderTextNodeMap : SubclassMap<HeaderTextNode>
    {
        public HeaderTextNodeMap()
        {
            Map(x => x.Text);
            Map(x => x.TextNodeType, "TextNodeType_Id").CustomType<TextNodeType>();
        }
    }
}
