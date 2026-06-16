namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.ValueObjects;

public record Ruc(string Value)
{
    public static bool IsValid(string value) =>
        !string.IsNullOrWhiteSpace(value)
        && value.Length == 11
        && value.All(char.IsDigit);
}
