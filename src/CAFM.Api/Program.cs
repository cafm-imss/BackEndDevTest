using CAFM.Core.Hubs;
using CAFM.Core.Services;
using CAFM.Database;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Allow requests from Angular app
              .AllowAnyHeader() // Allow all headers
              .AllowAnyMethod() // Allow all HTTP methods
              .AllowCredentials(); // Allow cookies or credentials
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
builder.Services.AddSignalR();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CAFMDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("CAFMDB");
    options.UseSqlServer(connectionString);
});
//We should use interface instead of concrete class
builder.Services.AddScoped<ErrorLogService>();
builder.Services.AddScoped<WorkOrderService>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "CAFM API",
        Description = "API documentation for Work Orders management",
    });

    // Add XML comments (optional, for detailed method descriptions)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});
var app = builder.Build();
app.UseCors();
app.MapHub<WorkOrderHub>("/workOrderHub");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ar"),
    SupportedCultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("ar") },
    SupportedUICultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("ar") }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
