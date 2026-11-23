var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS")
    ?? "http://localhost:6765");

string storeUrl = Environment.GetEnvironmentVariable("STORE_URL")
    ?? "http://localhost:6767";

string analyzeUrl = Environment.GetEnvironmentVariable("ANALYZE_URL")
    ?? "http://localhost:6766";

builder.Services.AddHttpClient("Store", client =>
{
    client.BaseAddress = new Uri(storeUrl);
});

builder.Services.AddHttpClient("Analyze", client =>
{
    client.BaseAddress = new Uri(analyzeUrl);
});

builder.Services.AddSingleton<SubmissionService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
