using JixWebApp.Core.DTO;
using JixWebApp.Core.Entities;

namespace JixWebApp.Data;

public static class DefaultValues {
	public static ProjectDto ProjectInput = new ProjectDto() {
		Id = Guid.NewGuid(),
		Name = "Test",
		Description = "Test"
	};

	public static List<Project> Projects = new List<Project> {
		new Project() {
			Id = Guid.NewGuid(),
			Name = "Active Project",
			Description = "Test"
		},
		new Project() {
			Id = Guid.NewGuid(),
			Name = "Disabled Project",
			Description = "Test",
			IsDisabled = true
		}
	};
}
