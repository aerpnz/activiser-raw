//(C)++
//(C) Copyright 2008, Activiser(tm) Limited
//(C)--

using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using activiser.WebService.Properties;
using activiser.WebService.InputGateway;

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

    public static class ResultCodeExtensions {
        public static string Description(this ResultCode value)
        {
            string message;
            switch (value)
            {
                case ResultCode.Success:
                    message = Resources.ResultCodeSuccess; break;
                case ResultCode.EntityNotDefined:
                    message = Resources.ResultCodeEntityNotDefined; break;
                case ResultCode.EntityHasNoAttributes:
                    message = Resources.ResultCodeEntityHasNoAttributes; break;
                case ResultCode.EntityAttributeNotDefined:
                    message = Resources.ResultCodeEntityAttributeNotDefined; break;
                case ResultCode.EntityStructureInvalid:
                    message = Resources.ResultCodeEntityStructureInvalid; break;
                case ResultCode.EntityMissingPrimaryKey:
                    message = Resources.ResultCodeEntityMissingPrimaryKey; break;
                case ResultCode.EntityMissingRequiredAttribute:
                    message = Resources.ResultCodeEntityMissingRequiredAttribute; break;
                case ResultCode.MissingEntityCreated:
                    message = Resources.ResultCodeMissingEntityCreated; break;
                case ResultCode.EntityMissingFromDatabase:
                    message = Resources.ResultCodeEntityMissingFromDatabase; break;
                case ResultCode.ExistingEntityUpdated:
                    message = Resources.ResultCodeExistingEntityUpdated; break;
                case ResultCode.EntityAlreadyExists:
                    message = Resources.ResultCodeEntityAlreadyExists; break;
                case ResultCode.UnableToAddEntity:
                    message = Resources.ResultCodeUnableToAddEntity; break;
                case ResultCode.Unknown:
                default:
                    message = Resources.ResultCodeUnknown;
                    break;
            }
            return string.Format("{0:X}:{1}:{2}", (int) value, value, message);
        }
    }  
}