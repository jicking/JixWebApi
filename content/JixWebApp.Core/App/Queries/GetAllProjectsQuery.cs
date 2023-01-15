using JixWebApp.Core;
using JixWebApp.Core.DTO;
using JixWebApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JixWebApp.App.Queries;

public class GetAllProjectsQuery : IRequest<List<ProjectDto>> {

}

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectDto>> {
	private readonly JixWebAppDbContext _db;
	private readonly ILogger<GetAllProjectsQueryHandler> _logger;
	public GetAllProjectsQueryHandler(
		JixWebAppDbContext dbContext,
		ILogger<GetAllProjectsQueryHandler> logger
		) {
		this._db = dbContext;
		this._logger = logger;
	}

	public async Task<List<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken) {
		_logger.LogDebug($"Start query");
		var data = await _db.Projects
			.AsNoTracking()
			.Where(p => !p.IsDeleted)
			.ToListAsync();
		_logger.LogDebug($"End query");
		return data.ToDto();
	}
}