namespace KickBlastStudentForms.Helpers;

public static class ValidationHelper
{
    public static bool IsRequired(string? value) => !string.IsNullOrWhiteSpace(value);

    public static bool IsDecimal(string? value, out decimal result)
    {
        return decimal.TryParse(value, out result);
    }
}
