using Microsoft.AspNetCore.Http;

namespace JixWebApp.Core.DTO;

public class ProjectDto {
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public bool IsDisabled { get; set; }
}

public class CreateProjectDto {
	public string Name { get; set; }
	public string Description { get; set; }

	// Make sure you set your form enctype="multipart/form-data"
	[MaxFileSize(2 * 1024 * 1024)]
	[AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
	public IFormFile Logo { get; set; }
}
