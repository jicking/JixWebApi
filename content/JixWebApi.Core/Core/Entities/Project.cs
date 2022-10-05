namespace JixWebApi.Core.Entities;

public class Project : BaseEntity {
	public string Name { get; set; }
	public string Description { get; set; }
	public bool IsDisabled { get; set; }
}
