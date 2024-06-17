using System;
using System.Text.RegularExpressions;

namespace activiser.WebService
{
    public static class activiserExtensions
    {
        private const string guidRegexPattern = @"^[A-F0-9]{32}$|^[A-F0-9]{8}-([A-F0-9]{4}-){3}[A-F0-9]{12}$|^{[A-F0-9]{8}-([A-F0-9]{4}-){3}[A-F0-9]{12}}?$|^\(?[A-F0-9]{8}-([A-F0-9]{4}-){3}[A-Fa-f0-9]{12}\)?$|^({)?[0xA-F0-9]{3,10}(,{0,1}[0xA-F0-9]{3,6}){2},{0,1}({)([0xA-F0-9]{3,4}, {0,1}){7}[0xA-F0-9]{3,4}(}})$";
        static Regex guidRegex = new Regex(guidRegexPattern, RegexOptions.CultureInvariant|RegexOptions.IgnoreCase);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        public static bool IsGuid(this string value, out Guid result)
        {
            if (guidRegex.Match(value).Captures.Count == 1)
            {
                result = new Guid(value);
                return true;
            }
            else
            {
                result = Guid.Empty;
                return false;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        public static bool IsGuid(this string value, out Guid? result)
        {
            if (guidRegex.Match(value).Captures.Count == 1)
            {
                result = new Guid(value);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        public static Guid? ParseGuid(this string value)
        {
            return (guidRegex.Match(value).Captures.Count == 1) ? new Guid(value) : (Guid?) null;
        }
    }
}