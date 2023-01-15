using AutoWrapper.Extensions;
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
			builder.Services.AddApplicationInsightsTelemetry();

			// Azure KeyVault
			var keyVaultName = builder.Configuration["AzureKeyVaultName"];
			if (!string.IsNullOrEmpty(keyVaultName) && !builder.Environment.IsDevelopment()) {
				logger.Info("Will use Azure Keyvault");
				builder.Configuration.AddAzureKeyVault(
					new Uri($"https://{keyVaultName}.vault.azure.net/"),
					new DefaultAzureCredential());
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
			bool useInmemoryDb = builder.Configuration["UseInmemoryDb"].ToBoolean();
			if (useInmemoryDb) {
				builder.Services.AddDbContext<JixWebAppDbContext>(options =>
					options.UseInMemoryDatabase("JixWebAppDbContext"));
			} else {
				var connectionString = builder.Configuration.GetConnectionString("JixWebAppDbContext");
				builder.Services.AddDbContext<JixWebAppDbContext>(options =>
					options.UseSqlServer(connectionString));
			}

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			// Add custom Options
			builder.Services.Configure<StorageServiceOptions>(
				builder.Configuration.GetSection(StorageServiceOptions.SectionName));

			// Add custom Services
			builder.Services.AddScoped<IStorageService, StorageService>();

			// MediatR
			builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

			var app = builder.Build();

			// Seed test data for inmemory db only.
			if (useInmemoryDb) {
				using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
				serviceScope.ServiceProvider.GetService<JixWebAppDbContext>().SeedTestData();
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			else {
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
			// app.UseAuthorization();
			// app.UseAuthentication();
			app.MapRazorPages();
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