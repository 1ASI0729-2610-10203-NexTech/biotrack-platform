namespace nextech.biotrack.platform.PatientProfile.Domain.Model.ValueObjects;

public record Imc(decimal Value)
{
    public static Imc Calculate(decimal weightKg, decimal heightCm)
    {
        var heightM = heightCm / 100m;
        var value = Math.Round(weightKg / (heightM * heightM), 2);
        return new Imc(value);
    }

    public string Category => Value switch
    {
        < 18.5m => "Underweight",
        < 25.0m => "Normal",
        < 30.0m => "Overweight",
        _ => "Obese"
    };

    public static bool IsValidInputs(decimal weightKg, decimal heightCm) =>
        weightKg > 0 && heightCm > 0;
}
