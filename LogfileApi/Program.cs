using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;
using LogfileApi.Services.LogFileControllerEndpoints;
using LogfileApi.Services.LogFileItemsControllerEndpoints;
using LogfileApi.Services.LogFileTitlesControllerEndpoints;
using LogfileApi.Services.LogFileItemsControllerEndpoints.DevelopmentServices;
using LogfileApi.Services.LogFileTitlesControllerEndpoints.DevelopmentServices;

namespace LogfileApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Ensure the connection string is loaded correctly
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<LogFileContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost3000",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // LogFileItemsControllerEndpoints services
            builder.Services.AddScoped<LogFileItemSearcher>();
            builder.Services.AddScoped<LogFileItemPoster>();
            builder.Services.AddScoped<LogFileItemDeleter>();

            // LogFileTitlesControllerEndpoints services
            builder.Services.AddScoped<LogFileTitleSearcher>();
            builder.Services.AddScoped<LogFileTitleRowCounter>();
            builder.Services.AddScoped<LogFileTitlePoster>();
            builder.Services.AddScoped<LogFileTitleDeleter>();
            builder.Services.AddScoped<LogFileTitleUpdater>();
            builder.Services.AddScoped<LogFileTitleGetter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use CORS middleware
            app.UseCors("AllowLocalhost3000");

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}