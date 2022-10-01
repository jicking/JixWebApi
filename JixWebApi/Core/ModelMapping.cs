using JixWebApi.Core.DTO;
using JixWebApi.Core.Entities;
using Mapster;

namespace JixWebApi.Core;
public static class ModelMapping {
	public static ProjectDto ToDto(this Project model) {
		if (model == null) return null;
		return model.Adapt<ProjectDto>();
	}

	public static List<ProjectDto> ToDto(this IList<Project> model) {
		var result = new List<ProjectDto>();
		foreach (var item in model)
			result.Add(item.ToDto());
		return result;
	}

	public static Project ToEntity(this ProjectDto dto) {
		if (dto == null) return null;
		return dto.Adapt<Project>();
	}
}
