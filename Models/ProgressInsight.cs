namespace LiftLedger.Services.Models;

public class ProgressInsight
{
    public bool IsNewPR { get; set; }
    public double Delta { get; set; }
    public double PercentChange { get; set; }
    public DateTime FirstDate { get; set; }
    public DateTime LatestDate { get; set; }
    public string InsightText { get; set; } = string.Empty;
}