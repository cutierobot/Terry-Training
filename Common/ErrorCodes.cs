using System.Diagnostics;

namespace Common;
public static class ErrorCodes
{
    public static readonly string None = string.Empty;
    public static readonly string Exception = "Exception";
    public static readonly string NullValue = "NulLValue";
    public static readonly string TaskUndefined = "TaskUndefined";
    public static readonly string FileNotFound = "FileNotFound";
    public static readonly string FileNotWritten = "FileNotWritten";
    public static readonly string FileCopy = "FileCopy";
    public static readonly string FileRead = "FileRead";
    public static readonly string ProcessingError = "ProcessingError";
    public static readonly string DatabaseError = "DatabaseError";
    public static readonly string InvalidFilenameFormat = "InvalidFilenameFormat";
    public static readonly string ValidationError = "ValidationError";
    public static readonly string DuplicateError = "DuplicateError";

}