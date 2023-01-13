using JixWebApp.App.Queries;
using JixWebApp.Controllers;
using JixWebApp.Core;
using JixWebApp.Core.DTO;
using JixWebApp.Data;
using JixWebApp.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Xunit;

namespace JixWebApp.Tests.Controllers;

public class ProjectsControllerTests {
	private readonly ProjectsController _sut;

	public ProjectsControllerTests() {
		// setup
		// mock dependencies
		var logger = NullLogger<ProjectsController>.Instance;
		var projectService = Substitute.For<IProjectService>();
		var storageService = Substitute.For<IStorageService>();
		var mediator = Substitute.For<IMediator>();

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
		mediator
			.Send(Arg.Any<GetAllProjectsQuery>())
			.Returns(new List<ProjectDto>() {
				DefaultValues.ProjectInput
			});

		_sut = new ProjectsController(logger, projectService, storageService, mediator);
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
