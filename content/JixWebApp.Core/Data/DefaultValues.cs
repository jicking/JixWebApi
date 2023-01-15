using JixWebApp.Core.DTO;
using JixWebApp.Core.Entities;

namespace JixWebApp.Data;

/// <summary>
/// Declare default values and test data(prefix with test) here
/// </summary>
public static class DefaultValues {

	// Test Data
	public static List<Project> TestProjects = new List<Project> {
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


	// DTOs - for testing as data payload
	public static ProjectDto TestProjectDto = new ProjectDto() {
		Id = Guid.NewGuid(),
		Name = "Test",
		Description = "Test"
	};

	// Default Values ...
	public static Guid ProjectId = Guid.NewGuid();

	public static List<Project> Projects = new List<Project> {
		new Project() {
			Id = ProjectId,
			Name = "Demo Project",
			Description = "Demo Project"
		}
	};
}
