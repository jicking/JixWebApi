using AutoWrapper;
using JixWebApi.Core.Services;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

namespace JixWebApi;
public class Program {
	public static void Main(string[] args) {

		var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
		logger.Debug("Init main JixWebAPI =================");

		try {
			var builder = WebApplication.CreateBuilder(args);

			// NLog: Setup NLog for Dependency injection
			builder.Logging.ClearProviders();
			builder.Host.UseNLog();

			// Swagger
			builder.Services.AddSwaggerGen();

			// CORS
			builder.Services.AddCors(options => {
				options.AddDefaultPolicy(policy => {
					policy
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
				});
			});

			// Auth
			// Data

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			// Add custom Options
			// Add custom Services
			builder.Services.AddScoped<IProjectService, ProjectService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment()) {
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			if (!app.Environment.IsProduction()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseRouting();
			app.UseCors();
			app.UseAuthorization();
			//app.UseAuthentication();
			app.MapRazorPages();
			app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { IsApiOnly = false });
			app.MapControllers();

			app.Run();
		}
		catch (Exception exception) {
			// NLog: catch setup errors
			logger.Error(exception, "Stopped JixWebAPI because of exception =================");
			throw;
		}
		finally {
			// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
			NLog.LogManager.Shutdown();
		}
	}
}