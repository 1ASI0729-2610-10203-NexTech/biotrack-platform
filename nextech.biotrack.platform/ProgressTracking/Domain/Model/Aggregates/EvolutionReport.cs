using nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

public partial class EvolutionReport
{
    public EvolutionReport() : this(0, 0, DateOnly.MinValue, DateOnly.MinValue)
    {
    }

    public EvolutionReport(int patientUserId, int requestedByUserId, DateOnly periodStart, DateOnly periodEnd)
    {
        PatientUserId = patientUserId;
        RequestedByUserId = requestedByUserId;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        Status = ReportStatus.Generated.ToString();
    }

    public int Id { get; }
    public int PatientUserId { get; private set; }
    public int RequestedByUserId { get; private set; }
    public DateOnly PeriodStart { get; private set; }
    public DateOnly PeriodEnd { get; private set; }
    public string Status { get; private set; }
    public byte[]? PdfContent { get; private set; }

    public EvolutionReport SetGenerated(byte[] pdfContent)
    {
        Status = ReportStatus.Generated.ToString();
        PdfContent = pdfContent;
        return this;
    }

    public EvolutionReport SetInsufficientData()
    {
        Status = ReportStatus.InsufficientData.ToString();
        return this;
    }

    public EvolutionReport SetError()
    {
        Status = ReportStatus.Error.ToString();
        return this;
    }
}
