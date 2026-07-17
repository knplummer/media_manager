using System.Reflection;

//this namespace will not follow the folder structure so that it can be shared across the application
namespace MediaManager.ErrorConstants;

public static partial class ErrorResponseCodes
{
    #region Shared Application Errors (1-999)
    public const int InternalError = 1;
    public const int MissingEventId = 2;
    public const int MissingSource = 3;
    public const int MissingTimestamp = 4;
    #endregion

    public static KeyValuePair<int, string> ToResponseCode(this int code)
    {
        var properties = typeof(ErrorResponseCodes).GetProperties(BindingFlags.Public | BindingFlags.Static);
        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(int) && (int)property.GetValue(null)! == code)
            {
                return new KeyValuePair<int, string>(code, property.Name);
            }
        }

        var fields = typeof(ErrorResponseCodes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(int) && (int)field.GetValue(null)! == code)
            {
                return new KeyValuePair<int, string>(code, field.Name);
            }
        }

        return new KeyValuePair<int, string>(1, "Internal Error");
    }
}