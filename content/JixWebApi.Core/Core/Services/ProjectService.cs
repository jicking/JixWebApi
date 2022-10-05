using JixWebApi.Core.DTO;
using JixWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace JixWebApi.Core.Services;

public interface IProjectService {
	List<ProjectDto> GetAll();
	Result<ProjectDto> Add(ProjectDto value);
}

public class ProjectService : IProjectService {

	private static JixWebApiDbContext _db;

	public ProjectService(
		JixWebApiDbContext dbContext
		) {
		_db = dbContext;
	}

	public Result<ProjectDto> Add(ProjectDto value) {

		try {
			// validation
			var validationErrors = new List<KeyValuePair<string, string>>();
			var existingProjectOnDB = _db.Projects.AsNoTracking().FirstOrDefault(c => c.Name == value.Name);
			if (existingProjectOnDB != null) {
				validationErrors.Add(new KeyValuePair<string, string>("Name", "project name already exist on db."));
			}

			if (validationErrors.Count > 0) {
				return new Result<ProjectDto>(validationErrors);
			}

			// persist
			var project = value.ToEntity();
			_db.Add(project);
			_db.SaveChanges();

			value = project.ToDto();

			return new Result<ProjectDto>(value);
		}
		catch (Exception ex) {
			return new Result<ProjectDto>(ex);
		}
	}

	public List<ProjectDto> GetAll() {
		return _db.Projects.AsNoTracking().ToList().ToDto();
	}
}
