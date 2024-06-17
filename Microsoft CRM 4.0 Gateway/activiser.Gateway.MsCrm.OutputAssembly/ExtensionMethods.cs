using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using activiser.WebService.CrmOutputGateway.Properties;

namespace ExtensionMethods
{
    public static class GuidExtensions
    {
        static Regex guidRegex = new Regex(Resources.guidRegex);
        
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
    }
}