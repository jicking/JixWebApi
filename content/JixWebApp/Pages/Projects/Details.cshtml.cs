using JixWebApp.Core.Entities;
using JixWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JixWebApp.Pages.Projects {
	public class DetailsModel : PageModel {
		private readonly JixWebAppDbContext _context;

		public DetailsModel(JixWebAppDbContext context) {
			_context = context;
		}

		public Project Project { get; set; }

		public async Task<IActionResult> OnGetAsync(Guid? id) {
			if (id == null || _context.Projects == null) {
				return NotFound();
			}

			var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
			if (project == null) {
				return NotFound();
			}
			else {
				Project = project;
			}
			return Page();
		}
	}
}
