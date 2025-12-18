# LiftLedger Insights Service

A stateless ASP.NET Core service that computes progress insights from exercise history data.

**Status:** ✅ v1.0 Complete and Deployed to Production

**Production URL:** https://liftledgerservices-e2bcfshcf6frfycb.centralus-01.azurewebsites.net

## Features

- **Progress Analysis**: Detects PRs, calculates deltas, and percent changes
- **Smart Insights**: Generates contextual messages based on progress patterns
- **Early Logging Detection**: Handles consecutive same-value logs gracefully (2-3 points within 7 days)
- **Health Checks**: Built-in health endpoint for monitoring
- **Production Ready**: Error handling, logging, and CORS support

## API Endpoints

### POST `/api/insights/progress`

Analyzes exercise history and returns progress insights.

**Production URL:**
```
https://liftledgerservices-e2bcfshcf6frfycb.centralus-01.azurewebsites.net/api/insights/progress
```

**Request:**
```json
{
  "exercise": "Bench Press",
  "history": [
    { "date": "2024-12-01", "value": 135 },
    { "date": "2025-01-10", "value": 155 },
    { "date": "2025-03-16", "value": 165 }
  ],
  "metric": "weight"
}
```

**Response:**
```json
{
  "isNewPR": true,
  "delta": 30,
  "percentChange": 22.2,
  "firstDate": "2024-12-01T00:00:00",
  "latestDate": "2025-03-16T00:00:00",
  "insightText": "New PR! You hit 165 weight on Bench Press."
}
```

### GET `/health`

Health check endpoint for monitoring.

**Production URL:**
```
https://liftledgerservices-e2bcfshcf6frfycb.centralus-01.azurewebsites.net/health
```

Returns: `Healthy`

## Local Development

```bash
# Restore dependencies
dotnet restore

# Run the application
dotnet run

# Or use watch mode for auto-reload
dotnet watch run
```

The API will be available at `http://localhost:5068` (HTTP) or `https://localhost:7258` (HTTPS).

## Testing

Use the `liftledger-services.http` file with the REST Client extension, or test with:

```bash
curl -X POST http://localhost:5068/api/insights/progress \
  -H "Content-Type: application/json" \
  -d '{"exercise":"Bench Press","history":[{"date":"2024-12-01","value":135}],"metric":"weight"}'
```

## Deployment

This project is deployed to **Azure App Service** via GitHub Actions CI/CD.

- **Deployment Method**: GitHub Actions (automatic on push to `main` branch)
- **Workflow**: `.github/workflows/azure-deploy.yml`
- **Status**: ✅ Active and deployed

### Deployment Process

1. Push to `main` branch triggers GitHub Actions
2. Builds and tests the application
3. Publishes the application
4. Deploys to Azure App Service automatically

## Project Structure

```
liftledger-services/
├── Controllers/
│   ├── InsightsController.cs    # API endpoint handler
│   └── ErrorController.cs        # Error handling
├── Models/
│   ├── ProgressPoint.cs          # Data point model
│   ├── ProgressRequest.cs        # Request model
│   └── ProgressInsight.cs        # Response model
├── Services/
│   └── InsightsService.cs        # Business logic
├── Program.cs                     # Application entry point
└── .github/workflows/
    └── azure-deploy.yml           # CI/CD pipeline
```

## Technology Stack

- .NET 10.0
- ASP.NET Core Web API
- C# 12
- Azure App Service (Linux)
- GitHub Actions

## Version History

### v1.0 (Current - Deployed)
- ✅ Stateless progress insights service
- ✅ PR detection and delta calculations
- ✅ Smart insight text generation
- ✅ Early logging detection
- ✅ Health check endpoint
- ✅ Production deployment to Azure
- ✅ CI/CD via GitHub Actions

## License

MIT