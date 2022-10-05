using JixWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JixWebApi.Data;

public class JixWebApiDbContext : DbContext {
	public JixWebApiDbContext(DbContextOptions<JixWebApiDbContext> options)
		: base(options) {
	}

	public DbSet<Project> Projects { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		// Seed data but will be part of migration
		//modelBuilder.Entity<Project>().HasData(DefaultValues.Projects);

		// Will exclude deleted objects from default query
		// Enable when not using EF InMemoryDB
		//modelBuilder.Entity<Project>().HasQueryFilter(c => !c.IsDeleted);
	}

	public override int SaveChanges() {
		SetMetaFields();
		return base.SaveChanges();
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
		SetMetaFields();
		return base.SaveChangesAsync(cancellationToken);
	}

	private void SetMetaFields() {
		foreach (var entry in ChangeTracker.Entries<BaseEntity>()) {
			switch (entry.State) {
				case EntityState.Added:
					entry.Entity.Created = DateTimeOffset.Now;
					break;

					//case EntityState.Modified:
					//	entry.Entity.LastUpdatedOn = DateTimeOffset.Now;
					//	break;
			}
		}
	}

	public void SeedInMemoryDb() {
		if (Projects.Any())
			return;

		Projects.AddRange(DefaultValues.Projects);
		SaveChanges();
	}
}
