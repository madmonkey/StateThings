using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    public enum TextSizes
    {
        Normal,
        Large,
        Extralarge,
        VeryLarge
    }

    public enum VerticalAlignment
    {
        Middle,
        Top,
        Bottom
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    [Serializable]
    public class StyleDefinition : EntityBase
    {
        public bool IsBold { get; set; }

        public bool IsItalic { get; set; }

        public TextSizes TextSize { get; set; }

        public VerticalAlignment VertAlignment { get; set; }

        public HorizontalAlignment HorzAlignment { get; set; }

        public StyleDefinition()
        {
            TextSize = TextSizes.Normal;
            HorzAlignment = HorizontalAlignment.Left;
            VertAlignment = VerticalAlignment.Middle;
        }
        public StyleDefinition(StyleDefinition sourceStyleDefinition)
        {
            IsBold = sourceStyleDefinition.IsBold;
            IsItalic = sourceStyleDefinition.IsItalic;
            TextSize = sourceStyleDefinition.TextSize;
            VertAlignment = sourceStyleDefinition.VertAlignment;
            HorzAlignment = sourceStyleDefinition.HorzAlignment;
        }
    }
}
