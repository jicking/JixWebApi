using JixWebApp.Core;
using JixWebApp.Core.DTO;
using JixWebApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JixWebApp.App.Commands;
public sealed record AddProjectCommand : IRequest<Result<ProjectDto>> {
	public ProjectDto Project { get; set; }
}

public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, Result<ProjectDto>> {
	private readonly JixWebAppDbContext _db;
	private readonly ILogger<AddProjectCommandHandler> _logger;

	public AddProjectCommandHandler(
		JixWebAppDbContext dbContext,
		ILogger<AddProjectCommandHandler> logger
		) {
		this._db = dbContext;
		this._logger = logger;
	}

	public async Task<Result<ProjectDto>> Handle(AddProjectCommand request, CancellationToken cancellationToken) {
		try {
			_logger.LogDebug($"Start process");

			// validation
			var validationErrors = new List<KeyValuePair<string, string>>();
			var value = request.Project;
			var existingProjectOnDB = await _db.Projects.AsNoTracking().FirstOrDefaultAsync(c => c.Name == value.Name);
			if (existingProjectOnDB != null) {
				_logger.LogWarning($"project name [{value.Name}] already exist on db.");
				validationErrors.Add(new KeyValuePair<string, string>("Name", "project name already exist on db."));
			}

			if (validationErrors.Count > 0) {
				return new Result<ProjectDto>(validationErrors);
			}

			// persist
			var project = value.ToEntity();
			_db.Add(project);
			await _db.SaveChangesAsync();

			value = project.ToDto();

			_logger.LogDebug($"End process");

			return new Result<ProjectDto>(value);
		}
		catch (Exception ex) {
			_logger.LogError(ex, $"Error");
			return new Result<ProjectDto>(ex);
		}
	}
}
