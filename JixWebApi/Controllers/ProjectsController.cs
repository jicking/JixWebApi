using JixWebApi.Core.DTO;
using JixWebApi.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace JixWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase {
	private readonly ILogger<ProjectsController> _logger;
	private readonly IProjectService _projectService;

	public ProjectsController(
		ILogger<ProjectsController> logger,
		IProjectService projectService) {
		_logger = logger;
		_projectService = projectService;
	}

	// GET: api/<ProjectsController>
	[HttpGet]
	public IEnumerable<ProjectDto> Get() {
		return _projectService.GetAll();
	}

	// POST api/<ProjectsController>
	[HttpPost]
	public IActionResult Post([FromBody] ProjectDto value) {
		try {
			// custom validation

			var newProject = _projectService.Add(value);
			_logger.LogInformation("Added new project");
			return Ok(newProject);
		}
		catch (Exception ex) {
			_logger.LogError(ex, "");
			throw;
		}
	}
}
