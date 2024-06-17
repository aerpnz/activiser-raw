namespace activiser.WebService
{
    public sealed partial class Utilities
    {
        private Utilities() { }

        public static ActiviserAttributeType GetActiviserTypeCode(string crmDataType)
        {
            if (crmDataType == "ntext" || crmDataType == "nvarchar" || crmDataType == "state" || crmDataType == "text")
                return ActiviserAttributeType.String;
            if (crmDataType == "int")
                return ActiviserAttributeType.Integer;
            if (crmDataType == "float")
                return ActiviserAttributeType.Float;
            if (crmDataType == "bit")
                return ActiviserAttributeType.Boolean;
            if (crmDataType == "datetime")
                return ActiviserAttributeType.DateTime;
            if (crmDataType == "dateonly")
                return ActiviserAttributeType.DateOnly;
            if (crmDataType == "timeonly")
                return ActiviserAttributeType.TimeOnly;
            if (crmDataType == "primarykey")
                return ActiviserAttributeType.GuidPK;
            if (crmDataType == "customer" || crmDataType == "lookup" || crmDataType == "owner")
                return ActiviserAttributeType.GuidFK;
            if (crmDataType == "decimal")
                return ActiviserAttributeType.Decimal;
            if (crmDataType == "money")
                return ActiviserAttributeType.Currency;
            if (crmDataType == "picklist" || crmDataType == "status")
                return ActiviserAttributeType.IntegerFK;
            if (crmDataType == "smallint")
                return ActiviserAttributeType.Short;
            if (crmDataType == "timestamp")
                return ActiviserAttributeType.Timestamp;
            if (crmDataType == "tinyint")
                return ActiviserAttributeType.Byte;
            if (crmDataType == "uniqueidentifier")
                return ActiviserAttributeType.Guid;
            if (crmDataType == "partylist" || crmDataType == "virtual")
                return ActiviserAttributeType.Unknown;
            if (crmDataType == "bigint")
                return ActiviserAttributeType.BigInt;
            return ActiviserAttributeType.Unknown;
        }
    }
}