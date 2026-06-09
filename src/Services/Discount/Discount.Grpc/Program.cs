using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// ✅ Ensure /app/data directory exists before migrations run
var connectionString = builder.Configuration.GetConnectionString("Database");
var dataSource = connectionString?.Replace("Data Source=", "").Trim();
if (!string.IsNullOrEmpty(dataSource))
{
    var dir = Path.GetDirectoryName(dataSource);
    if (!string.IsNullOrEmpty(dir))
        Directory.CreateDirectory(dir);
}

app.UseMigration();
app.MapGrpcService<DiscountService>();

app.Run();