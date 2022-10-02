using JixWebApi.Core.DTO;
using JixWebApi.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
		_logger.LogInformation("Fetch all projects");
		return _projectService.GetAll();
	}

	// POST api/<ProjectsController>
	[HttpPost]
	public IActionResult Post([FromBody] ProjectDto value) {
		try {
			// custom validation

			var addResult = _projectService.Add(value);

			if (addResult.IsError) {
				throw addResult.Exception;
			}

			if (addResult.HasValidationError) {
				foreach (var e in addResult.ValidationErrors) {
					ModelState.AddModelError(e.Key, e.Value);
				}
				return BadRequest(ModelState);
			}

			_logger.LogInformation("Added new project ");
			return Ok(addResult.Value);
		}
		catch (Exception ex) {
			_logger.LogError(ex, "");
			throw;
		}
	}
}
