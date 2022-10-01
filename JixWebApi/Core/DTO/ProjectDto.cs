namespace JixWebApi.Core.DTO;

public class ProjectDto {
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public bool IsDisabled { get; set; }
}
