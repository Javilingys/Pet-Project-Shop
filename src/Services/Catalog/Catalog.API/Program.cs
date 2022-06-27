using Catalog.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = CreateSerilogLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration);
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CatalogDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"), dbOptions =>
    {
        dbOptions.EnableRetryOnFailure();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await CatalogDbContextSeed.Seed(app);

app.Run();

// Create bootstrap logger before main logger inside Host>UseSerilog() will be configured
Serilog.ILogger CreateSerilogLogger()
{
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .CreateBootstrapLogger();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}