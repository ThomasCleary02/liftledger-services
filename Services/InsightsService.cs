using LiftLedger.Services.Models;

namespace LiftLedger.Services.Services;

public class InsightsService
{
    public ProgressInsight AnalyzeProgress(ProgressRequest request)
    {
        // Validate input
        if (request.History == null || request.History.Count == 0)
        {
            throw new ArgumentException("History cannot be empty", nameof(request));
        }

        // Sort by date
        var sortedHistory = request.History.OrderBy(h => h.Date).ToList();

        // Handle single point explicitly
        if (sortedHistory.Count == 1) 
        {
            var point = sortedHistory[0];
            return new ProgressInsight
            {
                IsNewPR = true,
                Delta = 0,
                PercentChange = 0,
                FirstDate = point.Date,
                LatestDate = point.Date,
                InsightText = $"First logged {request.Exercise}: {point.Value} {request.Metric}."
            };
        }

        // Multi-point logic
        var first = sortedHistory.First();
        var latest = sortedHistory.Last();
        var delta = latest.Value - first.Value;

        // Safe percent change calculation
        double percentChange = 0;
        if (first.Value > 0)
        {
            percentChange = (delta / first.Value) * 100;
        }

        // PR detection: latest > max of all previous values
        var previousValues = sortedHistory.Take(sortedHistory.Count - 1).Select(p => p.Value);
        var isNewPR = !previousValues.Any() || latest.Value > previousValues.Max();

        // Generate smart insight text
        string insightText;

        // Check if all values are the same (for early logging detection)
        var allValuesSame = sortedHistory.All(h => Math.Abs(h.Value - first.Value) < 0.001);

        // Early logging detection: handle consecutive same-value logs (2-3 points)
        if (delta == 0 && sortedHistory.Count <= 3 && allValuesSame)
        {
            insightText = $"Keep logging to see your progress!";
        }
        else if (isNewPR && sortedHistory.Count > 1)
        {
            insightText = $"New PR! You hit {latest.Value} {request.Metric} on {request.Exercise}.";
        }
        else if (delta > 0)
        {
            insightText = $"You've increased your {request.Exercise} by {delta} {request.Metric} since {first.Date:MMMM}";
        }
        else if (delta == 0)
        {
            insightText = $"Your {request.Exercise} has stayed consistent since {first.Date:MMMM}";
        }
        else
        {
            insightText = $"Your {request.Exercise} is down {Math.Abs(delta)} {request.Metric} since {first.Date:MMMM}";
        }

        return new ProgressInsight
        {
            IsNewPR = isNewPR,
            Delta = delta,
            PercentChange = Math.Round(percentChange, 1),
            FirstDate = first.Date,
            LatestDate = latest.Date,
            InsightText = insightText
        };
    }
}