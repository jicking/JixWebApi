using JixWebApp.Core;
using JixWebApp.Core.DTO;
using JixWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JixWebApp.Services;

public interface IProjectService {
	Task<List<ProjectDto>> GetAllAsync();
	Task<Result<ProjectDto>> AddAsync(ProjectDto value);
}

public class ProjectService : IProjectService {

	private static JixWebAppDbContext _db;

	public ProjectService(
		JixWebAppDbContext dbContext
		) {
		_db = dbContext;
	}

	public async Task<Result<ProjectDto>> AddAsync(ProjectDto value) {

		try {
			// validation
			var validationErrors = new List<KeyValuePair<string, string>>();
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

	public async Task<List<ProjectDto>> GetAllAsync() {
		var data = await _db.Projects
			.AsNoTracking()
			.Where(p => !p.IsDeleted)
			.ToListAsync();
		return data.ToDto();
	}
}
