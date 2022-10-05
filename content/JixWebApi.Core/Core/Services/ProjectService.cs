using JixWebApi.Core.DTO;
using JixWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace JixWebApi.Core.Services;

public interface IProjectService {
	Task<List<ProjectDto>> GetAllAsync();
	Task<Result<ProjectDto>> AddAsync(ProjectDto value);
}

public class ProjectService : IProjectService {

	private static JixWebApiDbContext _db;

	public ProjectService(
		JixWebApiDbContext dbContext
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
		var data = await _db.Projects.AsNoTracking().ToListAsync();
		return data.ToDto();
	}
}
