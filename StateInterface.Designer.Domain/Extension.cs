using System.Text;

namespace StateInterface.Designer.Model
{
    public static class Extension
    {
        public static string RemoveNonPrintable(this string s)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c >= 32 && c <= 126)
                {
                    result.Append(c);
                }
            }
            return result.ToString().Trim();
        }
    }
}
