namespace activiser.InputGateway.CrmPlugIn
{
    public enum ErrorCode
    {
        None = 0,
        NoGateway = 1,
        StartTransactionException = 1000,
        FinishTransactionException = 2000,
        CreateEntityException = 3000,
        UpdateEntityException = 4000,
        AssignEntityException = 5000,
        RouteEntityException = 6000,
        SetEntityStateException = 7000,
        DeleteEntityException = 8000,
    }
}