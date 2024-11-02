using System.Text.RegularExpressions;

namespace UsersMicroservice.Core.Domain
{
    public static class UUIDValidator
    {
        private static readonly Regex UUIDRegExp = new Regex(
            "^[0-9A-Za-z]{8}-[0-9A-Za-z]{4}-4[0-9A-Za-z]{3}-[89ABab][0-9A-Za-z]{3}-[0-9A-Za-z]{12}$",
            RegexOptions.Compiled
        );

        public static bool IsValid(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            return UUIDRegExp.IsMatch(id);
        }
    }
}