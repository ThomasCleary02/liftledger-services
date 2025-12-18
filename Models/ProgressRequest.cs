namespace LiftLedger.Services.Models;

public class ProgressRequest 
{
    public string Exercise { get; set; } = string.Empty;
    public string Metric { get; set; } = string.Empty;
    public List<ProgressPoint> History { get; set; } = new();
}