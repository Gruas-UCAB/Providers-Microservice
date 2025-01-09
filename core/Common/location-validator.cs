using System.Text.RegularExpressions;

namespace ProvidersMicroservice.core.Common
{
    public class LocationValidator
    {
        public static bool IsValid(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            var regex = new Regex(@"^(\-?\d+(\.\d+)?),\s*(\-?\d+(\.\d+)?)$");
            var match = regex.Match(location);

            if (!match.Success)
            {
                return false;
            }

            if (double.TryParse(match.Groups[1].Value, out double latitude) &&
                double.TryParse(match.Groups[3].Value, out double longitude))
            {
                return latitude >= -90 && latitude <= 90 && longitude >= -180 && longitude <= 180;
            }

            return false;
        }
    }
}
