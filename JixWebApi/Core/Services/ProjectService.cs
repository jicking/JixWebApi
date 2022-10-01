using JixWebApi.Core.DTO;

namespace JixWebApi.Core.Services;

public interface IProjectService {
	List<ProjectDto> GetAll();
	ProjectDto Add(ProjectDto value);
}

public class ProjectService : IProjectService {

	private static List<ProjectDto> _projects = new List<ProjectDto>();

	public ProjectDto Add(ProjectDto value) {
		value.Id = Guid.NewGuid();
		_projects.Add(value);
		return value;
	}

	public List<ProjectDto> GetAll() {
		return _projects;
	}
}
