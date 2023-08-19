using System.Text;

namespace CoreDevextremeTheme.Extensions
{
    public static class StringExtensions
    {

        public static string ToSentenceCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var result = new StringBuilder();

            bool capitalizeNext = true; // ilk harfi büyük yapmak için

            foreach (char c in input)
            {
                if (capitalizeNext && char.IsLetter(c))
                {
                    result.Append(char.ToUpper(c)); // büyük harf ekle
                    capitalizeNext = false; // sonraki harfleri küçük yapmak için
                }
                else
                {
                    result.Append(char.ToLower(c)); // küçük harf ekle
                }

                if (c == '.' || c == '!' || c== '?') // noktalama işareti görürsen
                {
                    capitalizeNext = true; // sonraki harfi büyük yapmak için
                }
            }

            return result.ToString();
        }

        public static string ToProperCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var words = input.Split(' ');
            var result = new StringBuilder();

            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    result.Append(char.ToUpper(word[0]));
                    result.Append(word.Substring(1).ToLower());
                }
                result.Append(' ');
            }

            return result.ToString().Trim();
        }
    }
}
