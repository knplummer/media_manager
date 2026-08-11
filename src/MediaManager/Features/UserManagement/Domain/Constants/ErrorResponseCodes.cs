//this namespace will not follow the folder structure so that it can be shared across the application
namespace MediaManager.ErrorConstants;

public static partial class ErrorResponseCodes
{
    #region User Errors (1001-1999)
    public const int UserDoesNotExist = 1001;
    public const int UserAlreadyExists = 1002;
    #endregion
}
