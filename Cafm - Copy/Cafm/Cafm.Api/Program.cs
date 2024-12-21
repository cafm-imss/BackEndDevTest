using Cafm.Api.Extentions;
using Cafm.Application;
using Cafm.Application.Hubs;
using Cafm.Framework.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configurations(builder.Configuration);
builder.Services.AddRepository(builder.Configuration);

Log.Logger = SerilogLogger.Initialize();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<WorkOrderHub>("/workOrderHub");
    endpoints.MapControllers(); // API endpoints
});

app.Run();
