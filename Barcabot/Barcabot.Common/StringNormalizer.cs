using System.Globalization;
using System.Linq;
using System.Text;

namespace Barcabot.Common
{
    public static class StringNormalizer
    {
        public static string Normalize(string input)
        {
            return string.Concat(input.Normalize(NormalizationForm.FormD).Where(
                    c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }
    }
}