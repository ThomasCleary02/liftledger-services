# LiftLedger Insights Service

A stateless ASP.NET Core service that computes progress insights from exercise history data.

## Features

- **Progress Analysis**: Detects PRs, calculates deltas, and percent changes
- **Smart Insights**: Generates contextual messages based on progress patterns
- **Early Logging Detection**: Handles consecutive same-value logs gracefully
- **Health Checks**: Built-in health endpoint for monitoring

## API Endpoints

### POST `/api/insights/progress`

Analyzes exercise history and returns progress insights.

**Request:**
{
  "exercise": "Bench Press",
  "history": [
    { "date": "2024-12-01", "value": 135 },
    { "date": "2025-01-10", "value": 155 },
    { "date": "2025-03-16", "value": 165 }
  ],
  "metric": "weight"
}**Response:**
{
  "isNewPR": true,
  "delta": 30,
  "percentChange": 22.2,
  "firstDate": "2024-12-01T00:00:00",
  "latestDate": "2025-03-16T00:00:00",
  "insightText": "New PR! You hit 165 weight on Bench Press."
}### GET `/health`

Health check endpoint for monitoring.

## Local Development

# Restore dependencies
dotnet restore

# Run the application
dotnet run

# Or use watch mode for auto-reload
dotnet watch runThe API will be available at `http://localhost:5068` (HTTP) or `https://localhost:7258` (HTTPS).

## Testing

Use the `liftledger-services.http` file with the REST Client extension, or test with:
h
curl -X POST http://localhost:5068/api/insights/progress \
  -H "Content-Type: application/json" \
  -d '{"exercise":"Bench Press","history":[{"date":"2024-12-01","value":135}],"metric":"weight"}'## Deployment

This project is configured for deployment to Azure App Service via GitHub Actions.

See `.github/workflows/azure-deploy.yml` for deployment configuration.

## Technology Stack

- .NET 10.0
- ASP.NET Core Web API
- C# 12

## License

MIT