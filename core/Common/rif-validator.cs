using System.Text.RegularExpressions;

namespace ProvidersMicroservice.core.Common
{
    public class RifValidator
    {
        private static readonly Regex RifRegExp = new Regex(
            @"^[CEGJPV]\d{8}\d$",
            RegexOptions.Compiled
        );
        public static bool IsValid(string rif)
        {
            if (string.IsNullOrEmpty(rif))
                return false;

            return RifRegExp.IsMatch(rif);
        }
    }
}