using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class StyleDefinitionMap : ComponentMap<StyleDefinition>
    {
        public StyleDefinitionMap()
        {
            Map(x => x.TextSize, "TextSize_Id").CustomType<TextSizes>();
            Map(x => x.HorzAlignment, "HorizontalAlignment_Id").CustomType<HorizontalAlignment>();
            Map(x => x.VertAlignment, "VerticalAlignment_Id").CustomType<VerticalAlignment>();
            Map(x => x.IsBold);
            Map(x => x.IsItalic);
        }
    }
}
