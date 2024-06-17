namespace activiser.WebService
{

    public enum ActiviserAttributeType : int
    {
        Unknown = -1,
        Guid = 0,
        Integer = 1,
        String = 2,
        DateTime = 3,
        Float = 4,
        Boolean = 5,
        Byte = 6,
        Currency = 7,
        BigInt = 8,
        Decimal = 9,
        Short = 10,
        Timestamp = 11,
        DateOnly = 12,
        TimeOnly = 13,
        GuidPK = 100,
        IntegerPK = 101,
        BigIntPK = 102,
        GuidFK = 200,
        IntegerFK = 201,
        BigIntFK = 202,
        EmailAddress = 1010,
        WebAddress = 1011,
        Address = 1012,
        PhoneNumber = 1013,
        Password = 1014
    }

}