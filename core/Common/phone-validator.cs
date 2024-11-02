using System;
using System.Text.RegularExpressions;

namespace UsersMicroservice.Core.Common
{
    public static class PhoneNumberValidator
    {
        private static readonly Regex PhoneNumberRegExp = new Regex(
            @"^\+?[1-9]\d{1,14}$",
            RegexOptions.Compiled
        );

        public static bool IsValid(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            return PhoneNumberRegExp.IsMatch(phoneNumber);
        }
    }
}