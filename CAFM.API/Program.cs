using CAFM.API.Extension;
using CAFM.Core.Hubs;
using CAFM.Database.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace CAFM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register DbContext
            builder.Services.AddDbContext<CmmsBeTestContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddApplicationServices();
            // Configure CORS
            builder.Services.AddCors(options => {
                options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
            });

            builder.Services.AddControllers()
                       .AddJsonOptions(options =>
                       {
                           options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                           options.JsonSerializerOptions.MaxDepth = 64; // Optionally, increase the max depth if needed
                       });            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseCors("CORSPolicy");

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  // Your other controller routes
                endpoints.MapHub<WorkOrderHub>("/workOrderHub");  // Map the SignalR hub
            });
            app.UseCors(); // Apply the CORS policy

            app.MapControllers();

            app.Run();
        }
    }
}
