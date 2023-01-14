using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JixWebApp.Data;

// Disable this code when running EF migrations
// Might only need this at initial prototyping stage anyways
public class JixWebAppDbContextFactory : IDesignTimeDbContextFactory<JixWebAppDbContext> {
	JixWebAppDbContext IDesignTimeDbContextFactory<JixWebAppDbContext>.CreateDbContext(string[] args) {
		var optionsBuilder = new DbContextOptionsBuilder<JixWebAppDbContext>();
		optionsBuilder.UseInMemoryDatabase("JixWebAppDb");
		return new JixWebAppDbContext(optionsBuilder.Options);
	}
}
