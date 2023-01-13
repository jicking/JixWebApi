using AutoWrapper.Wrappers;
using JixWebApp.App.Commands;
using JixWebApp.App.Queries;
using JixWebApp.Core;
using JixWebApp.Core.DTO;
using JixWebApp.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JixWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase {
	private readonly ILogger<ProjectsController> _logger;
	private readonly IStorageService _storageService;
	private readonly IMediator _mediator;

	public ProjectsController(
		ILogger<ProjectsController> logger,
		IStorageService storageService,
		IMediator mediator) {
		_logger = logger;
		_storageService = storageService;
		_mediator = mediator;
	}

	// GET: api/<ProjectsController>
	[HttpGet]
	public async Task<IEnumerable<ProjectDto>> GetAsync() {
		try {
			_logger.LogInformation("Fetch all projects");
			var data = await _mediator.Send(new GetAllProjectsQuery());
			return data;
		}
		catch (Exception ex) {
			_logger.LogError(ex, "");
			throw;
		}
	}

	// POST api/<ProjectsController>
	[HttpPost]
	public async Task<IActionResult> PostAsync([FromForm] CreateProjectDto value) {
		// custom validation

		// process file upload
		if (value.Logo != null) {
			var logoUrl = await _storageService.UploadFileDemoAsync(value.Logo, value.Name);
		}

		var addResult = await _mediator.Send(new AddProjectCommand() { Project = value.ToDto() });

		if (addResult.IsError) {
			throw addResult.Exception;
		}

		// throw validation error
		if (addResult.HasValidationError) {
			foreach (var e in addResult.ValidationErrors) {
				ModelState.AddModelError(e.Key, e.Value);
			}
			throw new ApiProblemDetailsException(ModelState);
		}

		_logger.LogInformation("Added new project ");
		return Ok(addResult.Value);
	}
}
