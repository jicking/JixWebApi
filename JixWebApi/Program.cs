using JixWebApi.Core.Services;
using NLog;
using NLog.Web;

namespace JixWebApi;
public class Program {
	public static void Main(string[] args) {

		var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
		logger.Debug("Init main JixWebAPI =================");

		try {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();

			// Custom Services
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

			app.UseRouting();

			app.UseAuthorization();

			app.MapRazorPages();

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