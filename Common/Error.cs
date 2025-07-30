namespace Common;

public record Error(string Code, string? Message)
{
    public static Error None = new(ErrorCodes.None, string.Empty);
    public static Error NullValue = new(ErrorCodes.NullValue, "Return value is null.");
}
