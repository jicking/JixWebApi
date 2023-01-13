using JixWebApp.Core;
using JixWebApp.Core.DTO;
using JixWebApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JixWebApp.App.Commands;
public sealed record AddProjectCommand : IRequest<Result<ProjectDto>> {
	public ProjectDto Project { get; set; }
}

public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, Result<ProjectDto>> {
	private readonly JixWebAppDbContext _db;

	public AddProjectCommandHandler(JixWebAppDbContext dbContext) {
		this._db = dbContext;
	}

	public async Task<Result<ProjectDto>> Handle(AddProjectCommand request, CancellationToken cancellationToken) {
		try {
			// validation
			var validationErrors = new List<KeyValuePair<string, string>>();
			var value = request.Project;
			var existingProjectOnDB = await _db.Projects.AsNoTracking().FirstOrDefaultAsync(c => c.Name == value.Name);
			if (existingProjectOnDB != null) {
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

			return new Result<ProjectDto>(value);
		}
		catch (Exception ex) {
			return new Result<ProjectDto>(ex);
		}
	}
}
