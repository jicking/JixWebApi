using JixWebApp.Core;
using JixWebApp.Core.DTO;
using JixWebApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JixWebApp.App.Queries;

public class GetAllProjectsQuery : IRequest<List<ProjectDto>> {

}

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectDto>> {
	private readonly JixWebAppDbContext _db;

	public GetAllProjectsQueryHandler(JixWebAppDbContext dbContext) {
		this._db = dbContext;
	}

	public async Task<List<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken) {
		var data = await _db.Projects
			.AsNoTracking()
			.Where(p => !p.IsDeleted)
			.ToListAsync();
		return data.ToDto();
	}
}