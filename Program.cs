using LiftLedger.Services.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<InsightsService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                "https://liftledger.fit",
                "http://localhost:3000",
                "http://localhost:5173",
                "http://localhost:8080",
                "http://localhost:4200",
                "https://localhost:3000",
                "https://localhost:5173"
              )
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Add health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Production error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

// CORS must be before UseHttpsRedirection to handle preflight requests
app.UseCors();

app.UseHttpsRedirection();

// Health check endpoint
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();