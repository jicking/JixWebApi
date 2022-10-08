using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JixWebApi.Data;

public class JixWebApiDbContextFactory : IDesignTimeDbContextFactory<JixWebApiDbContext> {
	JixWebApiDbContext IDesignTimeDbContextFactory<JixWebApiDbContext>.CreateDbContext(string[] args) {
		var optionsBuilder = new DbContextOptionsBuilder<JixWebApiDbContext>();
		optionsBuilder.UseInMemoryDatabase("JixWebApiDb");
		//optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JixWebApiDb;Trusted_Connection=True;MultipleActiveResultSets=true");

		return new JixWebApiDbContext(optionsBuilder.Options);
	}
}
