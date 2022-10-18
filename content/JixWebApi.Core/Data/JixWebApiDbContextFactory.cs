using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JixWebApi.Data;

// Disable this code when running EF migrations
// Might only need this at initial prototyping stage anyways
public class JixWebApiDbContextFactory : IDesignTimeDbContextFactory<JixWebApiDbContext> {
	JixWebApiDbContext IDesignTimeDbContextFactory<JixWebApiDbContext>.CreateDbContext(string[] args) {
		var optionsBuilder = new DbContextOptionsBuilder<JixWebApiDbContext>();
		optionsBuilder.UseInMemoryDatabase("JixWebApiDb");
		//optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JixWebApiDb;Trusted_Connection=True;MultipleActiveResultSets=true");

		return new JixWebApiDbContext(optionsBuilder.Options);
	}
}
