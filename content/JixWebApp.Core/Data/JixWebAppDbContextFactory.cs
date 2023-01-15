using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JixWebApp.Data;

// This is required when scaffolding Razor Page/Controller
// because EF dbcontext is on a separate project library.
// Expecting  that you are using EF inmemory db,
// if not then update the options builder (eg: sqlite, sql).
// Disable this code when running EF migrations
// will need this at initial prototyping stage anyways.
public class JixWebAppDbContextFactory : IDesignTimeDbContextFactory<JixWebAppDbContext> {
	JixWebAppDbContext IDesignTimeDbContextFactory<JixWebAppDbContext>.CreateDbContext(string[] args) {
		var optionsBuilder = new DbContextOptionsBuilder<JixWebAppDbContext>();
		optionsBuilder.UseInMemoryDatabase("JixWebAppDb");
		return new JixWebAppDbContext(optionsBuilder.Options);
	}
}
