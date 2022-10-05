using JixWebApi.Core.DTO;
using JixWebApi.Core.Services;
using JixWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JixWebApi.Tests.Core.Services;

public class ProjectServiceTests {

	private readonly IProjectService _sut;

	public ProjectServiceTests() {
		// setup
		// mock dependencies

		//Set EF
		var options = new DbContextOptionsBuilder<JixWebApiDbContext>()
			.UseInMemoryDatabase("JixWebApiDbContext")
			.Options;

		var context = new JixWebApiDbContext(options);

		_sut = new ProjectService(context);
	}

	[Fact()]
	public void AddTest() {
		var project = new ProjectDto() {
			Name = "test",
			Description = "test"
		};
		var result = _sut.Add(project);

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
		_sut.Add(project);
		var result = _sut.GetAll();

		Assert.NotEmpty(result);
	}
}