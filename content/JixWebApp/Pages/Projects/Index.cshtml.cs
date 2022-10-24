using JixWebApp.Core.Entities;
using JixWebApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JixWebApp.Pages.Projects {
	public class IndexModel : PageModel {
		private readonly JixWebAppDbContext _context;

		public IndexModel(JixWebAppDbContext context) {
			_context = context;
		}

		public IList<Project> Project { get; set; } = default!;

		public async Task OnGetAsync() {
			if (_context.Projects != null) {
				Project = await _context.Projects.ToListAsync();
			}
		}
	}
}
