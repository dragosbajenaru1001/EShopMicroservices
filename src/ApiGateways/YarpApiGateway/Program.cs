var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP configuration pipeline
app.MapReverseProxy();

app.Run();
