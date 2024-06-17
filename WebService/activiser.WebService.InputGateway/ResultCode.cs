//(C)++
//(C) Copyright 2008, Activiser(tm) Limited
//(C)--

namespace activiser.WebService.InputGateway
{
    public enum ResultCode // : byte
    {
        Unknown,
        Success, // no error
        EntityNotDefined,
        EntityHasNoAttributes,
        EntityAttributeNotDefined,
        EntityStructureInvalid,
        EntityMissingPrimaryKey,
        EntityMissingRequiredAttribute,
        MissingEntityCreated,
        EntityMissingFromDatabase,
        ExistingEntityUpdated,
        EntityAlreadyExists,
        UnableToAddEntity,
        UnableToUpdateEntity,

    }
}
