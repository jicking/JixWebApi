using JixWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JixWebApp.App.Commands.Tests;
public class AddProjectCommandHandlerTest {
	private readonly Commands.AddProjectCommandHandler _sut;

	public AddProjectCommandHandlerTest() {
		//	Setup
		//	Mock dependencies
		//	Set EF
		var options = new DbContextOptionsBuilder<JixWebAppDbContext>()
			.UseInMemoryDatabase("JixWebAppDbContext")
			.Options;

		var context = new JixWebAppDbContext(options);
		context.SeedInMemoryDb();

		_sut = new AddProjectCommandHandler(context);
	}

	[Fact()]
	public async Task HandleNewProjectTest() {
		;
		var command = new AddProjectCommand();
		command.Project = DefaultValues.TestProjectDto;
		var result = await _sut.Handle(command, default);

		Assert.True(result.IsSuccess);
		Assert.NotNull(result.Value);
	}
}