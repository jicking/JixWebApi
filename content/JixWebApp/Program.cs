using AutoWrapper;
using Azure.Identity;
using JixWebApp.Data;
using JixWebApp.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

namespace JixWebApp;
public class Program {
	public static void Main(string[] args) {

		var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
		logger.Debug("Init main JixWebApp =================");

		try {
			var builder = WebApplication.CreateBuilder(args);

			// NLog: Setup NLog for Dependency injection
			builder.Logging.ClearProviders();
			builder.Host.UseNLog();

			// Azure KeyVault
			var keyVaultName = builder.Configuration["AzureKeyVaultName"];
			if (!string.IsNullOrEmpty(keyVaultName) && !builder.Environment.IsDevelopment()) {
				logger.Info("Will use Azure Keyvault");
				builder.Configuration.AddAzureKeyVault(
					new Uri($"https://{keyVaultName}.vault.azure.net/"),
					new DefaultAzureCredential());
			}

			// App Insights
			var appInsightsConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
			if (string.IsNullOrEmpty(appInsightsConnectionString)) {
				builder.Services.AddApplicationInsightsTelemetry();
			}

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
			//var connectionString = builder.Configuration.GetConnectionString("JixWebAppDbContext");
			//builder.Services.AddDbContext<JixWebAppDbContext>(options =>
			//	options.UseSqlServer(connectionString));
			builder.Services.AddDbContext<JixWebAppDbContext>(options =>
				options.UseInMemoryDatabase("JixWebAppDbContext"));

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			// Add custom Options
			builder.Services.Configure<StorageServiceOptions>(
				builder.Configuration.GetSection(StorageServiceOptions.SectionName));

			// Add custom Services
			builder.Services.AddScoped<IProjectService, ProjectService>();
			builder.Services.AddScoped<IStorageService, StorageService>();

			// MediatR
			//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
			builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			if (!app.Environment.IsDevelopment()) {
				app.UseExceptionHandler("/Error");
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
			//app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions {
			//	IsApiOnly = false,
			//	EnableResponseLogging = false,
			//	IsDebug = false,
			//	ShouldLogRequestData = false,
			//	UseApiProblemDetailsException = true
			//});
			app.MapControllers();

			app.Run();
		}
		catch (Exception exception) {
			// NLog: catch setup errors
			logger.Error(exception, "Stopped JixWebApp because of exception =================");
			throw;
		}
		finally {
			// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
			LogManager.Shutdown();
		}
	}
}