using JixWebApp.Core.DTO;
using JixWebApp.Core.Entities;
using JixWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JixWebApp.App.Commands.Tests;
public class AddProjectCommandHandlerTest {
	private readonly Commands.AddProjectCommandHandler _sut;

	public AddProjectCommandHandlerTest() {
		// setup
		// mock dependencies

		//Set EF
		var options = new DbContextOptionsBuilder<JixWebAppDbContext>()
			.UseInMemoryDatabase("JixWebAppDbContext")
			.Options;

		var context = new JixWebAppDbContext(options);

		// seed
		// TODO: Better to seed test scenarios in db init
		var project = new Project() {
			Name = "GetAllTest",
			Description = "GetAllTest"
		};
		context.Projects.Add(project);
		context.SaveChanges();

		_sut = new Commands.AddProjectCommandHandler(context);
	}

	[Fact()]
	public async Task HandleNewProjectTest() {
		var project = new ProjectDto() {
			Name = "New",
			Description = "New"
		};
		var command = new AddProjectCommand();
		command.Project = project;
		var result = await _sut.Handle(command, default);

		Assert.True(result.IsSuccess);
		Assert.NotNull(result.Value);
	}
}