using JixWebApp.Core.Entities;
using JixWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JixWebApp.Pages.Projects {
	public class CreateModel : PageModel {
		private readonly JixWebAppDbContext _context;

		public CreateModel(JixWebAppDbContext context) {
			_context = context;
		}

		public IActionResult OnGet() {
			return Page();
		}

		[BindProperty]
		public Project Project { get; set; }


		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync() {
			if (!ModelState.IsValid) {
				return Page();
			}

			_context.Projects.Add(Project);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
