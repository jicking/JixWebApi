using JixWebApp.Core.DTO;
using JixWebApp.Core.Services;
using JixWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JixWebApp.Tests.Core.Services;

public class ProjectServiceTests {

	private readonly IProjectService _sut;

	public ProjectServiceTests() {
		// setup
		// mock dependencies

		//Set EF
		var options = new DbContextOptionsBuilder<JixWebAppDbContext>()
			.UseInMemoryDatabase("JixWebAppDbContext")
			.Options;

		var context = new JixWebAppDbContext(options);

		_sut = new ProjectService(context);
	}

	[Fact()]
	public void AddTest() {
		var project = new ProjectDto() {
			Name = "test",
			Description = "test"
		};
		var result = _sut.AddAsync(project).Result;

		// check result states
		Assert.True(result.IsSuccess);
		Assert.False(result.HasValidationError);
		Assert.False(result.IsError);

		// compare result value to input
		Assert.Equal(result.Value.Name, project.Name);
		Assert.Equal(result.Value.Description, project.Description);
		Assert.Equal(result.Value.IsDisabled, project.IsDisabled);
	}

	[Fact()]
	public void GetAllTest() {
		var project = new ProjectDto() {
			Name = "GetAllTest",
			Description = "GetAllTest"
		};
		_sut.AddAsync(project).Wait();
		var result = _sut.GetAllAsync().Result;

		Assert.NotEmpty(result);
	}
}
