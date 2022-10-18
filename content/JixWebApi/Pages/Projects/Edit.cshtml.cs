using JixWebApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JixWebApi.Pages.Projects {
	public class EditModel : PageModel {
		private readonly JixWebApi.Data.JixWebApiDbContext _context;

		public EditModel(JixWebApi.Data.JixWebApiDbContext context) {
			_context = context;
		}

		[BindProperty]
		public Project Project { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(Guid? id) {
			if (id == null || _context.Projects == null) {
				return NotFound();
			}

			var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
			if (project == null) {
				return NotFound();
			}
			Project = project;
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync() {
			if (!ModelState.IsValid) {
				return Page();
			}

			_context.Attach(Project).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) {
				if (!ProjectExists(Project.Id)) {
					return NotFound();
				}
				else {
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool ProjectExists(Guid id) {
			return _context.Projects.Any(e => e.Id == id);
		}
	}
}
