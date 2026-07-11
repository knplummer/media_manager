namespace MediaManager.Shared.Domain.Enums;

public enum ErrorResponseCodes
{
    #region Shared Application Errors (1-999)
    InternalError = 1,
    MissingTransactionId = 2,
    MissingSource = 3,
    MissingTimestamp = 4
    #endregion
}