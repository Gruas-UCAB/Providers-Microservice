using System.Text.RegularExpressions;

namespace ProvidersMicroservice.core.Common;
public static class CranePlateValidator
{
    public static bool IsValid(string plate)
    {
        if (string.IsNullOrEmpty(plate))
        {
            return false;
        }
        string pattern = @"^[A-Z]\d{2}[A-Z]\d{2}[A-Z]$";
        return Regex.IsMatch(plate.ToUpper(), pattern);
    }
}