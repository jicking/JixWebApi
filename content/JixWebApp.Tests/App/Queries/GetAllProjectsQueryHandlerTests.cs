using JixWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JixWebApp.App.Queries.Tests;
public class GetAllProjectsQueryHandlerTests {

	private readonly GetAllProjectsQueryHandler _sut;

	public GetAllProjectsQueryHandlerTests() {
		//	Setup
		//	Mock dependencies
		//	Set EF
		var options = new DbContextOptionsBuilder<JixWebAppDbContext>()
			.UseInMemoryDatabase("JixWebAppDbContext")
			.Options;
		var context = new JixWebAppDbContext(options);
		context.SeedInMemoryDb();

		_sut = new GetAllProjectsQueryHandler(context);
	}

	[Fact()]
	public async Task HandleTest() {
		var result = await _sut.Handle(new GetAllProjectsQuery(), default);
		Assert.NotEmpty(result);
	}
}