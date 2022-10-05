using JixWebApi.Core.DTO;

namespace JixWebApi.Core.Services;

public interface IProjectService {
	List<ProjectDto> GetAll();
	Result<ProjectDto> Add(ProjectDto value);
}

public class ProjectService : IProjectService {

	private static List<ProjectDto> _projects = new List<ProjectDto>();

	public Result<ProjectDto> Add(ProjectDto value) {

		try {
			// validation
			var validationErrors = new List<KeyValuePair<string, string>>();
			if (_projects.Select(p => p.Name).Contains(value.Name)) {
				validationErrors.Add(new KeyValuePair<string, string>("name", "project name already exist on db."));
			}

			if (validationErrors.Count > 0) {
				return new Result<ProjectDto>(validationErrors);
			}

			// persist
			value.Id = Guid.NewGuid();
			_projects.Add(value);
			return new Result<ProjectDto>(value);
		}
		catch (Exception ex) {
			return new Result<ProjectDto>(ex);
		}
	}

	public List<ProjectDto> GetAll() {
		return _projects;
	}
}
