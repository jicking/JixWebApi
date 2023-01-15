using JixWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace JixWebApp.App.Queries.Tests;
public class GetAllProjectsQueryHandlerTests {

	private readonly GetAllProjectsQueryHandler _sut;

	public GetAllProjectsQueryHandlerTests() {
		//	Setup
		//	Mock dependencies
		var logger = NullLogger<GetAllProjectsQueryHandler>.Instance;

		//	Set EF
		var options = new DbContextOptionsBuilder<JixWebAppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;
		var context = new JixWebAppDbContext(options);
		context.SeedTestData();

		_sut = new GetAllProjectsQueryHandler(context, logger);
	}

	[Fact()]
	public async Task HandleTest() {
		var result = await _sut.Handle(new GetAllProjectsQuery(), default);
		Assert.NotEmpty(result);
	}
}