using JixWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace JixWebApp.App.Commands.Tests;
public class AddProjectCommandHandlerTest {
	private readonly Commands.AddProjectCommandHandler _sut;

	public AddProjectCommandHandlerTest() {
		//	Setup
		//	Mock dependencies
		var logger = NullLogger<AddProjectCommandHandler>.Instance;


		//	Set EF
		var options = new DbContextOptionsBuilder<JixWebAppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		var context = new JixWebAppDbContext(options);
		context.SeedTestData();

		_sut = new AddProjectCommandHandler(context, logger);
	}

	[Fact()]
	public async Task HandleNewProjectTest() {
		var command = new AddProjectCommand();
		command.Project = DefaultValues.TestProjectDto;
		var result = await _sut.Handle(command, default);

		Assert.True(result.IsSuccess);
		Assert.NotNull(result.Value);
	}
}