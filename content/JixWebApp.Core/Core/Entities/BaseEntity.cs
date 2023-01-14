namespace JixWebApp.Core.Entities;


public interface IEntityHistory<T> {
	DateTimeOffset? Created { get; set; }
	T CreatedBy { get; set; }
	DateTimeOffset? Deleted { get; set; }
	T DeletedBy { get; set; }
	bool IsDeleted { get; set; }
}

public abstract class BaseEntity : IEntityHistory<string> {
	public Guid Id { get; set; }

	public DateTimeOffset? Created { get; set; }
	public string CreatedBy { get; set; }
	public DateTimeOffset? LastUpdated { get; set; }
	public string LastUpdatedBy { get; set; }
	public DateTimeOffset? Deleted { get; set; }
	public string DeletedBy { get; set; }
	public bool IsDeleted { get; set; }
}
