using StateInterface.Designer.Model;

namespace StateInterface.Areas.Design.Models
{
    public class StyleModel
    {
        public string FontSize { get; set; }
        public string FontWeight { get; set; }
        public string FontStyle { get; set; }
        public string TextAlign { get; set; }
        public string VerticalAlign { get; set; }

        public StyleModel(TextSizes size, bool isBold, bool isItalic, HorizontalAlignment hAlignment, VerticalAlignment vAlignment)
        {
            FontWeight = isBold == false ? "normal" : "bold";
            FontStyle = isItalic == false ? "normal" : "italic";
            switch (size)
            {
                case TextSizes.Large:
                    FontSize = "medium";
                    break;
                case TextSizes.Extralarge:
                    FontSize = "large";
                    break;
                case TextSizes.VeryLarge:
                    FontSize = "x-large";
                    break;
                case TextSizes.Normal:
                default:
                    FontSize = "small";
                    break;
            }

            switch (hAlignment)
            {
                case  HorizontalAlignment.Center:
                    TextAlign = "center";
                    break;
                case HorizontalAlignment.Right:
                    TextAlign = "right";
                    break;
                case HorizontalAlignment.Left:
                default:
                    TextAlign = "left";
                    break;
            }

            switch (vAlignment)
            {
                case VerticalAlignment.Top:
                    VerticalAlign = "top";
                    break;
                case VerticalAlignment.Bottom:
                    VerticalAlign = "bottom";
                    break;
                case VerticalAlignment.Middle:
                default:
                    VerticalAlign = "middle";
                    break;
            }
        }
        public override string ToString()
        {
            return string.Concat(FontSize, " ", FontWeight, " ", FontStyle, " ", TextAlign, " ", VerticalAlign);
        }
    }
}