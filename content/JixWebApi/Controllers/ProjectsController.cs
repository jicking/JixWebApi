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
	public async Task<IEnumerable<ProjectDto>> GetAsync() {
		_logger.LogInformation("Fetch all projects");
		return await _projectService.GetAllAsync();
	}

	// POST api/<ProjectsController>
	[HttpPost]
	public async Task<IActionResult> PostAsync([FromBody] ProjectDto value) {
		try {
			// custom validation

			var addResult = await _projectService.AddAsync(value);

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
