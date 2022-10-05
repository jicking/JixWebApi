using JixWebApi.Controllers;
using JixWebApi.Core;
using JixWebApi.Core.DTO;
using JixWebApi.Core.Services;
using JixWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Xunit;

namespace JixWebApi.Tests.Controllers {
	public class ProjectsControllerTests {
		private readonly ProjectsController _sut;

		public ProjectsControllerTests() {
			// setup
			// mock dependencies
			var logger = NullLogger<ProjectsController>.Instance;
			var projectService = Substitute.For<IProjectService>();

			// mocks results
			projectService
				.GetAll()
				.Returns(new List<ProjectDto>() {
					DefaultValues.ProjectInput
				});
			projectService
				.Add(Arg.Any<ProjectDto>())
				.Returns(new Result<ProjectDto>(DefaultValues.ProjectInput));

			_sut = new ProjectsController(logger, projectService);
		}

		[Fact()]
		public void GetTest() {
			var result = _sut.Get();
			Assert.NotEmpty(result);
		}

		[Fact()]
		public void PostTest() {
			var project = DefaultValues.ProjectInput;
			var result = (OkObjectResult)_sut.Post(project);
			var value = (ProjectDto)result.Value;
			Assert.Equal(project.Name, value.Name);
			Assert.Equal(project.Description, value.Description);
		}
	}
}