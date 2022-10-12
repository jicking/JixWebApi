using JixWebApi.Controllers;
using JixWebApi.Core;
using JixWebApi.Core.DTO;
using JixWebApi.Core.Services;
using JixWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Xunit;

namespace JixWebApi.Tests.Controllers;

public class ProjectsControllerTests {
	private readonly ProjectsController _sut;

	public ProjectsControllerTests() {
		// setup
		// mock dependencies
		var logger = NullLogger<ProjectsController>.Instance;
		var projectService = Substitute.For<IProjectService>();
		var storageService = Substitute.For<IStorageService>();

		// mocks results
		projectService
			.GetAllAsync()
			.Returns(new List<ProjectDto>() {
				DefaultValues.ProjectInput
			});
		projectService
			.AddAsync(Arg.Any<ProjectDto>())
			.Returns(new Result<ProjectDto>(DefaultValues.ProjectInput));
		storageService
			.UploadFileDemoAsync(Arg.Any<IFormFile>(), Arg.Any<string>())
			.Returns("file.jpg");

		_sut = new ProjectsController(logger, projectService, storageService);
	}

	[Fact()]
	public void GetTest() {
		var result = _sut.GetAsync().Result;
		Assert.NotEmpty(result);
	}

	[Fact()]
	public void PostTest() {
		var project = new CreateProjectDto() {
			Name = DefaultValues.ProjectInput.Name,
			Description = DefaultValues.ProjectInput.Description
		};
		var result = (OkObjectResult)_sut.PostAsync(project).Result;
		var value = (ProjectDto)result.Value;
		Assert.Equal(project.Name, value.Name);
		Assert.Equal(project.Description, value.Description);
	}
}
