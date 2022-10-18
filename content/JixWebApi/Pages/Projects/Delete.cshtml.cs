using JixWebApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JixWebApi.Pages.Projects {
	public class DeleteModel : PageModel {
		private readonly JixWebApi.Data.JixWebApiDbContext _context;

		public DeleteModel(JixWebApi.Data.JixWebApiDbContext context) {
			_context = context;
		}

		[BindProperty]
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

		public async Task<IActionResult> OnPostAsync(Guid? id) {
			if (id == null || _context.Projects == null) {
				return NotFound();
			}
			var project = await _context.Projects.FindAsync(id);

			if (project != null) {
				Project = project;
				_context.Projects.Remove(Project);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
